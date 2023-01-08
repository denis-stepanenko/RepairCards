using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class UsersPage : Page
    {
        private readonly UserRepo _repo = new UserRepo();

        public UsersPage()
        {
            InitializeComponent();

            if (!new[] { 2, 4 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var items = _repo.GetAll();
            itemsRadGridView.ItemsSource = items;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow(null).ShowDialog();
            Refresh();
        }

        void Edit()
        {
            var item = (User)itemsRadGridView.SelectedItem;
            if (item != null)
            {
                new UserWindow(item.Id).ShowDialog();
                Refresh();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void itemsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<User>().ToList();

            if (items.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            items.ForEach(x => _repo.Delete(x.Id));

            Refresh();
        }
        
    }
}
