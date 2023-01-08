using MahApps.Metro.Controls;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class TemplateOperationWindow : MetroWindow
    {
        private readonly TemplateOperationRepo _repo = new TemplateOperationRepo();

        private readonly int _templateOperationId;
        private TemplateOperation _templateOperation;

        public TemplateOperationWindow(int templateOperationId)
        {
            InitializeComponent();
            _templateOperationId = templateOperationId;

            Refresh();
        }

        void Refresh()
        {
            _templateOperation = _repo.Get(_templateOperationId);

            codeTextBlock.Text = _templateOperation.Operation.Code;
            nameTextBlock.Text = _templateOperation.Operation.Name;
            laborTextBlock.Text = _templateOperation.Operation.Labor.ToString();
            unitNameTextBlock.Text = _templateOperation.Operation.UnitName;
            groupNameTextBlock.Text = _templateOperation.Operation.GroupName;
            departmentTextBlock.Text = _templateOperation.Operation.Department.ToString();
            countIntegerUpDown.Value = _templateOperation.Count;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (countIntegerUpDown.Value == null)
                errors += "Не указано поле \"Количество\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            _templateOperation.Count = (int)countIntegerUpDown.Value;

            _repo.Update(_templateOperation);

            Close();
        }
    }
}
