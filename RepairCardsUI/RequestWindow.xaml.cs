using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class RequestWindow : Window
    {
        private readonly RequestRepo _repo = new RequestRepo();

        private int? _requestId;
        private Request _request;

        public RequestWindow(int? requestId)
        {
            InitializeComponent();
            _requestId = requestId;
            
            if (AuthorizationService.User.RoleId != 8)
            {
                repairProductSelectControl.ShowClearButton = false;
                repairProductSelectControl.ShowChooseButton = false;
                repairOrderSelectControl.IsEnabled = false;
            }

            if (AuthorizationService.User.RoleId != 10)
            {
                constructorToggleSwitch.IsEnabled = false;
            }

            if (AuthorizationService.User.RoleId != 11)
            {
                contractNumberTextBox.IsReadOnly = true;
            }

            if (requestId != null)
            {
                Refresh();
            }
            else
            {
                _request = new Request();

                if (!new[] { 8, 10, 11 }.Contains(AuthorizationService.User.RoleId) && !_request.Constructor)
                {
                    responseGroupBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Refresh()
        {
            _request = _repo.Get((int)_requestId);

            productTextBox.Text = _request.ProductCode + " - " + _request.ProductName;
            repairTypeComboBox.SelectedIndex = (int)_request.RepairTypeId - 1;
            factoryNumberTextBox.Text = _request.ProductFactoryNumber;
            shortOrderTextBox.Text = _request.ShortOrder;
            cardSelectControl.Text = _request.CardNumber;
            contractNumberTextBox.Text = _request.ContractNumber;

            repairProductSelectControl.Text = _request.RepairCode + " - " + _request.RepairName;

            repairOrderSelectControl.Text = _request.RepairOrder
                + (!(string.IsNullOrWhiteSpace(_request.RepairDirection)) ? (", " + _request.RepairDirection) : "")
                + (!(string.IsNullOrWhiteSpace(_request.RepairCipher)) ? (", " + _request.RepairCipher) : "")
                + (!string.IsNullOrWhiteSpace(_request.RepairClientOrder) ? ", " + _request.RepairClientOrder : "");

            constructorToggleSwitch.IsChecked = _request.Constructor;

            if (_request.Constructor)
            {
                cardSelectControl.ShowClearButton = false;
                cardSelectControl.ShowChooseButton = false;
                repairTypeComboBox.IsEnabled = false;
                shortOrderTextBox.IsReadOnly = true;
                factoryNumberTextBox.IsReadOnly = true;
                repairOrderSelectControl.ShowClearButton = false;
                repairOrderSelectControl.ShowChooseButton = false;
                repairProductSelectControl.ShowClearButton = false;
                repairProductSelectControl.ShowChooseButton = false;
                contractNumberTextBox.IsReadOnly = true;
            }


            if(AuthorizationService.User.Department != _request.Department)
            {
                cardSelectControl.ShowClearButton = false;
                cardSelectControl.ShowChooseButton = false;
                repairTypeComboBox.IsEnabled = false;
                shortOrderTextBox.IsReadOnly = true;
                factoryNumberTextBox.IsReadOnly = true;
            }

            if (!new[] { 8, 10, 11 }.Contains(AuthorizationService.User.RoleId) && !_request.Constructor)
            {
                responseGroupBox.Visibility = Visibility.Collapsed;
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(cardSelectControl.Text))
                errors += "Не указано поле \"Карта\".\n";

            if (string.IsNullOrWhiteSpace(shortOrderTextBox.Text))
                errors += "Не указано поле \"5-значный заказ\".\n";

            if (AuthorizationService.User.RoleId == 8)
            {
                if (_request.RepairCode == null)
                    errors += "Не указано поле \"Продукт\".\n";

                if (string.IsNullOrWhiteSpace(repairOrderSelectControl.Text))
                    errors += "Не указано поле \"Заказ\".\n";
            }

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            if (_repo.IsThereRequestForSuchCard(_request))
            {
                MessageBox.Show("Заявка на выбранную карту уже была создана ранее");
                return;
            }

            if (_requestId == null)
            {
                _request.Department = AuthorizationService.User.Department;
                _request.ShortOrder = shortOrderTextBox.Text;
                _request.Constructor = (bool)constructorToggleSwitch.IsChecked;
                _request.ContractNumber = contractNumberTextBox.Text;

                _requestId = _repo.Add(_request);
            }
            else
            {
                _request.ShortOrder = shortOrderTextBox.Text;
                _request.Constructor = (bool)constructorToggleSwitch.IsChecked;
                _request.ContractNumber = contractNumberTextBox.Text;

                _repo.Update(_request);
            }

            Close();
        }

        private void RepairProductSelectControl_Click()
        {
            var chooseProductWindow = new SelectProductWindow();

            if (chooseProductWindow.ShowDialog() == true)
            {
                repairProductSelectControl.Text = chooseProductWindow.Product.Code + " - " + chooseProductWindow.Product.Name;
                _request.RepairCode = chooseProductWindow.Product.Code;
                _request.RepairName = chooseProductWindow.Product.Name;

                repairOrderSelectControl.Text = "";
                _request.RepairOrder = null;
                _request.RepairDirection = null;
                _request.RepairCipher = null;
                _request.RepairClientOrder = null;
            }
        }

        private void RepairProductSelectControl_Clear()
        {
            repairProductSelectControl.Text = "";
            _request.RepairCode = null;
            _request.RepairName = null;

            repairOrderSelectControl.Text = "";
            _request.RepairOrder = null;
            _request.RepairDirection = null;
            _request.RepairCipher = null;
            _request.RepairClientOrder = null;
        }

        private void RepairOrderSelectControl_Click()
        {
            var chooseOrderWindow = new SelectOrderWindow(_request.RepairCode);
            if (chooseOrderWindow.ShowDialog() == true)
            {
                var orderInfo = chooseOrderWindow.Order;

                repairOrderSelectControl.Text = orderInfo.Number
                    + (!(string.IsNullOrWhiteSpace(orderInfo.Direction)) ? (", " + orderInfo.Direction) : "")
                    + (!(string.IsNullOrWhiteSpace(orderInfo.Cipher)) ? (", " + orderInfo.Cipher) : "")
                    + (!string.IsNullOrWhiteSpace(orderInfo.ClientOrder) ? ", " + orderInfo.ClientOrder : "");

                _request.RepairOrder = orderInfo.Number;
                _request.RepairDirection = orderInfo.Direction;
                _request.RepairCipher = orderInfo.Cipher;
                _request.RepairClientOrder = orderInfo.ClientOrder;
            }
        }

        private void RepairOrderSelectControl_Clear()
        {
            repairOrderSelectControl.Text = "";

            _request.RepairOrder = null;
            _request.RepairDirection = null;
            _request.RepairCipher = null;
            _request.RepairClientOrder = null;
        }

        private void ConstructorToggleSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
            if (constructorToggleSwitch.IsChecked == true)
            {
                cardSelectControl.ShowClearButton = false;
                cardSelectControl.ShowChooseButton = false;
                repairTypeComboBox.IsEnabled = false;
                shortOrderTextBox.IsReadOnly = true;
                factoryNumberTextBox.IsReadOnly = true;
                repairOrderSelectControl.ShowClearButton = false;
                repairOrderSelectControl.ShowChooseButton = false;
                repairProductSelectControl.ShowClearButton = false;
                repairProductSelectControl.ShowChooseButton = false;
            }
            else if(constructorToggleSwitch.IsChecked == false && AuthorizationService.User.RoleId == 8)
            {
                repairOrderSelectControl.ShowClearButton = true;
                repairOrderSelectControl.ShowChooseButton = true;
                repairProductSelectControl.ShowClearButton = true;
                repairProductSelectControl.ShowChooseButton = true;
            }
        }

        private void CardSelectControl_Click()
        {
            var chooseCardWindow = new SelectCardWindow();
            if (chooseCardWindow.ShowDialog() == true)
            {
                var card = chooseCardWindow.Card;
                _request.CardNumber = chooseCardWindow.Card.Number;
                cardSelectControl.Text = $"{card.Number}, {card.ProductCode}, {card.RepairType.Name}";

                productTextBox.Text = card.ProductCode + " - " + card.ProductName;
                repairTypeComboBox.SelectedIndex = (int)card.RepairTypeId - 1;
                factoryNumberTextBox.Text = card.FactoryNumber;

                _request.ProductCode = card.ProductCode;
                _request.ProductName = card.ProductName;
                _request.RepairTypeId = (int)card.RepairTypeId;
                _request.ProductFactoryNumber = card.FactoryNumber;
            }
        }

        private void CardSelectControl_Clear()
        {
            _request.CardNumber = null;
            cardSelectControl.Text = "";

            productTextBox.Text = null;
            repairTypeComboBox.SelectedIndex = -1;
            factoryNumberTextBox.Text = null;

            _request.ProductCode = null;
            _request.ProductName = null;
            _request.RepairTypeId = 0;
            _request.ProductFactoryNumber = null;
        }
    }
}
