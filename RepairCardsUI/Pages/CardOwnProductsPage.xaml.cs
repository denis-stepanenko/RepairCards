using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class CardOwnProductsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOwnProductRepo _cardProductRepo = new CardOwnProductRepo();
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly CardOwnProductOperationRepo _cardOwnProductOperationRepo = new CardOwnProductOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();

        private readonly int _cardId;

        public CardOwnProductsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addProductEntriesFromTreeButton.Visibility = Visibility.Collapsed;
                addProductFromAnotherCardButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                isOvercoatingRequiredButton.IsEnabled = false;
            }

            if (!new[] { 2, 8 }.Contains(AuthorizationService.User.RoleId))
            {
                exportToPDMButton.IsEnabled = false;
            }

            Refresh();
        }

        void Refresh()
        {
            var products = _cardProductRepo.GetAllByCard(_cardId);
            var trees = GetTreeProducts(products.ToList());
            treeTreeListView.ItemsSource = trees;

            if (_cardRepo.IsConfirmed(_cardId, 1))
            {
                addProductEntriesFromTreeButton.Visibility = Visibility.Collapsed;
                addProductFromAnotherCardButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                isOvercoatingRequiredButton.IsEnabled = false;
            }
        }

        List<TreeProduct> GetTreeProducts(List<CardOwnProduct> products)
        {
            List<TreeProduct> GetChildren(int? parentId) =>
                products.Where(x => x.ParentId == parentId)
                        .Select(x => new TreeProduct
                        {
                            Id = x.Id,
                            Code = x.Code,
                            Name = x.Name,
                            Count = x.Count,
                            Route = x.Route,
                            ParentId = parentId,
                            HasChangedComposition = x.HasChangedComposition,
                            IsOvercoatingRequired = x.IsOvercoatingRequired
                        }).ToList();

            void Build(List<TreeProduct> items)
            {
                items.ForEach(x => x.Children.AddRange(GetChildren(x.Id)));
                items.ForEach(x => Build(x.Children));
            }

            var roots = GetChildren(null);

            Build(roots);

            return roots;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseCardProductWindow = new SelectCardProductWindow();
            if (chooseCardProductWindow.ShowDialog() != true) return;


            var route = _productOperationRepo.GetRoute(chooseCardProductWindow.Product.Code);

            int id = _cardProductRepo.Add(new CardOwnProduct
            {
                CardId = _cardId,
                Code = chooseCardProductWindow.Product.Code,
                Name = chooseCardProductWindow.Product.Name,
                Count = chooseCardProductWindow.Count,
                Route = route
            });

            var operations = _productOperationRepo.GetProductOperations(chooseCardProductWindow.Product.Code, route);

            foreach (var operation in operations)
            {
                var newCardOwnProductOperation = new CardOwnProductOperation
                {
                    CardOwnProductId = id,
                    Code = operation.Code,
                    Name = operation.Name,
                    Labor = operation.Labor,
                    Type = 0,
                    Department = operation.Department
                };

                _cardOwnProductOperationRepo.Add(newCardOwnProductOperation);
            }


            Refresh();
        }

        void Open()
        {
            var ownProduct = (TreeProduct)treeTreeListView.SelectedItem;
            if (ownProduct == null) return;

            var cardOwnProductWindow = new CardOwnProductWindow(ownProduct.Id, ownProduct.Code, ownProduct.Route);
            cardOwnProductWindow.ShowDialog();

            ownProduct.Count = cardOwnProductWindow.CardOwnProduct.Count;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var items = treeTreeListView.SelectedItems.OfType<TreeProduct>();
            if(items.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            var roots = items.Where(x => x.ParentId == null).ToList();

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 4))) ||
                (card.IsDepartment5Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 5))) ||
                (card.IsDepartment6Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 6))) ||
                (card.IsDepartment13Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 13))) ||
                (card.IsDepartment17Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 17))) ||
                (card.IsDepartment80Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 80))) ||
                (card.IsDepartment82Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 82))))
            {
                MessageBox.Show("В некоторых из удаляемых продуктов есть ремонтные операции, по цеху которых ООИОТ поставили утверждение");
                return;
            }

            roots.ForEach(x => _cardProductRepo.DeleteRecursively(x.Id));

            Refresh();
        }


        private void AddProductEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            var ownProductChooseProductEntriesWindow = new OwnProductSelectProductEntriesWindow(_cardId);
            ownProductChooseProductEntriesWindow.ShowDialog();
            Refresh();
        }

        private void addProductEntriesFromTreeButton_Click(object sender, RoutedEventArgs e)
        {
            new OwnProductSelectProductEntriesFromTreeWindow(_cardId).ShowDialog();
            Refresh();
        }

        private void addProductFromAnotherCardButton_Click(object sender, RoutedEventArgs e)
        {
            new OwnProductSelectFromAnotherCardWindow(_cardId).ShowDialog();
            Refresh();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => Open();

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void treeTreeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Open();

        private void exportToPDMButton_Click(object sender, RoutedEventArgs e)
        {
            var card = _cardRepo.Get(_cardId);

            if (card.Order == null)
            {
                MessageBox.Show("В карте ремонта не указан заказ");
                return;
            }

            if (!card.ProductCode.Contains("/"))
            {
                MessageBox.Show("Указанный в карте децимальный номер не является ремонтным");
                return;
            }

            var products = _cardProductRepo.GetAllByCard(card.Id);
            if (products.Any(x => x.Code == card.ProductCode))
            {
                MessageBox.Show("Указанный в карте децимальный номер также есть в списке ДСЕ на восполнение, поэтому экспорт невозможен");
                return;
            }

            var isPlannedOrder = _utilsRepo.IsPlannedOrder(card.Order);
            if (isPlannedOrder)
            {
                MessageBox.Show("Еcть дефицит на этот заказ в текущем году. Свяжитесь с ПДО по вопросу удаления дефицита");
                return;
            }

            var isExistingOrderInPDM = _utilsRepo.IsExistingOrderInPDM(card.Order);
            if (isExistingOrderInPDM)
            {
                var dialog = MessageBox.Show("Есть ВПР на этот заказ. Хотите перезаписать (все данные по уже созданной ВПР будут потеряны)?", "Внимание", MessageBoxButton.YesNo);

                if (dialog == MessageBoxResult.Yes)
                {
                    try
                    {
                        _utilsRepo.DeleteOrderInPDM(card);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                else return;
            }

            _utilsRepo.ExportToPDM(card);

            if (_cardProductRepo.AreThereProductsWithChangedComposition(card))
                MessageBox.Show("Готово. В ВПР перенесены все ДСЕ, кроме помеченных \"Изменено\". Помеченные нужно перенести вручную");
            else
                MessageBox.Show("Готово");

        }

        private void treeTreeListView_CopyingCellClipboardContent(object sender, Telerik.Windows.Controls.GridViewCellClipboardEventArgs e)
        {
            if (e.Cell != treeTreeListView.CurrentCellInfo) 
                e.Cancel = true;
        }

        private void isOvercoatingRequiredButton_Click(object sender, RoutedEventArgs e)
        {
            var items = treeTreeListView.SelectedItems.OfType<TreeProduct>().ToList();
            if (items.Count == 0) return;

            items.ForEach(x => _cardProductRepo.MarkIfOvercoatingRequired(x.Id));

            Refresh();
        }
    }
}
