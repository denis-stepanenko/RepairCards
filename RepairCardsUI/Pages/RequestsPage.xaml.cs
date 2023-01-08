using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class RequestsPage : Page
    {
        private readonly RequestRepo _repo = new RequestRepo();

        public RequestsPage()
        {
            InitializeComponent();

            if (new[] { 1, 6, 9 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            if (AuthorizationService.User.RoleId == 8)
            {
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var requests = _repo.GetAll();
            requestsRadGridView.ItemsSource = requests;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Edit()
        {
            var request = (Request)requestsRadGridView.SelectedItem;
            if (request == null) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (request.Department != AuthorizationService.User.Department)
                {
                    MessageBox.Show("Изменение заявки невозможно, потому что заявка не принадлежит вашему цеху");
                    return;
                }

            var requestWindow = new RequestWindow(request.Id);
            requestWindow.ShowDialog();
            Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void requestsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var requests = requestsRadGridView.SelectedItems.Cast<Request>().ToList();

            if (requests.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (requests.Any(x => x.Department != AuthorizationService.User.Department))
                {
                    MessageBox.Show("Удаление заявок невозможно, потому что среди удаляемых заявок есть заявки, не принадлежащие вашему цеху");
                    return;
                }

            requests.ForEach(x => _repo.Delete(x.Id));

            Refresh();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var requestWindow = new RequestWindow(null);
            requestWindow.ShowDialog();
            Refresh();
        }
    }
}