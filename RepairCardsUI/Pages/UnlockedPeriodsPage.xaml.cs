using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class UnlockedPeriodsPage : Page
    {
        private readonly UnlockedPeriodRepo _repo = new UnlockedPeriodRepo();

        public UnlockedPeriodsPage()
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
            var executors = _repo.GetAll();
            itemsRadGridView.ItemsSource = executors;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Edit()
        {
            var period = (UnlockedPeriod)itemsRadGridView.SelectedItem;
            if (period == null) return;

            var w = new UnlockedPeriodWindow(period.Id);
            w.ShowDialog();
            Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void executorsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<UnlockedPeriod>().ToList();

            if (items.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            items.ForEach(x => _repo.Remove(x));

            Refresh();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new UnlockedPeriodWindow(null);
            w.ShowDialog();
            Refresh();
        }
    }
}
