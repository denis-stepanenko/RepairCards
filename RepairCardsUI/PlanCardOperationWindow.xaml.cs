using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PlanCardOperationWindow : Window
    {
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();

        private readonly int _cardOperationId;
        private CardOperation _operation;

        public PlanCardOperationWindow(int cardOperationId)
        {
            InitializeComponent();
            _cardOperationId = cardOperationId;
            Refresh();
        }

        void Refresh()
        {
            _operation = _cardOperationRepo.Get(_cardOperationId);

            codeTextBlock.Text = _operation.Code;
            nameTextBlock.Text = _operation.Name;
            dateDatePicker.SelectedDate = _operation.Date;
            laborTextBlock.Text = _operation.Labor.ToString();
            unitNameTextBlock.Text = _operation.UnitName;
            groupNameTextBlock.Text = _operation.GroupName;
            departmentTextBlock.Text = _operation.Department.ToString();
            countIntegerUpDown.Value = _operation.Count;
            commentTextBox.Text = _operation.Comment;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата\".\n";

            if (countIntegerUpDown.Value == null)
                errors += "Не указано поле \"Количество\".\n";  

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            _operation.Code = codeTextBlock.Text;
            _operation.Name = nameTextBlock.Text;
            _operation.Date = dateDatePicker.SelectedDate;
            _operation.Count = (int)countIntegerUpDown.Value;
            _operation.LaborAll = _operation.Labor * (int)countIntegerUpDown.Value;
            _operation.Comment = commentTextBox.Text;

            // Проверка периода
            var d = _utilsRepo.GetServerDate();

            if (_operation.Date < new DateTime(d.Year, d.Month, 1))
            {
                bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(_operation.Date.Value.Year, _operation.Date.Value.Month, _operation.CardId);
                if (!isUnlockedPeriod)
                {
                    MessageBox.Show("Вы не можете работать с завершенным месяцем");
                    return;
                }
            }

            _cardOperationRepo.Update(_operation);
            
            Close();
        }
    }
}
