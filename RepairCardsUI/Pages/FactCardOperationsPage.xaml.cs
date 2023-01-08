using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class FactCardOperationsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();
        private readonly ExecutorRepo _executorRepo = new ExecutorRepo();

        private readonly int _cardId;

        public FactCardOperationsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                addFromPlanButton.Visibility = Visibility.Collapsed;
                addFromTemplateButton.Visibility = Visibility.Collapsed;
                addFromAnotherCardButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                changeExecutorButton.IsEnabled = false;
                changeDateButton.IsEnabled = false;
            }

            if (AuthorizationService.User.RoleId != 6)
            {
                confirmButton.Visibility = Visibility.Collapsed;
                unconfirmButton.Visibility = Visibility.Collapsed;
            }

            var executors = new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department) ?
                _executorRepo.GetAllByDepartment(AuthorizationService.User.Department) :
                _executorRepo.GetAll();

            executorsComboBox.ItemsSource = executors;

            Refresh();
        }

        void Refresh()
        {

            if (_cardRepo.IsConfirmed(_cardId, 1) || _cardRepo.IsConfirmed(_cardId, 5))
            {
                addButton.Visibility = Visibility.Collapsed;
                addFromPlanButton.Visibility = Visibility.Collapsed;
                addFromTemplateButton.Visibility = Visibility.Collapsed;
                addFromAnotherCardButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                changeExecutorButton.IsEnabled = false;
                changeDateButton.IsEnabled = false;
            }

            var operations = _cardOperationRepo.GetAllByCardAndType(_cardId, 1);
            operationsRadGridView.ItemsSource = operations;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var factChooseOperationWindow = new FactSelectOperationWindow(_cardId);
            factChooseOperationWindow.ShowDialog();
            Refresh();
        }

        private void AddFromTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var factChooseOperationFromTemplateWindow = new FactSelectOperationFromTemplateWindow(_cardId);
            factChooseOperationFromTemplateWindow.ShowDialog();
            Refresh();
        }

        private void AddFromPlanButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseOperationFromPlanWindow = new SelectOperationFromPlanWindow(_cardId);
            chooseOperationFromPlanWindow.ShowDialog();
            Refresh();
        }

        private void addFromAnotherCardButton_Click(object sender, RoutedEventArgs e)
        {
            new SelectFactOperationFromAnotherCardWindow(_cardId).ShowDialog();
            Refresh();
        }

        void Edit()
        {
            var operation = (CardOperation)operationsRadGridView.SelectedItem;
            if (operation != null)
            {
                if (_cardOperationRepo.IsConfirmed(operation))
                {
                    MessageBox.Show("Операция утверждена ОТК. Невозможно ее редактировать");
                    return;
                }

                var card = _cardRepo.Get(_cardId);

                if ((card.IsDepartment4Confirmed && operation.Department == 4) ||
                    (card.IsDepartment5Confirmed && operation.Department == 5) ||
                    (card.IsDepartment6Confirmed && operation.Department == 6) ||
                    (card.IsDepartment13Confirmed && operation.Department == 13) ||
                    (card.IsDepartment17Confirmed && operation.Department == 17) ||
                    (card.IsDepartment80Confirmed && operation.Department == 80) ||
                    (card.IsDepartment82Confirmed && operation.Department == 82))
                {
                    MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь изменить");
                    return;
                }

                if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                    if (operation.Department != AuthorizationService.User.Department)
                    {
                        MessageBox.Show("Изменение операции невозможно, потому что операция не принадлежит вашему цеху");
                        return;
                    }

                var factCardOperationWindow = new FactCardOperationWindow(operation.Id);
                factCardOperationWindow.ShowDialog();
                Refresh();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void operationsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<CardOperation>().ToList();

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Department == 82)))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь удалить");
                return;
            }


            if (operations.Count() == 0) return;

            if (operations.Any(x => _cardOperationRepo.IsConfirmed(x)))
            {
                MessageBox.Show("Одна из операций утверждена ОТК. Невозможно ее редактировать");
                return;
            }

            // Проверка периода
            var date = _utilsRepo.GetServerDate();
            foreach (var item in operations)
            {
                if (item.Date < new DateTime(date.Year, date.Month, 1))
                {
                    bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(item.Date.Value.Year, item.Date.Value.Month, _cardId);
                    if (!isUnlockedPeriod)
                    {
                        MessageBox.Show("Среди удаляемых операций есть операции из завершенного месяца");
                        return;
                    }
                }
            }

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (operations.Any(x => x.Department != AuthorizationService.User.Department))
                {
                    MessageBox.Show("Удаление операций невозможно, потому что среди удаляемых операций есть операции, не принадлежащие вашему цеху");
                    return;
                }

            operations.ForEach(x => _cardOperationRepo.Delete(x.Id));

            Refresh();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsRadGridView.SelectedItems.Cast<CardOperation>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;

            foreach (var item in items)
            {
                var operation = _cardOperationRepo.Get(item.Id);
                if (operation.IsConfirmed)
                    continue;

                _cardOperationRepo.Confirm(item, user.Id, user.Name);
            }

            Refresh();
        }

        private void unconfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsRadGridView.SelectedItems.Cast<CardOperation>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;

            foreach (var item in items)
            {
                var operation = _cardOperationRepo.Get(item.Id);
                if (operation.IsConfirmed)
                {
                    if (operation.ConfirmUserId != user.Id)
                        continue;
                }

                _cardOperationRepo.Unconfirm(item);
            }

            Refresh();
        }

        private void changeExecutorButton_Click(object sender, RoutedEventArgs e)
        {
            var executor = executorsComboBox.SelectedItem as Executor;
            if (executor == null) return;

            var operations = operationsRadGridView.SelectedItems.Cast<CardOperation>().ToList();
            if (operations.Count == 0) return;

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Department == 82)))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь удалить");
                return;
            }

            if (!operations.Any(x => x.Department == executor.Department))
            {
                MessageBox.Show("Не у каждой операции цех совпадает с цехом исполнителя");
                return;
            }

            var currentDate = _utilsRepo.GetServerDate();

            if (operations.Any(x => x.Date < new DateTime(currentDate.Year, currentDate.Month, 1) &&
            !_unlockedPeriodRepo.IsUnlockedPeriod(x.Date.Value.Year, x.Date.Value.Month, x.CardId)))
            {
                MessageBox.Show("Вы не можете работать с закрытым месяцем. Одна из операций находится в закрытом месяце");
                return;
            }

            operations.ForEach(x => x.ExecutorId = executor.Id);
            operations.ForEach(x => _cardOperationRepo.Update(x));

            Refresh();
        }

        private void changeDateButton_Click(object sender, RoutedEventArgs e)
        {
            var newDate = dateDateTimePicker.SelectedDate;
            if (newDate == null) return;

            var operations = operationsRadGridView.SelectedItems.Cast<CardOperation>().ToList();
            if (operations.Count == 0) return;

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Department == 82)))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь удалить");
                return;
            }

            var currentDate = _utilsRepo.GetServerDate();

            if (operations.Any(x => x.Date < new DateTime(currentDate.Year, currentDate.Month, 1) &&
            !_unlockedPeriodRepo.IsUnlockedPeriod(x.Date.Value.Year, x.Date.Value.Month, x.CardId)))
            {
                MessageBox.Show("Вы не можете работать с закрытым месяцем. Одна из операций находится в закрытом месяце");
                return;
            }

            operations.ForEach(x => x.Date = newDate);
            operations.ForEach(x => _cardOperationRepo.Update(x));

            Refresh();
        }
    }
}
