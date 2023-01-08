using AgileObjects.AgileMapper.Extensions;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Data;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardPurchasedProductWindow : Window
    {
        private readonly CardPurchasedProductRepo _cardProductRepo = new CardPurchasedProductRepo();
        private readonly CardPurchasedProductOperationRepo _cardProductOperationRepo = new CardPurchasedProductOperationRepo();

        private readonly int _productId;
        private readonly string _productCode;
        private CardPurchasedProduct _cardPurchasedProduct;

        public CardPurchasedProductWindow(int productId, string productCode)
        {
            InitializeComponent();
            _productId = productId;
            _productCode = productCode;

            if (!new[] { 2, 3, 4, 5, 6, 7, 8 }.Contains(AuthorizationService.User.RoleId))
            {
                countNumericUpDown.IsReadOnly = true;
                acceptButton.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                countNumericUpDown.IsReadOnly = true;
                countNumericUpDown.HideUpDownButtons = true;
            }

            if (AuthorizationService.User.RoleId != 6)
            {
                confirmButton.Visibility = Visibility.Collapsed;
                unconfirmButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var operations = _cardProductOperationRepo.GetAllByProduct(_productId);
            operationsRadGridView.ItemsSource = operations;
            _cardPurchasedProduct = _cardProductRepo.Get(_productId);

            codeTextBlock.Text = _cardPurchasedProduct.Code;
            nameTextBlock.Text = _cardPurchasedProduct.Name;
            countNumericUpDown.Value = _cardPurchasedProduct.Count;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseOperationWindow = new PurchasedProductSelectOperationWindow(_productId, _productCode);
            chooseOperationWindow.ShowDialog();
            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<CardPurchasedProductOperation>();
            if (operations.Count() == 0) return;

            if (operations.Any(x => _cardProductOperationRepo.Get(x.Id).IsConfirmed))
            {
                MessageBox.Show("Некоторые операции утверждены отк. Невозможно редактировать");
                return;
            }

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            operations.ForEach(x => _cardProductOperationRepo.Delete(x.Id));

            Refresh();

        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (countNumericUpDown.Value == null)
                errors += "Не указано поле \"Количество\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            _cardPurchasedProduct.Count = (int)countNumericUpDown.Value;

            _cardProductRepo.Update(_cardPurchasedProduct);

            Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var operation = operationsRadGridView.SelectedItem as CardPurchasedProductOperation;
            if (operation == null) return;

            if (operation.Department != AuthorizationService.User.Department && AuthorizationService.User.RoleId != 2)
            {
                MessageBox.Show("Вы не можете изменить эту операцию, потому что она пренадлежит другому цеху");
                return;
            }

            if (_cardProductOperationRepo.Get(operation.Id).IsConfirmed)
            {
                MessageBox.Show("Операция утверждена отк. Невозможно редактировать");
                return;
            }

            new CardPurchasedProductOperationWindow(operation.Id).ShowDialog();
            Refresh();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsRadGridView.SelectedItems.Cast<CardPurchasedProductOperation>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;

            foreach (var item in items)
            {
                var operation = _cardProductOperationRepo.Get(item.Id);
                if (operation.IsConfirmed) continue;

                _cardProductOperationRepo.Confirm(item, user.Id, user.Name);
            }

            Refresh();
        }

        private void unconfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsRadGridView.SelectedItems.Cast<CardPurchasedProductOperation>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;

            foreach (var item in items)
            {
                var operation = _cardProductOperationRepo.Get(item.Id);
                if (operation.IsConfirmed && operation.ConfirmUserId != user.Id) continue;

                _cardProductOperationRepo.Unconfirm(item);
            }

            Refresh();
        }
    }
}
