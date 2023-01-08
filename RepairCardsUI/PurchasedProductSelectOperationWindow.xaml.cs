using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PurchasedProductSelectOperationWindow : Window
    {
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly ExecutorRepo _executorRepo = new ExecutorRepo();
        private readonly CardPurchasedProductOperationRepo _cardProductOperationRepo = new CardPurchasedProductOperationRepo();

        private readonly int _productId;
        private readonly string _productCode;
        private int _executorId;

        public PurchasedProductSelectOperationWindow(int productId, string productCode)
        {
            InitializeComponent();
            _productId = productId;
            _productCode = productCode;
            Refresh();
        }

        void Refresh()
        {
            IEnumerable<ProductOperation> operations;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                operations = _productOperationRepo.GetProductOperationsByDepartment(_productCode, AuthorizationService.User.Department);
            else
                operations = _productOperationRepo.GetProductOperations(_productCode);

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

            if (string.IsNullOrWhiteSpace(executorSelectControl.Text))
                errors += "Не указано поле \"Исполнитель\".\n";

            if (_executorId != 0)
            {
                if (errors != "")
                {
                    MessageBox.Show(errors);
                    return;
                }
            }

            var executor = _executorRepo.Get(_executorId);
            if (operations.Any(x => x.Department != executor.Department))
            {
                MessageBox.Show("Добавление операций невозможно, потому что не для всех операций цех операции совпадает с цехом исполнителя");
                return;
            }

            foreach (var operation in operations)
            {
                var newOperation = new CardPurchasedProductOperation
                {
                    CardPurchasedProductId = _productId,
                    Code = operation.Code,
                    Name = operation.Name,
                    Labor = operation.Labor,
                    Type = 0,
                    Date = (DateTime)dateDatePicker.SelectedDate,
                    Department = operation.Department,
                    ExecutorId = _executorId
                };

                _cardProductOperationRepo.Add(newOperation);
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
