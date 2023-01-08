using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardRepairProductOperationWindow : Window
    {
        private readonly CardRepairProductOperationRepo _repo = new CardRepairProductOperationRepo();

        private readonly int _cardOperationId;
        private CardRepairProductOperation _operation;

        public CardRepairProductOperationWindow(int cardOperationId)
        {
            InitializeComponent();
            _cardOperationId = cardOperationId;
            Refresh();
        }

        void Refresh()
        {
            _operation = _repo.Get(_cardOperationId);

            codeTextBlock.Text = _operation.Code;
            nameTextBlock.Text = _operation.Name;
            executorSelectControl.Text = _operation.Executor?.Name;
            dateDatePicker.SelectedDate = _operation.Date;
            unitNameTextBlock.Text = _operation.UnitName;
            groupNameTextBlock.Text = _operation.GroupName;
            departmentTextBlock.Text = _operation.Department.ToString();
            laborTextBlock.Text = _operation.Labor.ToString();
            countIntegerUpDown.Value = (double)_operation.Count;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";

            if (executorSelectControl.Text == null)
                errors += "Не указано поле \"Исполнитель\".\n";

            if (countIntegerUpDown.Value == null)
                errors += "Не указано поле \"Количество\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            _operation.Code = codeTextBlock.Text;
            _operation.Name = nameTextBlock.Text;
            _operation.Date = (DateTime)dateDatePicker.SelectedDate;
            _operation.Count = (int)countIntegerUpDown.Value;
            _operation.LaborAll = _operation.Labor * (int)countIntegerUpDown.Value;

            _repo.Update(_operation);

            Close();
        }

        private void ExecutorSelectControl_Click()
        {
            var chooseExecutorWindow = new SelectExecutorWindow();
            if (chooseExecutorWindow.ShowDialog() == true)
            {
                _operation.ExecutorId = chooseExecutorWindow.Executor.Id;
                executorSelectControl.Text = chooseExecutorWindow.Executor.Name;
            }
        }
    }
}
