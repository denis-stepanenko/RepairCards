using AgileObjects.AgileMapper.Extensions;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Data;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardOwnProductWindow : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOwnProductRepairOperationRepo _cardOwnProductRepairOperationRepo = new CardOwnProductRepairOperationRepo();
        private readonly CardOwnProductRepo _cardProductRepo = new CardOwnProductRepo();
        private readonly CardOwnProductOperationRepo _cardProductOperationRepo = new CardOwnProductOperationRepo();

        private readonly int _productId;
        private readonly string _productCode;
        private readonly string _route;
        public CardOwnProduct CardOwnProduct;

        public CardOwnProductWindow(int productId, string productCode, string route)
        {
            InitializeComponent();
            _productId = productId;
            _productCode = productCode;
            _route = route;

            if (!new[] { 2, 3, 4, 5, 6, 7, 8 }.Contains(AuthorizationService.User.RoleId))
            {
                countNumericUpDown.IsReadOnly = true;
                acceptButton.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Collapsed;
                addRepairOperationButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                deleteRepairOperationButton.Visibility = Visibility.Collapsed;
                countNumericUpDown.IsReadOnly = true;
                countNumericUpDown.HideUpDownButtons = true;
                editButton.Visibility = Visibility.Collapsed;
                copyMenuItem.IsEnabled = false;
                pasteMenuItem.IsEnabled = false;
            }

            if (AuthorizationService.User.RoleId != 6)
            {
                confirmButton.Visibility = Visibility.Collapsed;
                unconfirmButton.Visibility = Visibility.Collapsed;
                confirmRepairOperationButton.Visibility = Visibility.Collapsed;
                unconfirmRepairOperationButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var operations = _cardProductOperationRepo.GetAllByProduct(_productId);
            var repairOperations = _cardOwnProductRepairOperationRepo.GetAll(_productId);
            operationsRadGridView.ItemsSource = operations;
            repairOperationsRadGridView.ItemsSource = repairOperations;
            CardOwnProduct = _cardProductRepo.Get(_productId);

            codeTextBlock.Text = CardOwnProduct.Code;
            nameTextBlock.Text = CardOwnProduct.Name;
            countNumericUpDown.Value = CardOwnProduct.Count;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseOperationWindow = new OwnProductSelectOperationWindow(_productId, _productCode, _route);
            chooseOperationWindow.ShowDialog();
            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<CardOwnProductOperation>();
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

        private void RefreshRepairOperationsButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddRepairOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var ownProductChooseRepairOperationWindow = new OwnProductSelectRepairOperationWindow(_productId);
            ownProductChooseRepairOperationWindow.ShowDialog();
            Refresh();
        }

        private void DeleteRepairOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = repairOperationsRadGridView.SelectedItems.Cast<CardOwnProductRepairOperation>();
            if (operations.Count() == 0) return;

            var card = _cardRepo.Get(CardOwnProduct.CardId);

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

            if (operations.Any(x => _cardOwnProductRepairOperationRepo.Get(x.Id).IsConfirmed))
            {
                MessageBox.Show("Некоторые операции утверждены отк. Невозможно редактировать");
                return;
            }

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            operations.ForEach(x => _cardOwnProductRepairOperationRepo.Delete(x.Id));

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

            var operations = _cardOwnProductRepairOperationRepo.GetAll(_productId);

            var card = _cardRepo.Get(CardOwnProduct.CardId);

            if ((card.IsDepartment4Confirmed && operations.Any(x => x.Department == 4)) ||
                (card.IsDepartment5Confirmed && operations.Any(x => x.Department == 5)) ||
                (card.IsDepartment6Confirmed && operations.Any(x => x.Department == 6)) ||
                (card.IsDepartment13Confirmed && operations.Any(x => x.Department == 13)) ||
                (card.IsDepartment17Confirmed && operations.Any(x => x.Department == 17)) ||
                (card.IsDepartment80Confirmed && operations.Any(x => x.Department == 80)) ||
                (card.IsDepartment82Confirmed && operations.Any(x => x.Department == 82)))
            {
                MessageBox.Show("ООИОТ поставили утверждение на цеха операций, которые добавлены в этом ДСЕ. Изменение количества у ДСЕ повлекло бы изменение трудоемкости по утвержденному цеху");
                return;
            }

            CardOwnProduct.Count = (int)countNumericUpDown.Value;

            _cardProductRepo.Update(CardOwnProduct);

            Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var operation = operationsRadGridView.SelectedItem as CardOwnProductOperation;
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

            new CardOwnProductOperationWindow(operation.Id).ShowDialog();
            Refresh();
        }

        private void editRepairOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operation = repairOperationsRadGridView.SelectedItem as CardOwnProductRepairOperation;
            if (operation == null) return;

            if (operation.Department != AuthorizationService.User.Department && AuthorizationService.User.RoleId != 2)
            {
                MessageBox.Show("Вы не можете изменить эту операцию, потому что она пренадлежит другому цеху");
                return;
            }

            if (_cardOwnProductRepairOperationRepo.Get(operation.Id).IsConfirmed)
            {
                MessageBox.Show("Операция утверждена отк. Невозможно редактировать");
                return;
            }

            new CardOwnProductRepairOperationWindow(operation.Id).ShowDialog();

            Refresh();
        }

        private void copyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsRadGridView.SelectedItems.ToList();
            LocalClipboardService.Set(items);
        }

        private void pasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var items = LocalClipboardService.Get<CardOwnProductOperation>();
            items.ForEach(x => x.CardOwnProductId = _productId);

            items.ForEach(x => _cardProductOperationRepo.Add(x));

            Refresh();
        }

        private void copyRepairOperationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var items = repairOperationsRadGridView.SelectedItems.ToList();
            LocalClipboardService.Set(items);
        }

        private void pasteRepairOperationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var items = LocalClipboardService.Get<CardOwnProductRepairOperation>();
            items.ForEach(x => x.CardOwnProductId = _productId);

            items.ForEach(x => _cardOwnProductRepairOperationRepo.Add(x));

            Refresh();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsRadGridView.SelectedItems.Cast<CardOwnProductOperation>().ToList();
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
            var items = operationsRadGridView.SelectedItems.Cast<CardOwnProductOperation>().ToList();
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

        private void confirmRepairOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var items = repairOperationsRadGridView.SelectedItems.Cast<CardOwnProductRepairOperation>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;

            foreach (var item in items)
            {
                var operation = _cardOwnProductRepairOperationRepo.Get(item.Id);
                if (operation.IsConfirmed) continue;

                _cardOwnProductRepairOperationRepo.Confirm(item, user.Id, user.Name);
            }

            Refresh();
        }

        private void unconfirmRepairOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var items = repairOperationsRadGridView.SelectedItems.Cast<CardOwnProductRepairOperation>().ToList();
            if (items.Count == 0) return;

            var user = AuthorizationService.User;

            foreach (var item in items)
            {
                var operation = _cardOwnProductRepairOperationRepo.Get(item.Id);
                if (operation.IsConfirmed && operation.ConfirmUserId != user.Id) continue;

                _cardOwnProductRepairOperationRepo.Unconfirm(item);
            }

            Refresh();
        }
    }
}
