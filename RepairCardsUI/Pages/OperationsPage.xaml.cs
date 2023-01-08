using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class OperationsPage : Page
    {
        private readonly OperationRepo _repo = new OperationRepo();

        public OperationsPage()
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
            var operations = _repo.GetAll();
            operationsRadGridView.ItemsSource = operations;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Edit()
        {
            var operation = (Operation)operationsRadGridView.SelectedItem;
            if (operation != null)
            {
                var operationWindow = new OperationWindow(operation.Id);
                operationWindow.ShowDialog();
                Refresh();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void operationsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var executors = operationsRadGridView.SelectedItems.Cast<Operation>().ToList();
            if (executors.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (executors.Any(x => _repo.IsOperationAlreadyUsedInTemplates(x.Id)))
            {
                MessageBox.Show("Невозможно удалить операцию, потому что она используется в шаблонах");
                return;
            }

            executors.ForEach(x => _repo.Delete(x.Id));

            Refresh();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var operationWindow = new OperationWindow(null);
            operationWindow.ShowDialog();
            Refresh();
        }
    }
}
