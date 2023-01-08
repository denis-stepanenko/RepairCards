using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class ExecutorWindow : Window
    {
        private readonly ExecutorRepo _repo = new ExecutorRepo();

        private int? _executorId;
        private Executor _executor;

        public ExecutorWindow(int? executorId)
        {
            InitializeComponent();
            _executorId = executorId;

            if (executorId != null)
            {
                Refresh();
            }
        }

        void Refresh()
        {
            _executor = _repo.Get((int)_executorId);

            nameTextBox.Text = _executor.Name;
            departmentComboBox.Text = _executor.Department.ToString();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                errors += "Не указано поле \"Наименование\".\n";

            if (departmentComboBox.SelectedIndex == -1)
                errors += "Не указано поле \"Цех\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            if (_executorId == null)
            {
                var newExecutor = new Executor
                {
                    Name = nameTextBox.Text,
                    Department = Convert.ToInt32(departmentComboBox.Text)
                };

                _executorId = _repo.Add(newExecutor);
            }
            else
            {
                _executor.Name = nameTextBox.Text;
                _executor.Department = Convert.ToInt32(departmentComboBox.Text);

                _repo.Update(_executor);
            }

            Close();
        }
    }
}
