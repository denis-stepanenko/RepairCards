using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;
using System.Windows.Input;

namespace RepairCardsUI
{
    public partial class SelectCardWindow : Window
    {
        private readonly CardRepo _repo = new CardRepo();

        public SelectCardWindow()
        {
            InitializeComponent();
            Refresh();
        }

        public Card Card { get; set; }

        void Refresh()
        {
            var cards = _repo.GetAll();
            cardsRadGridView.ItemsSource = cards;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var card = (Card)cardsRadGridView.SelectedItem;
            if (card != null)
            {
                Card = card;
                DialogResult = true;
            }
        }

        private void cardsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AcceptButton_Click(null, null);
        }
    }
}
