using AgileObjects.AgileMapper.Extensions;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Data;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardRepairProductWindow : Window
    {
        private readonly CardRepairProductRepo _cardProductRepo = new CardRepairProductRepo();
        private readonly CardRepairProductOperationRepo _cardProductOperationRepo = new CardRepairProductOperationRepo();

        private readonly int _productId;
        private CardRepairProduct _product;

        public CardRepairProductWindow(int productId, string productCode)
        {
            InitializeComponent();
            _productId = productId;

            if (!new[] { 2, 3, 4, 5, 6, 7, 8 }.Contains(AuthorizationService.User.RoleId))
            {
                countNumericUpDown.IsReadOnly = true;
                acceptButton.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                countNumericUpDown.IsReadOnly = true;
                countNumericUpDown.HideUpDownButtons = true;
                editButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var items = _cardProductOperationRepo.GetAll(_productId);
            itemsRadGridView.ItemsSource = items;
            _product = _cardProductRepo.Get(_productId);

            codeTextBlock.Text = _product.Code;
            nameTextBlock.Text = _product.Name;
            countNumericUpDown.Value = _product.Count;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new RepairProductSelectRepairOperationWindow(_productId).ShowDialog();

            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var items = itemsRadGridView.SelectedItems.Cast<CardRepairProductOperation>();
            if (items.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            items.ForEach(x => _cardProductOperationRepo.Delete(x.Id));

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

            _product.Count = (int)countNumericUpDown.Value;

            _cardProductRepo.Update(_product);

            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var item = itemsRadGridView.SelectedItem as CardRepairProductOperation;
            if (item == null) return;

            if (item.Department != AuthorizationService.User.Department && AuthorizationService.User.RoleId != 2)
            {
                MessageBox.Show("Вы не можете изменить эту операцию, потому что она пренадлежит другому цеху");
                return;
            }

            new CardRepairProductOperationWindow(item.Id).ShowDialog();

            Refresh();
        }

    }
}
