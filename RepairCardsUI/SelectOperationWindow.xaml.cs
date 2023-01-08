using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RepairCardsUI
{
    public partial class SelectOperationWindow : Window
    {
        private readonly OperationRepo _operationRepo = new OperationRepo();

        public Operation Operation { get; set; }

        public SelectOperationWindow()
        {
            InitializeComponent();
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

        void Ok()
        {
            var operation = operationsRadGridView.SelectedItem as Operation;
            if (operation == null) return;

            Operation = operation;

            DialogResult = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) => Ok();

        private void operationsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Ok();
    }
}
