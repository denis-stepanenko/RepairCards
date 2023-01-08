using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class CardPurchasedProductsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardPurchasedProductRepo _cardProductRepo = new CardPurchasedProductRepo();
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly CardPurchasedProductOperationRepo _cardPurchasedProductOperationRepo = new CardPurchasedProductOperationRepo();

        private readonly int _cardId;

        public CardPurchasedProductsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                addProductEntriesButton.Visibility = Visibility.Collapsed;
                addProductFromAnotherCardButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var products = _cardProductRepo.GetAllByCard(_cardId);
            productsRadGridView.ItemsSource = products;
            bool isConfirmed = _cardRepo.IsConfirmed(_cardId, 1);

            if (isConfirmed)
            {
                addButton.Visibility = Visibility.Collapsed;
                addProductEntriesButton.Visibility = Visibility.Collapsed;
                addProductFromAnotherCardButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseCardProductWindow = new SelectCardProductWindow();
            if (chooseCardProductWindow.ShowDialog() != true) return;

            int id = _cardProductRepo.Add(new CardPurchasedProduct
            {
                CardId = _cardId,
                Code = chooseCardProductWindow.Product.Code,
                Name = chooseCardProductWindow.Product.Name,
                Count = chooseCardProductWindow.Count
            });

            var operations = _productOperationRepo.GetProductOperations(chooseCardProductWindow.Product.Code);

            foreach (var operation in operations)
            {
                var newCardPurchasedProductOperation = new CardPurchasedProductOperation
                {
                    CardPurchasedProductId = id,
                    Code = operation.Code,
                    Name = operation.Name,
                    Labor = operation.Labor,
                    Type = 0,
                    Department = operation.Department
                };

                _cardPurchasedProductOperationRepo.Add(newCardPurchasedProductOperation);
            }

            Refresh();
        }

        private void AddProductEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            var purchasedProductChooseProductEntriesWindow = new PurchasedProductSelectProductEntriesWindow(_cardId);
            purchasedProductChooseProductEntriesWindow.ShowDialog();
            Refresh();
        }

        private void addProductFromAnotherCardButton_Click(object sender, RoutedEventArgs e)
        {
            new PurchasedProductSelectFromAnotherCardWindow(_cardId).ShowDialog();
            Refresh();
        }

        void Open()
        {
            var product = (CardPurchasedProduct)productsRadGridView.SelectedItem;
            if (product == null) return;

            var cardPurchasedProductWindow = new CardPurchasedProductWindow(product.Id, product.Code);
            cardPurchasedProductWindow.ShowDialog();

            Refresh();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => Open();
        private void productsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Open();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var products = productsRadGridView.SelectedItems.Cast<CardPurchasedProduct>().ToList();

            if (products.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            foreach (var product in products)
            {
                _cardProductRepo.Delete(product.Id);
            }

            Refresh();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void productsRadGridView_CopyingCellClipboardContent(object sender, Telerik.Windows.Controls.GridViewCellClipboardEventArgs e)
        {
            if (e.Cell != productsRadGridView.CurrentCellInfo)
                e.Cancel = true;
        }
    }
}
