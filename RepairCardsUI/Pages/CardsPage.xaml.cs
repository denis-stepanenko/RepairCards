using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class CardsPage : Page
    {
        private readonly CardRepo _repo = new CardRepo();
        private readonly CardDetailsRepo _cardDetailsRepo = new CardDetailsRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly CardMaterialRepo _cardMaterialRepo = new CardMaterialRepo();
        private readonly CardDocumentRepo _cardDocumentRepo = new CardDocumentRepo();
        private readonly CardOwnProductRepo _cardOwnProductRepo = new CardOwnProductRepo();
        private readonly CardOwnProductOperationRepo _cardOwnProductOperationRepo = new CardOwnProductOperationRepo();
        private readonly CardOwnProductRepairOperationRepo _cardOwnProductRepairOperationRepo = new CardOwnProductRepairOperationRepo();
        private readonly CardPurchasedProductRepo _cardPurchasedProductRepo = new CardPurchasedProductRepo();
        private readonly CardPurchasedProductOperationRepo _cardPurchasedProductOperationRepo = new CardPurchasedProductOperationRepo();

        public CardsPage()
        {
            InitializeComponent();

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var cards = _repo.GetAll();
            cardsRadGridView.ItemsSource = cards;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Open()
        {
            var card = (Card)cardsRadGridView.SelectedItem;
            if (card != null)
            {
                var cardWindow = new CardWindow(card.Id);
                cardWindow.ShowDialog();
                Refresh();
            }
        }

        private void openButton_Click(object sender, RoutedEventArgs e) => Open();
        private void cardsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Open();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var items = cardsRadGridView.SelectedItems.Cast<Card>().ToList();
            if (items.Count == 0) return;

            if (items.Any(x => _repo.AreThereChildProductCards(x)))
            {
                MessageBox.Show("Невозможно удалить одну из выбранных карт, потому что в ней указаны демонтированные или установленные карты ремонта");
                return;
            }

            foreach (var item in items)
            {
                var parentCard = _repo.GetParentForDismantledProductCard(item);
                if (parentCard != null)
                {
                    MessageBox.Show($"Удаление невозможно, потому что карта {item.Number} входит в {parentCard.Number} в качестве демонтированной.");
                    return;
                }
            }

            foreach (var item in items)
            {
                var parentCard = _repo.GetParentForInstalledProductCard(item);
                if (parentCard != null)
                {
                    MessageBox.Show($"Удаление невозможно, потому что карта {item.Number} входит в {parentCard.Number} в качестве установленной.");
                    return;
                }
            }

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (items.Any(x => x.Department != AuthorizationService.User.Department))
                {
                    MessageBox.Show("Удаление карт ремонта невозможно, потому что среди удаляемых карт есть карты, не принадлежащие вашему цеху.");
                    return;
                }

            items.ForEach(x => _repo.Delete(x.Id));

            Refresh();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var newCardWindow = new NewCardWindow();
            newCardWindow.ShowDialog();
            Refresh();
        }

        private void duplicateButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (Card)cardsRadGridView.SelectedItem;
            if (item == null) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (item.Department != AuthorizationService.User.Department)
                {
                    MessageBox.Show("Дублирование карты невозможно, потому что она не принадлежит вашему цеху.");
                    return;
                }

            string number = _repo.GetNewNumber(AuthorizationService.User.Department);


            Duplicate(item.Id, number);


            Refresh();
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

        void Duplicate(int id, string newNumber)
        {
            // Карта
            var card = _repo.Get(id);
            card.Number = newNumber;
            card.ParentId = null;
            card.ParentId2 = null;
            card.IsDepartment4Confirmed = false;
            card.IsDepartment5Confirmed = false;
            card.IsDepartment6Confirmed = false;
            card.IsDepartment13Confirmed = false;
            card.IsDepartment17Confirmed = false;
            card.IsDepartment80Confirmed = false;
            card.IsDepartment82Confirmed = false;
            card.CardStatusId = 1;

            int newCardId = _repo.Add(card, AuthorizationService.User);

            // Дефектация и неисправности
            var newCardDetails = _cardDetailsRepo.Get(newCardId);
            var cardDetails = _cardDetailsRepo.Get(id);
            cardDetails.Id = newCardId;
            _cardDetailsRepo.Update(cardDetails);

            // Операции
            var operations = _cardOperationRepo.GetAllByCard(id);
            foreach (var operation in operations)
            {
                operation.CardId = newCardId;
                _cardOperationRepo.Add(operation);
            }

            // ДСЕ
            var products = _cardOwnProductRepo.GetAllByCard(id);

            var tries = GetTreeProducts(products.ToList());

            tries.ForEach(x => AddRecursively(newCardId, x, null));

            // ПКИ
            var purchasedProducts = _cardPurchasedProductRepo.GetAllByCard(id);
            foreach (var product in purchasedProducts)
            {
                product.CardId = newCardId;
                var newProductId = _cardPurchasedProductRepo.Add(product);

                var productOperations = _cardPurchasedProductOperationRepo.GetAllByProduct(product.Id);
                foreach (var operation in productOperations)
                {
                    operation.CardPurchasedProductId = newProductId;
                    _cardPurchasedProductOperationRepo.Add(operation);
                }
            }

            // Материалы
            var materials = _cardMaterialRepo.GetAllByCard(id);
            foreach (var material in materials)
            {
                material.CardId = newCardId;
                _cardMaterialRepo.Add(material);
            }

            // Документы
            var documents = _cardDocumentRepo.GetAllByCard(id);
            foreach (var document in documents)
            {
                document.CardId = newCardId;
                _cardDocumentRepo.Add(document);
            }
        }

    }
}
