using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class RepairProductSelectProductEntriesWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly ProductRelationRepo _productEntryRepo = new ProductRelationRepo();
        private readonly CardRepairProductRepo _cardRepairProductRepo = new CardRepairProductRepo();

        private readonly int _cardId;

        public RepairProductSelectProductEntriesWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;

            Refresh();
        }

        void Refresh()
        {
            var card = _cardRepo.Get(_cardId);
            var items = _productEntryRepo.GetProductEntries((int)card.ProductId, 2, 0);
            itemsRadGridView.ItemsSource = items;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<ProductRelation>().ToList();
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                var newProduct = new CardRepairProduct
                {
                    CardId = _cardId,
                    Code = item.Code,
                    Name = item.Name,
                    Count = (int)item.CountAll
                };

                _cardRepairProductRepo.Add(newProduct);
            }

            Close();
        }
    }
}
