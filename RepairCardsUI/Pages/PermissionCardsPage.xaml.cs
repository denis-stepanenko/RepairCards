using RepairCardsUI.Data;
using RepairCardsUI.Infrastructure;
using RepairCardsUI.Models;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class PermissionCardsPage : Page
    {
        private EFContext _db;

        public PermissionCardsPage()
        {
            InitializeComponent();

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
                duplicateButton.IsEnabled = false;
            }

            if (!new[] { 2, 6 }.Contains(AuthorizationService.User.RoleId))
            {
                confirmButton.IsEnabled = false;
            }

            Refresh();
        }

        void Refresh()
        {
            _db = new EFContext();
            _db.PermissionCards.Load();
            itemsRadGridView.ItemsSource = _db.PermissionCards.Local;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Open()
        {
            var item = itemsRadGridView.SelectedItem as PermissionCard;
            if (item == null) return;

            new PermissionCardWindow(item, _db).ShowDialog();
        }

        private void openButton_Click(object sender, RoutedEventArgs e) => Open();
        private void cardsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Open();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            var items = itemsRadGridView.SelectedItems.OfType<PermissionCard>();
            _db.PermissionCards.RemoveRange(items);
            _db.SaveChanges();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new PermissionCardWindow(null, _db).ShowDialog();
        }

        private void duplicateButton_Click(object sender, RoutedEventArgs e)
        {
            var item = itemsRadGridView.SelectedItem as PermissionCard;
            if (item == null) return;

            int department = AuthorizationService.User.Department;
            var newNumber = _db.GetNewNumber(department);

            var card = _db.PermissionCards
                .AsNoTracking()
                .Include(x => x.Products)
                .Include(x => x.PurchasedProducts)
                .Include(x => x.Materials)
                .Include(x => x.Operations)
                .FirstOrDefault(x => x.Id == item.Id);

            card.Number = newNumber;

            _db.PermissionCards.Add(card);

            _db.SaveChanges();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var item = itemsRadGridView.SelectedItem as PermissionCard;
            if (item == null) return;

            var user = AuthorizationService.User;

            if (_db.PermissionCards.Any(x => x.Id == item.Id && x.OTKUserId == null))
            {
                item.OTKUserId = user.Id;
                item.OTKUsername = user.Name;
            }

            if (_db.PermissionCards.Any(x => x.Id == item.Id && x.OTKUserId == user.Id))
            {
                item.OTKUserId = null;
                item.OTKUsername = null;
            }

            _db.SaveChanges();
        }
    }
}
