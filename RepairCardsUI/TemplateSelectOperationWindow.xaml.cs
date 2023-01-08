using MahApps.Metro.Controls;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class TemplateSelectOperationWindow : MetroWindow
    {
        private readonly OperationRepo _operationRepo = new OperationRepo();
        private readonly TemplateOperationRepo _templateOperationRepo = new TemplateOperationRepo();

        private readonly int _templateId;

        public TemplateSelectOperationWindow(int templateId)
        {
            InitializeComponent();
            _templateId = templateId;

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

            if (countIntegerUpDown.Value == null)
                errors += "Не указано поле \"Количество\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            foreach (var operation in operations)
            {
                var newTemplateOperation = new TemplateOperation
                {
                    TemplateId = _templateId,
                    OperationId = operation.Id,
                    Count = (int)countIntegerUpDown.Value
                };

                _templateOperationRepo.Add(newTemplateOperation);
            }

            Close();
        }
    }
}
