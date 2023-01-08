using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PurchasedProductSelectProductEntriesWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly ProductRepo _productRepo = new ProductRepo();
        private readonly ProductRelationRepo _productEntryRepo = new ProductRelationRepo();
        private readonly CardPurchasedProductRepo _cardPurchasedProductRepo = new CardPurchasedProductRepo();
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly CardPurchasedProductOperationRepo _cardPurchasedProductOperationRepo = new CardPurchasedProductOperationRepo();

        private readonly int _cardId;

        public PurchasedProductSelectProductEntriesWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;

            Refresh();
        }

        void Refresh()
        {
            var products = _productRepo.GetAll();
            productsRadDataPager.Source = products;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var productEntries = productEntriesRadGridView.SelectedItems.Cast<ProductRelation>().ToList();
            if (productEntries.Count == 0) return;

            foreach (var productEntry in productEntries)
            {
                var newCardPurchasedProduct = new CardPurchasedProduct
                {
                    CardId = _cardId,
                    Code = productEntry.Code,
                    Name = productEntry.Name,
                    Count = (int)productEntry.CountAllWithoutWaste
                };

                int id = _cardPurchasedProductRepo.Add(newCardPurchasedProduct);

                var operations = _productOperationRepo.GetProductOperations(productEntry.Code);

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
            }

            Close();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            var product = productsRadGridView.SelectedItem as Product;

            if (product == null) return;

            var card = _cardRepo.Get(_cardId);
            var productEntries = _productEntryRepo.GetProductEntries(product.Id, product.TableId, 1);
            productEntriesRadGridView.ItemsSource = productEntries;
        }
    }
}
