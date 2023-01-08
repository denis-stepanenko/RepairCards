using MahApps.Metro.Controls;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class UnlockedPeriodWindow : MetroWindow
    {
        private readonly UnlockedPeriodRepo _repo = new UnlockedPeriodRepo();

        private int? _periodId;
        private UnlockedPeriod _period;
        private Card _card;

        public UnlockedPeriodWindow(int? periodId)
        {
            InitializeComponent();
            _periodId = periodId;

            if (periodId != null)
            {
                Refresh();
            }
            else
            {
                yearNumericUpDown.Value = DateTime.Now.Year;
                monthNumericUpDown.Value = DateTime.Now.Month;
            }
        }

        void Refresh()
        {
            _period = _repo.Get((int)_periodId);

            yearNumericUpDown.Value = _period.Year;
            monthNumericUpDown.Value = _period.Month;
            _card = _period.Card;
            cardSelectControl.Text = $"{_card.Number}, {_card.ProductCode}";
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (yearNumericUpDown.Value == null)
                errors += "Не выбран год\n";

            if (monthNumericUpDown.Value == null)
                errors += "Не выбран месяц\n";

            if (_card == null)
                errors += "Не выбрана карта";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            if (_periodId == null)
            {
                var newPeriod = new UnlockedPeriod
                {
                    Year = (int)yearNumericUpDown.Value,
                    Month = (int)monthNumericUpDown.Value,
                    CardId = _card.Id,
                    CreatorName = AuthorizationService.User.Name
                };

                var isExists = _repo.IsExistsPeriod(newPeriod);
                if (isExists)
                {
                    MessageBox.Show("Такой период уже существует");
                    return;
                }

                _periodId = _repo.Add(newPeriod);

            }
            else
            {
                _period.Year = (int)yearNumericUpDown.Value;
                _period.Month = (int)monthNumericUpDown.Value;
                _period.CardId = _card.Id;

                var isExists = _repo.IsExistsPeriod(_period);
                if (isExists)
                {
                    MessageBox.Show("Такой период уже существует");
                    return;
                }

                _repo.Update(_period);
            }

            Close();
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
