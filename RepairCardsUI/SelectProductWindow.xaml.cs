using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;
using System.Windows.Input;

namespace RepairCardsUI
{
    public partial class SelectProductWindow : Window
    {
        private readonly ProductRepo _repo = new ProductRepo();
        
        public SelectProductWindow()
        {
            InitializeComponent();

            Refresh();
        }

        public Product Product { get; set; }

        void Refresh()
        {
            var products = _repo.GetAll();
            productsRadDataPager.Source = products;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        void Ok()
        {
            var product = (Product)productsRadGridView.SelectedItem;
            if (product != null)
            {
                Product = product;
                DialogResult = true;
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e) => Ok();

        private void productsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Ok();
    }
}
