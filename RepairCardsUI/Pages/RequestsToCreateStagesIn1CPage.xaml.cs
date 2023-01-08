using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class RequestsToCreateStagesIn1CPage : Page
    {
        private readonly RequestToCreateStagesIn1CRepo _repo = new RequestToCreateStagesIn1CRepo();
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();

        private ServerFilteringHelper _filteringHelper;

        public RequestsToCreateStagesIn1CPage()
        {
            InitializeComponent();

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.IsEnabled = false;
                removeButton.IsEnabled = false;
            }

            if (AuthorizationService.User.Department != 40)
            {
                closeRequestButton.IsEnabled = false;
                cancelRequestClosingButton.IsEnabled = false;
            }

            _filteringHelper = new ServerFilteringHelper(RefreshCards);

            RefreshCards();

            Refresh();
        }

        void RefreshCards()
        {
            var items = _cardRepo.GetAll(filterTextBox.Text);
            cardsDataGrid.ItemsSource = items;
        }

        void Refresh()
        {
            var items = _repo.GetAll();
            itemsRadGridView.ItemsSource = items;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var items = cardsDataGrid.SelectedItems.OfType<Card>().ToList();
            if(items.Count == 0) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (items.Any(x => x.Department != AuthorizationService.User.Department))
                {
                    MessageBox.Show("Среди выбранных карт есть те, которые не принадлежат вашему цеху.");
                    return;
                }

            if(items.Any(x => _repo.AreThereRequestsWithCardNumber(x.Number)))
            {
                MessageBox.Show("Среди выбранных карт есть те, на которые уже созданы заявки");
                return;
            }

            var user = AuthorizationService.User;
            var date = _utilsRepo.GetServerDate();

            var newItems = items.Select(x =>
            new RequestToCreateStagesIn1C
            {
                CardNumber = x.Number,
                Department = x.Department,
                Date = date,
                CustomerName = user.Name,
            }).ToList();

            newItems.ForEach(x => _repo.Add(x));

            Refresh();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<RequestToCreateStagesIn1C>().ToList();
            if (items.Count == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (items.Any(x => x.Department != AuthorizationService.User.Department))
                {
                    MessageBox.Show("Удаление невозможно, потому что среди удаляемых записей есть те, которые не принадлежат вашему подразделению.");
                    return;
                }

            items.ForEach(x => _repo.Remove(x));

            Refresh();
        }

        private void closeRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<RequestToCreateStagesIn1C>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;
            var date = _utilsRepo.GetServerDate();

            items.ForEach(x => _repo.Confirm(x.Id, user.Name, date));

            Refresh();
        }

        private void cancelRequestClosingButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<RequestToCreateStagesIn1C>().ToList();
            if (items.Count == 0) return;

            items.ForEach(x => _repo.Confirm(x.Id, null, null));

            Refresh();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();
        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e) => _filteringHelper.Run();
    }
}
