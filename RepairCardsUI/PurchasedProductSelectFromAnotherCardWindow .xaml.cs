using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PurchasedProductSelectFromAnotherCardWindow : Window
    {
        private readonly CardPurchasedProductRepo _cardPurchasedProductRepo = new CardPurchasedProductRepo();
        private readonly CardPurchasedProductOperationRepo _cardPurchasedProductOperationRepo = new CardPurchasedProductOperationRepo();

        private readonly int _cardId;
        private Card _card;

        public PurchasedProductSelectFromAnotherCardWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
        }

        void Refresh()
        {
            var items = _cardPurchasedProductRepo.GetAllByCard(_card.Id);
            itemsRadGridView.ItemsSource = items;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<CardPurchasedProduct>().ToList();
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                item.CardId = _cardId;
                int newProductId = _cardPurchasedProductRepo.Add(item);

                // Производственные операции
                var productionOperations = _cardPurchasedProductOperationRepo.GetAllByProduct(item.Id);
                foreach (var operation in productionOperations)
                {
                    operation.CardPurchasedProductId = newProductId;
                    _cardPurchasedProductOperationRepo.Add(operation);
                }
            }

            Close();
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
