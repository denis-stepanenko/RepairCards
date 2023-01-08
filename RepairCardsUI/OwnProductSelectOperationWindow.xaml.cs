using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class OwnProductSelectOperationWindow : Window
    {
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly CardOwnProductOperationRepo _cardOwnProductOperationRepo = new CardOwnProductOperationRepo();
        private readonly ExecutorRepo _executorRepo = new ExecutorRepo();

        private readonly int _productId;
        private readonly string _productCode;
        private readonly string _route;
        private int? _executorId;

        public OwnProductSelectOperationWindow(int productId, string productCode, string route)
        {
            InitializeComponent();
            _productId = productId;
            _productCode = productCode;
            _route = route;
            Refresh();
        }

        void Refresh()
        {
            IEnumerable<ProductOperation> operations;

            if (string.IsNullOrWhiteSpace(_route))
            {
                if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                    operations = _productOperationRepo.GetProductOperationsByDepartment(_productCode, AuthorizationService.User.Department);
                else
                    operations = _productOperationRepo.GetProductOperations(_productCode);
            }
            else
            {
                if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                    operations = _productOperationRepo.GetProductOperationsByDepartment(_productCode, _route, AuthorizationService.User.Department);
                else
                    operations = _productOperationRepo.GetProductOperations(_productCode, _route);
            }

            operationsRadGridView.ItemsSource = operations;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<ProductOperation>().ToList();
            if (operations.Count == 0) return;

            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            if (_executorId != null)
            {
                var executor = _executorRepo.Get((int)_executorId);

                if (operations.Any(x => x.Department != executor.Department))
                {
                    MessageBox.Show("Добавление операций невозможно, потому что не для всех операций цех операции совпадает с цехом исполнителя");
                    return;
                }
            }

            foreach (var operation in operations)
            {
                var newCardOwnProductOperation = new CardOwnProductOperation
                {
                    CardOwnProductId = _productId,
                    Code = operation.Code,
                    Name = operation.Name,
                    Labor = operation.Labor,
                    Type = 0,
                    Date = (DateTime)dateDatePicker.SelectedDate,
                    Department = operation.Department,
                    ExecutorId = _executorId
                };

                _cardOwnProductOperationRepo.Add(newCardOwnProductOperation);
            }

            Close();
        }

        private void ExecutorSelectControl_Click()
        {
            var chooseExecutorWindow = new SelectExecutorWindow();
            if (chooseExecutorWindow.ShowDialog() == true)
            {
                _executorId = chooseExecutorWindow.Executor.Id;
                executorSelectControl.Text = chooseExecutorWindow.Executor.Name;
            }
        }
    }
}
