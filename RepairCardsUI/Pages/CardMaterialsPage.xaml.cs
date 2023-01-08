using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class CardMaterialsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardMaterialRepo _cardMaterialRepo = new CardMaterialRepo();
        private readonly int _cardId;

        public CardMaterialsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                addFromAnotherCardButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                addByProductButton.IsEnabled = false;
            }

            Refresh();
        }

        void Refresh()
        {
            var materials = _cardMaterialRepo.GetAllByCard(_cardId);
            materialsRadGridView.ItemsSource = materials;
            bool isConfirmed = _cardRepo.IsConfirmed(_cardId, 1);

            if (isConfirmed)
            {
                addButton.Visibility = Visibility.Collapsed;
                addFromAnotherCardButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                addByProductButton.IsEnabled = false;
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseMaterialWindow = new SelectMaterialWindow();
            if (chooseMaterialWindow.ShowDialog() != true) return;

            _cardMaterialRepo.Add(new CardMaterial
            {
                CardId = _cardId,
                Code = chooseMaterialWindow.Material.Code,
                Name = chooseMaterialWindow.Material.Name,
                Size = chooseMaterialWindow.Material.Size,
                Type = chooseMaterialWindow.Material.Type,
                Price = chooseMaterialWindow.Material.Price,
                Count = chooseMaterialWindow.Count,
                UnitId = chooseMaterialWindow.Material.UnitId,
                Department = AuthorizationService.User.Department
            });

            Refresh();
        }

        void Edit()
        {
            var material = (CardMaterial)materialsRadGridView.SelectedItem;
            if (material != null)
            {
                var cardMaterialWindow = new CardMaterialWindow(material.Id);
                cardMaterialWindow.ShowDialog();
                Refresh();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) => Edit();
        private void materialsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Edit();

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var materials = materialsRadGridView.SelectedItems.Cast<CardMaterial>().ToList();
            if (materials.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (materials.Any(x => x.Department != AuthorizationService.User.Department && x.Department != null))
            {
                MessageBox.Show("Среди выбранных материалов не все материалы принадлежат вашему цеху");
                return;
            }

            materials.ForEach(x => _cardMaterialRepo.Delete(x.Id));

            Refresh();
        }

        private void addFromAnotherCardButton_Click(object sender, RoutedEventArgs e)
        {
            new SelectMaterialFromAnotherCardWindow(_cardId).ShowDialog();
            Refresh();
        }

        private void addByProductButton_Click(object sender, RoutedEventArgs e)
        {
            new SelectProductMaterialsWindow(_cardId).ShowDialog();
            Refresh();
        }
    }
}
