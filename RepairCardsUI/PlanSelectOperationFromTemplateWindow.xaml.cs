using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PlanSelectOperationFromTemplateWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly TemplateOperationRepo _templateOperationRepo = new TemplateOperationRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();

        private readonly int _cardId;
        private Template _template;

        public PlanSelectOperationFromTemplateWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
        }

        void Refresh()
        {
            IEnumerable<TemplateOperation> operations;
            operations = _templateOperationRepo.GetAll(_template.Id);

            operationsRadGridView.ItemsSource = operations;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<TemplateOperation>().ToList();
            if (operations.Count == 0) return;

            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";
            
            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Operation.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Operation.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Operation.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Operation.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Operation.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Operation.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Operation.Department == 82)))
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
                    Code = operation.Operation.Code,
                    Name = operation.Operation.Name,
                    Count = operation.Count,
                    Labor = operation.Operation.Labor,
                    Type = 0,
                    Date = dateDatePicker.SelectedDate,
                    ExecutorId = null,
                    UnitName = operation.Operation.UnitName,
                    LaborAll = operation.Operation.Labor * operation.Count,
                    GroupName = operation.Operation.GroupName,
                    Department = operation.Operation.Department,
                    TemplateName = _template.Name
                };

                _cardOperationRepo.Add(newCardOperation);
            }


            Close();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_template == null) return;

            Refresh();
        }

        private void TemplateSelectControl_Click()
        {
            var chooseTemplateWindow = new SelectTemplateWindow();
            if (chooseTemplateWindow.ShowDialog() == true)
            {
                _template = chooseTemplateWindow.CurrentTemplate;
                templateSelectControl.Text = $"{_template.Id}, {_template.Name}";
            }
        }
    }
}
