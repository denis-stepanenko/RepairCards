using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class CardConfirmationsPage : Page
    {
        private readonly CardConfirmationRepo _repo = new CardConfirmationRepo();
        private readonly CardRepo _cardRepo = new CardRepo();

        private readonly int _cardId;

        public CardConfirmationsPage()
        {
            InitializeComponent();

            _cardId = (int)PageNavigationHelper.Parameter;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

            if (!new[] { 3, 4, 6, 7, 9 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        void Refresh()
        {
            var confirmations = _repo.GetAllByCard(_cardId);
            confirmationsRadGridView.ItemsSource = confirmations;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newCardConfirmationWindow = new NewCardConfirmationWindow(_cardId);
            newCardConfirmationWindow.ShowDialog();
            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmation = confirmationsRadGridView.SelectedItem as CardConfirmation;
            if (confirmation == null) return;

            var dialog = MessageBox.Show("Удалить выбранную запись?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (confirmation.UserId != AuthorizationService.User.Id)
            {
                MessageBox.Show("Вы не можете удалить утверждение другого пользователя");
                return;
            }

            try
            {
                _repo.Delete(confirmation.Id);

                // Если ОТК сняли утверждение, то сменить статус на "В работе" :с
                var confirmations = _repo.GetAllByCard(confirmation.CardId);
                var otkConfirmationsCount = confirmations.Count(x => x.CardConfirmationObjectId == 1 && x.UserRoleId == 6);
                if (confirmation.CardConfirmationObjectId == 1 && confirmation.UserRoleId == 6 && otkConfirmationsCount == 0)
                {
                    var card = _cardRepo.Get(confirmation.CardId);
                    card.CardStatusId = 1;
                    _cardRepo.Update(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Refresh();
        }
    }
}
