using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class FactSelectOperationWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly OperationRepo _operationRepo = new OperationRepo();
        private readonly ExecutorRepo _executorRepo = new ExecutorRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly GeneralSettingsRepo _settingsRepo = new GeneralSettingsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();

        private readonly int _cardId;
        private int _executorId;

        public FactSelectOperationWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
            Refresh();
        }

        void Refresh()
        {
            IEnumerable<Operation> operations;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                operations = _operationRepo.GetAllActiveByDepartment(AuthorizationService.User.Department);
            else
                operations = _operationRepo.GetAllActive();

            operationsRadGridView.ItemsSource = operations;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void ExecutorSelectControl_Click()
        {
            var chooseExecutorWindow = new SelectExecutorWindow();
            if (chooseExecutorWindow.ShowDialog() == true)
            {
                _executorId = chooseExecutorWindow.Executor.Id;
                executorSelectControl.Text = chooseExecutorWindow.Executor.Name;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<Operation>().ToList();
            if (operations.Count == 0) return;

            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";

            if (string.IsNullOrWhiteSpace(executorSelectControl.Text))
                errors += "Не указано поле \"Исполнитель\".\n";
            
            if (countIntegerUpDown.Value == null)
                errors += "Не указано поле \"Количество\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

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


            var selectedDate = dateDatePicker.SelectedDate.Value;

            // Проверка периода
            var d = _utilsRepo.GetServerDate();
            if (selectedDate < new DateTime(d.Year, d.Month, 1))
            {
                bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(selectedDate.Year, selectedDate.Month, _cardId);
                if (!isUnlockedPeriod)
                {
                    MessageBox.Show("Вы не можете работать с завершенным месяцем");
                    return;
                }
            }
            
            var date = _utilsRepo.GetServerDate();
            var settings = _settingsRepo.Get();
            if (selectedDate.Year == date.Year && selectedDate.Month == date.Month)
                if (selectedDate.Day > settings.PeriodClosingDay)
                {
                    MessageBox.Show($"Вы не можете назначить операции выбранную дату, потому что операции в текущем месяце могут выполняться до {settings.PeriodClosingDay} числа включительно. Укажите дату следующего месяца");
                    return;
                }

            var executor = _executorRepo.Get(_executorId);
            if (operations.Any(x => x.Department != executor.Department))
            {
                MessageBox.Show("Добавление операций невозможно, потому что не для всех операций цех операции совпадает с цехом исполнителя");
                return;
            }

            foreach (var operation in operations)
            {
                var newCardOperation = new CardOperation
                {
                    CardId = _cardId,
                    Code = operation.Code,
                    Name = operation.Name,
                    Count = (int)countIntegerUpDown.Value,
                    Labor = operation.Labor,
                    Type = 1,
                    Date = dateDatePicker.SelectedDate,
                    ExecutorId = _executorId,
                    UnitName = operation.UnitName,
                    LaborAll = operation.Labor * (int)countIntegerUpDown.Value,
                    GroupName = operation.GroupName,
                    Department = operation.Department
                };

                _cardOperationRepo.Add(newCardOperation);
            }

            Close();
        }
    }
}
