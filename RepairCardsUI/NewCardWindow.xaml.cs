using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace RepairCardsUI
{
    public partial class NewCardWindow : Window
    {
        private readonly CardRepo _repo = new CardRepo();
        private int _cardId;
        private Card _card;

        public NewCardWindow()
        {
            InitializeComponent();
            _card = new Card();
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
                    if (Convert.ToInt32(departmentComboBox.Text) != AuthorizationService.User.Department)
                        errors += "В поле \"Цех\" должен быть указан ваш цех.\n";
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
            _card.ActNumber = actNumberTextBox.Text;
            _card.PermissionCardNumber = permissionCardNumberTextBox.Text;

            _card.CardStatusId = 1;

            var cardWithSuchNumber = _repo.FindByNumber(_card.Number);
            if (cardWithSuchNumber != null)
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

            _cardId = _repo.Add(_card, AuthorizationService.User);

            var cardWindow = new CardWindow(_cardId);
            Close();
            cardWindow.ShowDialog();
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

    }
}
