using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class RepairProductSelectRepairOperationWindow : Window
    {
        private readonly OperationRepo _operationRepo = new OperationRepo();
        private readonly ExecutorRepo _executorRepo = new ExecutorRepo();
        private readonly CardRepairProductOperationRepo _cardRepairProductOperationRepo = new CardRepairProductOperationRepo();

        private readonly int _productId;
        private int _executorId;

        public RepairProductSelectRepairOperationWindow(int productId)
        {
            InitializeComponent();
            _productId = productId;
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

            var executor = _executorRepo.Get(_executorId);
            if (operations.Any(x => x.Department != executor.Department))
            {
                MessageBox.Show("Добавление операций невозможно, потому что не для всех операций цех операции совпадает с цехом исполнителя");
                return;
            }

            foreach (var operation in operations)
            {
                var newOperation = new CardRepairProductOperation
                {
                    CardRepairProductId = _productId,
                    Code = operation.Code,
                    Name = operation.Name,
                    Labor = operation.Labor,
                    Count = (int)countIntegerUpDown.Value,
                    LaborAll = operation.Labor * (int)countIntegerUpDown.Value,
                    Date = (DateTime)dateDatePicker.SelectedDate,
                    Department = operation.Department,
                    UnitName = operation.UnitName,
                    GroupName = operation.GroupName,
                    ExecutorId = _executorId
                };

                _cardRepairProductOperationRepo.Add(newOperation);
            }

            Close();
        }
    }
}
