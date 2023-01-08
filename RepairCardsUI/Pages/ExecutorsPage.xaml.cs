using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class ExecutorsPage : Page
    {
        private readonly ExecutorRepo _repo = new ExecutorRepo();

        public ExecutorsPage()
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
            executorsRadGridView.ItemsSource = executors;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Edit()
        {
            var executor = (Executor)executorsRadGridView.SelectedItem;
            if (executor != null)
            {
                var executorWindow = new ExecutorWindow(executor.Id);
                executorWindow.ShowDialog();
                Refresh();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void executorsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var executors = executorsRadGridView.SelectedItems.Cast<Executor>().ToList();

            if (executors.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (executors.Any(x => _repo.IsExecuterAlreadyUsedInOperations(x.Id)))
            {
                MessageBox.Show("Невозможно удалить исполнителя, потому что он указан у операций в картах ремонта");
                return;
            }

            foreach (var executor in executors)
            {
                _repo.Delete(executor.Id);
            }

            Refresh();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var executorWindow = new ExecutorWindow(null);
            executorWindow.ShowDialog();
            Refresh();
        }
    }
}
