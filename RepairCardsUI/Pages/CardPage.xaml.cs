using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class CardPage : Page
    {
        private readonly CardRepo _repo = new CardRepo();
        private readonly RequestRepo _requestRepo = new RequestRepo();

        private readonly int _cardId;
        private Card _card;

        public CardPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            Refresh();
        }

        void Refresh()
        {
            Card parentCard = null;
            Card parentCard2 = null;

            _card = _repo.Get(_cardId);
            bool isConfirmed = _repo.IsConfirmed((int)_cardId, 1);

            if (_card.ParentId != null)
                parentCard = _repo.Get((int)_card.ParentId);

            if (_card.ParentId2 != null)
                parentCard2 = _repo.Get((int)_card.ParentId2);

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId) 
                || (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department) 
                && _card.Department != AuthorizationService.User.Department))
            {
                parentCardSelectControl.ShowClearButton = false;
                parentCardSelectControl.ShowChooseButton = false;
                numberTextBox.IsReadOnly = true;
                repairTypeComboBox.IsEnabled = false;
                departmentComboBox.IsEnabled = false;
                sourceComboBox.IsEnabled = false;
                dateDatePicker.IsEnabled = false;
                invoiceNumberTextBox.IsEnabled = false;
                productSelectControl.ShowClearButton = false;
                productSelectControl.ShowChooseButton = false;
                orderSelectControl.ShowClearButton = false;
                orderSelectControl.ShowChooseButton = false;
                factoryNumberTextBox.IsReadOnly = true;
                stageTextBox.IsReadOnly = true;
                reasonForRepairTextBox.IsReadOnly = true;
                cardStatusComboBox.IsEnabled = false;
                actNumberTextBox.IsReadOnly = true;
                saveButton.Visibility = Visibility.Collapsed;
            }

            if (AuthorizationService.User.RoleId != 4)
            {
                isDepartment4ConfirmedCheckBox.IsEnabled = false;
                isDepartment5ConfirmedCheckBox.IsEnabled = false;
                isDepartment6ConfirmedCheckBox.IsEnabled = false;
                isDepartment13ConfirmedCheckBox.IsEnabled = false;
                isDepartment17ConfirmedCheckBox.IsEnabled = false;
                isDepartment80ConfirmedCheckBox.IsEnabled = false;
                isDepartment82ConfirmedCheckBox.IsEnabled = false;
            }

            if (new[] { 1, 6, 8, 9 }.Contains(AuthorizationService.User.RoleId))
            {
                addRequestButton.Visibility = Visibility.Collapsed;
            }

            if (parentCard != null)
                parentCardSelectControl.Text = $"{parentCard.Number}, {parentCard.Direction}, {parentCard.Cipher}, {parentCard.RepairType.Name}, {parentCard.ProductCode}, {parentCard.ProductName}";

            if (parentCard2 != null)
                parentCard2TextBox.Text = $"{parentCard2.Number}, {parentCard2.RepairType.Name}, {parentCard2.ProductCode}, {parentCard2.ProductName}";

            numberTextBox.Text = _card.Number;
            repairTypeComboBox.SelectedIndex = (_card.RepairTypeId == null ? 0 : (int)_card.RepairTypeId - 1);
            departmentComboBox.Text = _card.Department.ToString();

            factoryNumberTextBox.Text = _card.FactoryNumber;
            stageTextBox.Text = _card.Stage;
            invoiceNumberTextBox.Text = _card.InvoiceNumber;
            dateDatePicker.SelectedDate = _card.Date;
            sourceComboBox.Text = _card.Source.ToString();
            reasonForRepairTextBox.Text = _card.ReasonForRepair;
            productSelectControl.Text = _card.ProductCode + " - " + _card.ProductName;
            cardStatusComboBox.SelectedIndex = (_card.CardStatusId == null ? 0 : (int)_card.CardStatusId - 1);
            actNumberTextBox.Text = _card.ActNumber;
            permissionCardNumberTextBox.Text = _card.PermissionCardNumber;
            isDepartment4ConfirmedCheckBox.IsChecked = _card.IsDepartment4Confirmed;
            isDepartment5ConfirmedCheckBox.IsChecked = _card.IsDepartment5Confirmed;
            isDepartment6ConfirmedCheckBox.IsChecked = _card.IsDepartment6Confirmed;
            isDepartment13ConfirmedCheckBox.IsChecked = _card.IsDepartment13Confirmed;
            isDepartment17ConfirmedCheckBox.IsChecked = _card.IsDepartment17Confirmed;
            isDepartment80ConfirmedCheckBox.IsChecked = _card.IsDepartment80Confirmed;
            isDepartment82ConfirmedCheckBox.IsChecked = _card.IsDepartment82Confirmed;

            orderSelectControl.Text = _card.Order
                + (!(string.IsNullOrWhiteSpace(_card.Direction)) ? (", " + _card.Direction) : "")
                + (!(string.IsNullOrWhiteSpace(_card.Cipher)) ? (", " + _card.Cipher) : "")
                + (!string.IsNullOrWhiteSpace(_card.ClientOrder) ? ", " + _card.ClientOrder : "");

            if (isConfirmed)
            {
                parentCardSelectControl.ShowClearButton = false;
                parentCardSelectControl.ShowChooseButton = false;
                numberTextBox.IsReadOnly = true;
                repairTypeComboBox.IsEnabled = false;
                departmentComboBox.IsEnabled = false;
                sourceComboBox.IsEnabled = false;
                dateDatePicker.IsEnabled = false;
                invoiceNumberTextBox.IsEnabled = false;
                productSelectControl.ShowClearButton = false;
                productSelectControl.ShowChooseButton = false;
                orderSelectControl.ShowClearButton = false;
                orderSelectControl.ShowChooseButton = false;
                factoryNumberTextBox.IsReadOnly = true;
                stageTextBox.IsReadOnly = true;
                reasonForRepairTextBox.IsReadOnly = true;
                cardStatusComboBox.IsEnabled = false;
                actNumberTextBox.IsReadOnly = true;
                saveButton.Visibility = Visibility.Collapsed;
            }

            bool cardIsConfirmedAndProductCodesAreTheSame = _requestRepo.CardIsConfirmedAndProductCodesAreTheSame(_card);
            cardIsConfirmedAndProductCodesAreTheSameCheckBox.IsChecked = cardIsConfirmedAndProductCodesAreTheSame;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(numberTextBox.Text))
            {
                errors += "Не указано поле \"Номер\".\n";
            }
            else if (!Regex.IsMatch(numberTextBox.Text, @"^[0-9]+/[0-9]+\.[0-9]{2}$"))
            {
                errors += "Поле \"Номер\" должно иметь формат \"любые цифры/номер цеха.две цифры года\" (например \"290/17.20\").\n";
            }

            if (repairTypeComboBox.SelectedIndex == -1)
                errors += "Не указано поле \"Тип ремонта\".\n";

            if (departmentComboBox.SelectedIndex == -1)
            {
                errors += "Не указано поле \"Цех\".\n";
            }
            else
            {
                if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                {
                    if (Convert.ToInt32(departmentComboBox.Text) != AuthorizationService.User.Department)
                    {
                        errors += "В поле \"Цех\" должен быть указан ваш цех.\n";
                    }
                }
            }

            if (sourceComboBox.SelectedIndex == -1)
                errors += "Не указано поле \"Откуда поступил\".\n";

            if (dateDatePicker.SelectedDate == null)
                errors += "Не указано поле \"Дата поступления\".\n";

            if (string.IsNullOrWhiteSpace(invoiceNumberTextBox.Text))
                errors += "Не указано поле \"Номер накладной\".\n";

            if (string.IsNullOrWhiteSpace(productSelectControl.Text))
                errors += "Не указано поле \"Продукт\".\n";

            if (string.IsNullOrWhiteSpace(factoryNumberTextBox.Text))
                errors += "Не указано поле \"Заводской номер\".\n";

            if (!string.IsNullOrWhiteSpace(stageTextBox.Text))
                if (!Regex.IsMatch(stageTextBox.Text, @"^0000-[0-9]+$"))
                    errors += "Поле \"Заказ производства\" должно иметь формат \"0000-любые цифры\" (например \"0000-150\").\n";

            if (cardStatusComboBox.SelectedIndex == -1)
                errors += "Не указано поле \"Статус\".\n";

            if (!string.IsNullOrWhiteSpace(actNumberTextBox.Text))
                if (!Regex.IsMatch(actNumberTextBox.Text, @"^[0-9]+/[0-9]+\.[0-9]{2}$"))
                    errors += "Поле \"Номер акта\" должно иметь формат \"любые цифры/номер цеха.две цифры года\" (например \"290/17.20\").\n";

            if (!string.IsNullOrWhiteSpace(permissionCardNumberTextBox.Text))
                if (!Regex.IsMatch(permissionCardNumberTextBox.Text, @"^[0-9]+/[0-9]+\.[0-9]{2}$"))
                    errors += "Поле \"Номер карты разрешения\" должно иметь формат \"любые цифры/номер цеха.две цифры года\" (например \"290/17.20\").\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            _card.Number = numberTextBox.Text;
            _card.RepairTypeId = repairTypeComboBox.SelectedIndex + 1;
            _card.Department = Convert.ToInt32(departmentComboBox.Text);
            _card.FactoryNumber = factoryNumberTextBox.Text;
            _card.Stage = stageTextBox.Text;
            _card.InvoiceNumber = invoiceNumberTextBox.Text;
            _card.Date = dateDatePicker.SelectedDate;
            _card.Source = Convert.ToInt32(sourceComboBox.Text);
            _card.ReasonForRepair = reasonForRepairTextBox.Text;
            _card.CardStatusId = cardStatusComboBox.SelectedIndex + 1;
            _card.ActNumber = actNumberTextBox.Text;
            _card.PermissionCardNumber = permissionCardNumberTextBox.Text;
            _card.IsDepartment4Confirmed = (bool)isDepartment4ConfirmedCheckBox.IsChecked;
            _card.IsDepartment5Confirmed = (bool)isDepartment5ConfirmedCheckBox.IsChecked;
            _card.IsDepartment6Confirmed = (bool)isDepartment6ConfirmedCheckBox.IsChecked;
            _card.IsDepartment13Confirmed = (bool)isDepartment13ConfirmedCheckBox.IsChecked;
            _card.IsDepartment17Confirmed = (bool)isDepartment17ConfirmedCheckBox.IsChecked;
            _card.IsDepartment80Confirmed = (bool)isDepartment80ConfirmedCheckBox.IsChecked;
            _card.IsDepartment82Confirmed = (bool)isDepartment82ConfirmedCheckBox.IsChecked;

            if (_card.Id == _card.ParentId)
            {
                MessageBox.Show("Невозможно использовать выбранную карту ремонта в качестве головной, потому что карта ремонта не может быть головной по отношению к самой себе");
                return;
            }

            var originalCard = _repo.Get(_card.Id);
            if (originalCard.ParentId != null)
                if (_repo.IsConfirmed((int)originalCard.ParentId, 1))
                    if (_card.ParentId != originalCard.ParentId)
                    {
                        MessageBox.Show("Вы не можете открепить эту карту от родительской, потому что родительская карта утверждена");
                        return;
                    }

            var cardWithSuchNumber = _repo.FindByNumber(_card.Number);
            if (cardWithSuchNumber != null)
                if (cardWithSuchNumber.Id != _card.Id)
                {
                    MessageBox.Show("Карта ремонта с таким номером уже существует");
                    return;
                }

            if (!string.IsNullOrWhiteSpace(_card.ActNumber))
                if (_repo.IsCardWithActNumber(_card))
                {
                    MessageBox.Show("Карта с таким номером акта уже существует");
                    return;
                }

            _repo.Update(_card);

            Refresh();
        }

        private void ProductSelectControl_Click()
        {
            var chooseProductWindow = new SelectProductWindow();

            if (chooseProductWindow.ShowDialog() == true)
            {
                productSelectControl.Text = chooseProductWindow.Product.Code + " - " + chooseProductWindow.Product.Name;
                _card.ProductId = chooseProductWindow.Product.Id;
                _card.ProductCode = chooseProductWindow.Product.Code;
                _card.ProductName = chooseProductWindow.Product.Name;

                orderSelectControl.Text = "";
                _card.Order = null;
                _card.Direction = null;
                _card.Cipher = null;
                _card.ClientOrder = null;
            }
        }

        private void ProductSelectControl_Clear()
        {
            productSelectControl.Text = "";
            _card.ProductId = null;
            _card.ProductCode = null;
            _card.ProductName = null;

            orderSelectControl.Text = "";
            _card.Order = null;
            _card.Direction = null;
            _card.Cipher = null;
            _card.ClientOrder = null;
        }

        private void OrderSelectControl_Click()
        {
            var chooseOrderWindow = new SelectOrderWindow(_card.ProductCode);
            if (chooseOrderWindow.ShowDialog() == true)
            {
                var orderInfo = chooseOrderWindow.Order;

                orderSelectControl.Text = orderInfo.Number
                    + (!(string.IsNullOrWhiteSpace(orderInfo.Direction)) ? (", " + orderInfo.Direction) : "")
                    + (!(string.IsNullOrWhiteSpace(orderInfo.Cipher)) ? (", " + orderInfo.Cipher) : "")
                    + (!string.IsNullOrWhiteSpace(orderInfo.ClientOrder) ? ", " + orderInfo.ClientOrder : "");

                _card.Order = orderInfo.Number;
                _card.Direction = orderInfo.Direction;
                _card.Cipher = orderInfo.Cipher;
                _card.ClientOrder = orderInfo.ClientOrder;
            }
        }

        private void OrderSelectControl_Clear()
        {
            orderSelectControl.Text = "";

            _card.Order = null;
            _card.Direction = null;
            _card.Cipher = null;
            _card.ClientOrder = null;
        }

        private void ParentCardSelectControl_Click()
        {
            var chooseCardWindow = new SelectCardWindow();
            if (chooseCardWindow.ShowDialog() == true)
            {
                var parentCard = chooseCardWindow.Card;

                parentCardSelectControl.Text = $"{parentCard.Number}, {parentCard.RepairType.Name}, {parentCard.ProductCode}, {parentCard.ProductName}";

                _card.ParentId = parentCard.Id;
            }
        }

        private void ParentCardSelectControl_Clear()
        {
            parentCardSelectControl.Text = "";
            _card.ParentId = null;
        }

        private void addRequestButton_Click(object sender, RoutedEventArgs e)
        {
            new RequestWindow(null).ShowDialog();
        }

    }
}
