using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectPlanOperationFromAnotherCardWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly UtilsRepo _utilsRepo = new UtilsRepo();
        private readonly UnlockedPeriodRepo _unlockedPeriodRepo = new UnlockedPeriodRepo();

        private readonly int _cardId;
        private Card _card;

        public SelectPlanOperationFromAnotherCardWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
        }

        void Refresh()
        {
            var items = _cardOperationRepo.GetAllByCardAndType(_card.Id, 0);

            itemsRadGridView.ItemsSource = items;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = itemsRadGridView.SelectedItems.Cast<CardOperation>().ToList();
            if (operations.Count == 0) return;

            if (dateDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Не указана дата");
                return;
            }

            var card = _cardRepo.Get(_cardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Department == 82)))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые вы пытаетесь добавить");
                return;
            }

            var selectedDate = dateDatePicker.SelectedDate.Value;

            // Проверка периода
            var d = _utilsRepo.GetServerDate();

            if (selectedDate < new DateTime(d.Year, d.Month, 1))
            {
                bool isUnlockedPeriod = _unlockedPeriodRepo.IsUnlockedPeriod(selectedDate.Year, selectedDate.Month, _cardId);
                if (!isUnlockedPeriod)
                {
                    MessageBox.Show("Вы не можете работать с завершенным месяцем");
                    return;
                }
            }

            foreach (var operation in operations)
            {
                operation.CardId = _cardId;
                operation.Date = selectedDate;
                _cardOperationRepo.Add(operation);
            }

            Close();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_card == null) return;

            Refresh();
        }

        private void cardSelectControl_Click()
        {
            var chooseCardWindow = new SelectCardWindow();
            if (chooseCardWindow.ShowDialog() == true)
            {
                _card = chooseCardWindow.Card;
                cardSelectControl.Text = $"{_card.Number}, {_card.ProductCode}";
            }
        }
    }
}
