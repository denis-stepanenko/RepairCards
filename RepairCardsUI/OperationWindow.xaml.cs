using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace RepairCardsUI
{
    public partial class OperationWindow : Window
    {
        private readonly OperationRepo _repo = new OperationRepo();

        private int? _operationId;
        private Operation _operation;

        public OperationWindow(int? operationId)
        {
            InitializeComponent();
            _operationId = operationId;

            if (operationId != null)
            {
                Refresh();
            }
        }

        void Refresh()
        {
            _operation = _repo.Get((int)_operationId);

            codeTextBox.Text = _operation.Code;
            nameTextBox.Text = _operation.Name;
            laborNumericUpDown.Value = (double)_operation.Labor;
            priceNumericUpDown.Value = (double)_operation.Price;
            unitNameTextBox.Text = _operation.UnitName;
            groupNameTextBox.Text = _operation.GroupName;
            departmentComboBox.Text = _operation.Department.ToString();
            descriptionTextBox.Text = _operation.Description;
            isInactiveCheckBox.IsChecked = _operation.IsInactive;
        }

        private void GenerateCode_Click(object sender, RoutedEventArgs e)
        {
            codeTextBox.Text = _repo.GenerateOperationNumber();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(codeTextBox.Text))
                errors += "Не указано поле \"Код\".\n";
            else if (!Regex.IsMatch(codeTextBox.Text, @"^[0-9]{6}$"))
                errors += "Значение в поле \"Код\" должно состоять из 6-ти цифр.\n";

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                errors += "Не указано поле \"Наименование\".\n";

            if (laborNumericUpDown.Value == null)
                errors += "Не указано поле \"Трудоемкость\".\n";

            if (priceNumericUpDown.Value == null)
                errors += "Не указано поле \"Цена\".\n";

            if (string.IsNullOrWhiteSpace(unitNameTextBox.Text))
                errors += "Не указано поле \"Единица измерения\".\n";

            if (string.IsNullOrWhiteSpace(groupNameTextBox.Text))
                errors += "Не указано поле \"Группа\".\n";

            if (departmentComboBox.SelectedIndex == -1)
                errors += "Не указано поле \"Цех\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            if (_operationId == null)
            {
                var newOperation = new Operation
                {
                    Code = codeTextBox.Text,
                    Name = nameTextBox.Text,
                    Labor = (decimal)laborNumericUpDown.Value,
                    Price = (decimal)priceNumericUpDown.Value,
                    UnitName = unitNameTextBox.Text,
                    GroupName = groupNameTextBox.Text,
                    Department = Convert.ToInt32(departmentComboBox.Text),
                    Description = descriptionTextBox.Text,
                    IsInactive = (bool)isInactiveCheckBox.IsChecked
                };

                if (_repo.IsThereOperationWithSuchCode(newOperation))
                {
                    MessageBox.Show("Уже есть операция с таким кодом");
                    return;
                }

                _operationId = _repo.Add(newOperation);
            }
            else
            {
                _operation.Code = codeTextBox.Text;
                _operation.Name = nameTextBox.Text;
                _operation.Labor = (decimal)laborNumericUpDown.Value;
                _operation.Price = (decimal)priceNumericUpDown.Value;
                _operation.UnitName = unitNameTextBox.Text;
                _operation.GroupName = groupNameTextBox.Text;
                _operation.Department = Convert.ToInt32(departmentComboBox.Text);
                _operation.Description = descriptionTextBox.Text;
                _operation.IsInactive = (bool)isInactiveCheckBox.IsChecked;

                if (_repo.IsThereOperationWithSuchCode(_operation))
                {
                    MessageBox.Show("Уже есть операция с таким кодом");
                    return;
                }

                _repo.Update(_operation);
            }

            Close();
        }
    }
}
