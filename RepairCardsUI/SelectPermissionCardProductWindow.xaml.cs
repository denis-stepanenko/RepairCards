using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectPermissionCardProductWindow : Window
    {
        private readonly ProductRepo _repo = new ProductRepo();

        public Product Product { get; set; }

        public SelectPermissionCardProductWindow()
        {
            InitializeComponent();
            Refresh();
        }

        void Refresh()
        {
            var products = _repo.GetAll();
            productsRadDataPager.Source = products;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (Product)productsRadGridView.SelectedItem;
            if (product == null) return;

            Product = product;
            DialogResult = true;
        }
    }
}
