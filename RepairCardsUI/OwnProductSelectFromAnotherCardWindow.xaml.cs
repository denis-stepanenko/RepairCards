using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class OwnProductSelectFromAnotherCardWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOwnProductRepo _cardOwnProductRepo = new CardOwnProductRepo();
        private readonly CardOwnProductOperationRepo _cardOwnProductOperationRepo = new CardOwnProductOperationRepo();
        private readonly CardOwnProductRepairOperationRepo _cardOwnProductRepairOperationRepo = new CardOwnProductRepairOperationRepo();
        private readonly CardOwnProductRepo _cardProductRepo = new CardOwnProductRepo();

        private readonly int _cardId;
        private Card _card;

        public OwnProductSelectFromAnotherCardWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
        }

        void Refresh()
        {
            var products = _cardProductRepo.GetAllByCard(_card.Id);
            var trees = GetTreeProducts(products.ToList());
            treeTreeListView.ItemsSource = trees;
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
                            HasChangedComposition = x.HasChangedComposition
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

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var items = treeTreeListView.SelectedItems.OfType<TreeProduct>().ToList();

            var roots = items.Where(x => x.ParentId == null).ToList();

            if (roots.Count == 0) return;

            if (!roots.Any(x => _cardOwnProductRepo.IsProductAddedAfterAddingFunctionalityForHierarchicalStorageOfProducts(x.Id)))
            {
                MessageBox.Show("Некоторые выбранные ДСЕ были добавлены до доработки (02.06.2022) по хранению ДСЕ в виде иерархии, поэтому их невозможно добавить");
                return;
            }

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 4))) ||
                (card.IsDepartment5Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 5))) ||
                (card.IsDepartment6Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 6))) ||
                (card.IsDepartment13Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 13))) ||
                (card.IsDepartment17Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 17))) ||
                (card.IsDepartment80Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 80))) ||
                (card.IsDepartment82Confirmed && roots.Any(x => _cardProductRepo.AreThereChildProductsWithRepairOperationsWithDepartment(x.Id, 82))))
            {
                MessageBox.Show("В некоторых из выбранных продуктов есть ремонтные операции, по цеху которых ООИОТ поставили утверждение");
                return;
            }

            roots.ForEach(x => AddRecursively(_cardId, x, null));

            Close();
        }

        void AddRecursively(int cardId, TreeProduct item, int? id)
        {
            int newProductId = _cardOwnProductRepo.Add(new CardOwnProduct
            {
                CardId = cardId,
                Code = item.Code,
                Name = item.Name,
                Count = (int)item.Count,
                Route = item.Route,
                HasChangedComposition = item.HasChangedComposition,
                ParentId = id
            });

            // Производственные операции
            var productOperations = _cardOwnProductOperationRepo.GetAllByProduct(item.Id);
            foreach (var operation in productOperations)
            {
                operation.CardOwnProductId = newProductId;
                _cardOwnProductOperationRepo.Add(operation);
            }

            // Ремонтные операции
            var productRepairOperations = _cardOwnProductRepairOperationRepo.GetAll(item.Id);
            foreach (var operation in productRepairOperations)
            {
                operation.CardOwnProductId = newProductId;
                _cardOwnProductRepairOperationRepo.Add(operation);
            }

            foreach (var child in item.Children)
            {
                AddRecursively(cardId, child, newProductId);
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_card == null) return;

            Refresh();
        }

        private void cardSelectControl_Click()
        {
            var chooseCardWindow = new SelectCardWindow();
            if (chooseCardWindow.ShowDialog() == true)
            {
                _card = chooseCardWindow.Card;
                cardSelectControl.Text = $"{_card.Number}, {_card.ProductCode}";
            }
        }
    }
}
