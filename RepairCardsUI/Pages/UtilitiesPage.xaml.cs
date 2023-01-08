using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class UtilitiesPage : Page
    {
        private readonly UtilsRepo _repo = new UtilsRepo();
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly GeneralSettingsRepo _settingsRepo = new GeneralSettingsRepo();

        public UtilitiesPage()
        {
            InitializeComponent();

            if (!new[] { 2, 8 }.Contains(AuthorizationService.User.RoleId))
            {
                //exportToPDMGroupBox.IsEnabled = false;
            }

            if (!new[] { 2, 3, 4, 5 }.Contains(AuthorizationService.User.RoleId))
            {
                actualizeOperationsGroupBox.IsEnabled = false;
            }

            if (!new[] { 2, 4 }.Contains(AuthorizationService.User.RoleId))
            {
                periodClosingDayGroupBox.IsEnabled = false;
            }

            var settings = _settingsRepo.Get();
            periodClosingDayNumericUpDown.Value = settings.PeriodClosingDay;
        }


        Card cardForActualization;

        private void acualizeOperationsCardSelectControl_Click()
        {
            var chooseCardWindow = new SelectCardWindow();
            if (chooseCardWindow.ShowDialog() == true)
            {
                cardForActualization = chooseCardWindow.Card;

                acualizeOperationsCardSelectControl.Text = $"{cardForActualization.Number}, {cardForActualization.RepairType.Name}, {cardForActualization.ProductCode}, {cardForActualization.ProductName}";
            }
        }

        private void acualizeOperationsCardSelectControl_Clear()
        {
            acualizeOperationsCardSelectControl.Text = "";
            cardForActualization = null;
        }

        private void actualizeOperationsButton_Click(object sender, RoutedEventArgs e)
        {
            var isConfirmed = _cardRepo.IsConfirmed(cardForActualization.Id, 1);

            if (isConfirmed)
            {
                MessageBox.Show("Вы не можете изменить карту т.к она утверждена");
                return;
            }

            if (cardForActualization != null)
            {

                _repo.ActualizeOperations(cardForActualization);

                MessageBox.Show("Готово");
            }
        }

        private void changePeriodClosingDayButton_Click(object sender, RoutedEventArgs e)
        {
            if (periodClosingDayNumericUpDown.Value == null)
            {
                MessageBox.Show("Введите значение");
                return;
            }

            var settings = _settingsRepo.Get();
            settings.PeriodClosingDay = (int)periodClosingDayNumericUpDown.Value;
            _settingsRepo.Update(settings);

            MessageBox.Show("Готово");
        }
    }
}
