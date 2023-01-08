using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class OwnProductSelectProductEntriesWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly ProductRelationRepo _productEntryRepo = new ProductRelationRepo();
        private readonly CardOwnProductRepo _cardOwnProductRepo = new CardOwnProductRepo();
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly CardOwnProductOperationRepo _cardOwnProductOperationRepo = new CardOwnProductOperationRepo();

        private readonly int _cardId;

        public OwnProductSelectProductEntriesWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;

            Refresh();
        }

        void Refresh()
        {
            var card = _cardRepo.Get(_cardId);
            var productEntries = _productEntryRepo.GetProductEntries((int)card.ProductId, 2, 0);
            productEntriesRadGridView.ItemsSource = productEntries;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var productEntries = productEntriesRadGridView.SelectedItems.Cast<ProductRelation>().ToList();
            if (productEntries.Count == 0) return;

            foreach (var productEntry in productEntries)
            {
                var newCardOwnProduct = new CardOwnProduct
                {
                    CardId = _cardId,
                    Code = productEntry.Code,
                    Name = productEntry.Name,
                    Count = (int)productEntry.CountAll,
                    Route = productEntry.Route
                };

                int id = _cardOwnProductRepo.Add(newCardOwnProduct);

                var operations = _productOperationRepo.GetProductOperations(productEntry.Code);

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
            }

            Close();
        }
    }
}
