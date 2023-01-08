using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Data;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardOwnProductOperationWindow : Window
    {
        private readonly ExecutorRepo _executorRepo = new ExecutorRepo();
        private readonly CardOwnProductOperationRepo _productOperationRepo = new CardOwnProductOperationRepo();

        private readonly int _operationId;
        private CardOwnProductOperation _operation;

        public CardOwnProductOperationWindow(int operationId)
        {
            InitializeComponent();
            _operationId = operationId;

            Refresh();
        }

        void Refresh()
        {
            _operation = _productOperationRepo.Get(_operationId);

            codeTextBlock.Text = _operation.Code;
            nameTextBlock.Text = _operation.Name;
            dateDatePicker.SelectedDate = _operation.Date;

            var executors = _executorRepo.GetAllByDepartment(_operation.Department);
            executorsComboBox.ItemsSource = executors;

            var idx = executorsComboBox.Items.IndexOf(executors.Where(x => x.Id == _operation.ExecutorId).FirstOrDefault());
            executorsComboBox.SelectedIndex = idx;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";

            if (executorsComboBox.SelectedIndex == -1)
                errors += "Не указано поле \"Исполнитель\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            var executor = executorsComboBox.SelectedItem as Executor;
            _operation.Date = dateDatePicker.SelectedDate;
            _operation.ExecutorId = executor.Id;

            _productOperationRepo.Update(_operation);

            Close();
        }
    }
}
