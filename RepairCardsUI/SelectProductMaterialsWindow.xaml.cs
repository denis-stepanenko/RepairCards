using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using RepairCardsUI.Models;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RepairCardsUI
{
    public partial class SelectProductMaterialsWindow : Window
    {
        private readonly ProductRepo _productRepo = new ProductRepo();
        private readonly ProductMaterialRepo _productMaterialRepo = new ProductMaterialRepo();
        private readonly CardMaterialRepo _cardMaterialRepo = new CardMaterialRepo();

        private readonly int _cardId;

        private ICollectionView _productView;

        public SelectProductMaterialsWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;

            var products = _productRepo.GetAll();
            _productView = CollectionViewSource.GetDefaultView(products);
            _productView.Filter = i =>
            {
                var x = i as Product;
                return
                x.Code.ToLower().Contains(filterTextBox.Text.ToLower())
                || (x.Name ?? "").ToLower().Contains(filterTextBox.Text.ToLower());
            };
            productsDataGrid.ItemsSource = _productView;
        }


        private void productsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = productsDataGrid.SelectedItem as Product;
            if (product == null) return;

            var materials = _productMaterialRepo.GetAll(product.Code);
            materialsDataGrid.ItemsSource = materials;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            var materials = materialsDataGrid.SelectedItems.OfType<ProductMaterial>().ToList();
            if(materials.Count == 0) return;

            var newMaterials = materials.Select(x => new CardMaterial
            {
                CardId = _cardId,
                Code = x.Code,
                Name = x.Name,
                Size = x.Size,
                Type = x.Type,
                Count = x.Count,
                Price = x.Price,
                Department = AuthorizationService.User.Department,
                UnitId = x.UnitId
            }).ToList();

            newMaterials.ForEach(x => _cardMaterialRepo.Add(x));

            Close();
        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e) => _productView.Refresh();

        
    }
}
