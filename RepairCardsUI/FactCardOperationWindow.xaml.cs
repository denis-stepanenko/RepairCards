using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class FactCardOperationWindow : Window
    {
        private readonly CardOperationRepo _repo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();
        private readonly GeneralSettingsRepo _settingsRepo = new GeneralSettingsRepo();

        private readonly int _cardOperationId;
        private CardOperation _operation;

        public FactCardOperationWindow(int cardOperationId)
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
            countIntegerUpDown.Value = _operation.Count;
            commentTextBox.Text = _operation.Comment;
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

            var selectedDate = dateDatePicker.SelectedDate.Value;
            var date = _utilsRepo.GetServerDate();
            var settings = _settingsRepo.Get();

            if (selectedDate.Year == date.Year && selectedDate.Month == date.Month)
                if (selectedDate.Day > settings.PeriodClosingDay)
                {
                    MessageBox.Show($"Вы не можете назначить операции выбранную дату, потому что операции в текущем месяце могут выполняться до {settings.PeriodClosingDay} числа включительно. Укажите дату следующего месяца");
                    return;
                }

            _operation.Code = codeTextBlock.Text;
            _operation.Name = nameTextBlock.Text;
            _operation.Date = dateDatePicker.SelectedDate;
            _operation.Count = (int)countIntegerUpDown.Value;
            _operation.LaborAll = _operation.Labor * (int)countIntegerUpDown.Value;
            _operation.Comment = commentTextBox.Text;

            // Проверка периода
            var currentDate = _utilsRepo.GetServerDate();
            if (_operation.Date < new DateTime(currentDate.Year, currentDate.Month, 1))
            {
                bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(_operation.Date.Value.Year, _operation.Date.Value.Month, _operation.CardId);
                if (!isUnlockedPeriod)
                {
                    MessageBox.Show("Вы не можете работать с завершенным месяцем");
                    return;
                }
            }

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
