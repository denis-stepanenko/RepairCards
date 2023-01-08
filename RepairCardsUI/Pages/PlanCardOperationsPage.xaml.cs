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
    public partial class PlanCardOperationsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();

        private readonly int _cardId;

        public PlanCardOperationsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                addFromTemplateButton.Visibility = Visibility.Collapsed;
                addFromAnotherCardButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {

            if (_cardRepo.IsConfirmed(_cardId, 1) || _cardRepo.IsConfirmed(_cardId, 5))
            {
                addButton.Visibility = Visibility.Collapsed;
                addFromTemplateButton.Visibility = Visibility.Collapsed;
                addFromAnotherCardButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            var operations = _cardOperationRepo.GetAllByCardAndType(_cardId, 0);
            operationsRadGridView.ItemsSource = operations;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var planChooseOperationWindow = new PlanSelectOperationWindow(_cardId);
            planChooseOperationWindow.ShowDialog();
            Refresh();
        }

        private void AddFromTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var planChooseOperationFromTemplateWindow = new PlanSelectOperationFromTemplateWindow(_cardId);
            planChooseOperationFromTemplateWindow.ShowDialog();
            Refresh();
        }

        private void addFromAnotherCardButton_Click(object sender, RoutedEventArgs e)
        {
            new SelectPlanOperationFromAnotherCardWindow(_cardId).ShowDialog();
            Refresh();
        }

        void Edit()
        {
            var operation = (CardOperation)operationsRadGridView.SelectedItem;
            if (operation == null) return;

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operation.Department == 4) ||
                (card.IsDepartment5Confirmed && operation.Department == 5) ||
                (card.IsDepartment6Confirmed && operation.Department == 6) ||
                (card.IsDepartment13Confirmed && operation.Department == 13) ||
                (card.IsDepartment17Confirmed && operation.Department == 17) ||
                (card.IsDepartment80Confirmed && operation.Department == 80) ||
                (card.IsDepartment82Confirmed && operation.Department == 82))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь добавить");
                return;
            }

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (operation.Department != AuthorizationService.User.Department)
                {
                    MessageBox.Show("Изменение операции невозможно, потому что операция не принадлежит вашему цеху");
                    return;
                }

            var planCardOperationWindow = new PlanCardOperationWindow(operation.Id);
            planCardOperationWindow.ShowDialog();
            Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void operationsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<CardOperation>().ToList();
            if (operations.Count() == 0) return;

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Department == 82)))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь добавить");
                return;
            }

            // Проверка периода
            var d = _utilsRepo.GetServerDate();

            foreach (var item in operations)
            {
                if (item.Date < new DateTime(d.Year, d.Month, 1))
                {
                    bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(item.Date.Value.Year, item.Date.Value.Month, _cardId);
                    if (!isUnlockedPeriod)
                    {
                        MessageBox.Show("Вы не можете работать с завершенным месяцем");
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

    }
}
