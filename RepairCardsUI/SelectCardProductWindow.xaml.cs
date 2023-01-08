using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectCardProductWindow : Window
    {
        private readonly ProductRepo _repo = new ProductRepo();
        
        public SelectCardProductWindow()
        {
            InitializeComponent();

            Refresh();
        }

        public Product Product { get; set; }
        public int Count { get; set; }

        void Refresh()
        {
            var products = _repo.GetAll();
            productsRadDataPager.Source = products;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (Product)productsRadGridView.SelectedItem;
            if (product != null)
            {
                Product = product;
                Count = (int)countNumericUpDown.Value;
                DialogResult = true;
            }
        }
    }
}
