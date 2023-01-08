using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class NewCardConfirmationWindow : Window
    {
        private readonly CardConfirmationObjectRepo _cardConfirmationObjectRepo = new CardConfirmationObjectRepo();
        private readonly CardConfirmationRepo _cardConfirmationRepo = new CardConfirmationRepo();
        private readonly CardRepo _cardRepo = new CardRepo();

        private readonly int _cardId;

        public NewCardConfirmationWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;

            nameTextBlock.Text = AuthorizationService.User.Name;
            roleTextBlock.Text = AuthorizationService.User.Role.Name;

            Refresh();
        }

        void Refresh()
        {
            var objects = _cardConfirmationObjectRepo.GetAll();
            objectsListBox.ItemsSource = objects;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmationObject = objectsListBox.SelectedItem as CardConfirmationObject;
            if (confirmationObject == null)
            {
                MessageBox.Show("Не выбрана область утверждения");
                return;
            }

            switch (AuthorizationService.User.RoleId)
            {
                // Начальник ТБ
                case 3:
                    if (confirmationObject.Id != 1)
                    {
                        MessageBox.Show("Начальник ТБ может утвердить только карту целиком");
                        return;
                    }
                    break;
                // Представитель ООИОТ
                case 4:
                    if (confirmationObject.Id != 5)
                    {
                        MessageBox.Show("Представитель ООИОТ может утвердить только операции");
                        return;
                    }
                    break;
                // Начальник ПРБ
                case 7:
                    if (confirmationObject.Id != 6)
                    {
                        MessageBox.Show("Начальник ПРБ может утвердить только материалы, ПКИ, ДСЕ");
                        return;
                    }
                    break;
                // Представитель ОТК
                case 6:
                    if (!(confirmationObject.Id == 1 || confirmationObject.Id == 2 || confirmationObject.Id == 3 || confirmationObject.Id == 4))
                    {
                        MessageBox.Show("Представитель ОТК может утвердить только карту целиком, внутреннюю дефектацию, неисправности и внешнюю дефектацию");
                        return;
                    }
                    break;
                // Представитель ВП
                case 9:
                    if (!(confirmationObject.Id == 2 || confirmationObject.Id == 3 || confirmationObject.Id == 4))
                    {
                        MessageBox.Show("Представитель ВП может утвердить только внутреннюю дефектацию, неисправности и внешнюю дефектацию");
                        return;
                    }
                    break;
            }


            try
            {
                var newConfirmation = new CardConfirmation
                {
                    CardId = _cardId,
                    UserId = AuthorizationService.User.Id,
                    UserRoleId = AuthorizationService.User.RoleId,
                    UserName = AuthorizationService.User.Name,
                    CardConfirmationObjectId = confirmationObject.Id
                };

                // Если ОТК ставит утверждение, то перевести статус карты в "Завершена" :с
                if (newConfirmation.CardConfirmationObjectId == 1 && newConfirmation.UserRoleId == 6)
                {
                    var card = _cardRepo.Get(newConfirmation.CardId);
                    card.CardStatusId = 2;
                    _cardRepo.Update(card);
                }

                _cardConfirmationRepo.Add(newConfirmation);

            }
            catch (Exception ex) when (ex.Message.Contains("uq_confirm"))
            {
                MessageBox.Show("Выбранная область уже утверждена вами или другим пользователем с такой же ролью");
                return;
            }


            Close();
        }
    }
}
