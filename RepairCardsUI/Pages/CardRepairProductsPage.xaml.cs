using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class CardRepairProductsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardRepairProductRepo _cardRepairProductRepo = new CardRepairProductRepo();

        private readonly int _cardId;

        public CardRepairProductsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                addProductEntriesButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var items = _cardRepairProductRepo.GetAll(_cardId);
            itemsRadGridView.ItemsSource = items;
            bool isConfirmed = _cardRepo.IsConfirmed(_cardId, 1);

            if (isConfirmed)
            {
                addButton.Visibility = Visibility.Collapsed;
                addProductEntriesButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseCardProductWindow = new SelectCardProductWindow();
            if (chooseCardProductWindow.ShowDialog() != true) return;

            int id = _cardRepairProductRepo.Add(new CardRepairProduct
            {
                CardId = _cardId,
                Code = chooseCardProductWindow.Product.Code,
                Name = chooseCardProductWindow.Product.Name,
                Count = chooseCardProductWindow.Count
            });

            Refresh();
        }

        private void AddProductEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            new RepairProductSelectProductEntriesWindow(_cardId).ShowDialog();
            Refresh();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (CardRepairProduct)itemsRadGridView.SelectedItem;
            if (item == null) return;

            new CardRepairProductWindow(item.Id, item.Code).ShowDialog();

            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<CardRepairProduct>().ToList();

            if (items.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            foreach (var item in items)
            {
                _cardRepairProductRepo.Delete(item.Id);
            }

            Refresh();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();
    }
}
