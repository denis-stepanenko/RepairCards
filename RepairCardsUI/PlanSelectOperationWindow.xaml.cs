using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PlanSelectOperationWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly OperationRepo _operationRepo = new OperationRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();

        private readonly int _cardId;

        public PlanSelectOperationWindow(int cardId)
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<Operation>().ToList();
            if (operations.Count == 0) return;

            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";

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

            var selectedDate = dateDatePicker.SelectedDate;

            // Проверка периода
            var d = _utilsRepo.GetServerDate();
            if (selectedDate < new DateTime(d.Year, d.Month, 1))
            {
                bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(selectedDate.Value.Year, selectedDate.Value.Month, _cardId);
                if (!isUnlockedPeriod)
                {
                    MessageBox.Show("Вы не можете работать с завершенным месяцем");
                    return;
                }
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
                    Type = 0,
                    Date = dateDatePicker.SelectedDate,
                    ExecutorId = null,
                    UnitName = operation.UnitName,
                    LaborAll = operation.Labor * (int)countIntegerUpDown.Value,
                    GroupName = operation.GroupName,
                    Department = operation.Department,
                    TemplateCode = null,
                    TemplateName = null
                };

                _cardOperationRepo.Add(newCardOperation);
            }

            Close();
        }
    }
}
