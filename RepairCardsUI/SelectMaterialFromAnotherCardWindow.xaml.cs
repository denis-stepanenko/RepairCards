using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectMaterialFromAnotherCardWindow : Window
    {
        private readonly CardMaterialRepo _repo = new CardMaterialRepo();

        private readonly int _cardId;
        private Card _card;

        public SelectMaterialFromAnotherCardWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
        }

        void Refresh()
        {
            var items = _repo.GetAllByCard(_card.Id);

            itemsRadGridView.ItemsSource = items;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<CardMaterial>().ToList();
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                item.CardId = _cardId;
                _repo.Add(item);
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
