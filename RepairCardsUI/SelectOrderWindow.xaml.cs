using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectOrderWindow : Window
    {
        private readonly OrderRepo _repo = new OrderRepo();

        private readonly string _productCode;

        public SelectOrderWindow(string productCode)
        {
            InitializeComponent();
            _productCode = productCode;

            Refresh();
        }

        public Order Order { get; set; }

        void Refresh()
        {
            var orders = _repo.GetAllByProduct(_productCode);
            ordersRadGridView.ItemsSource = orders;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var order = (Order)ordersRadGridView.SelectedItem;
            if (order != null)
            {
                Order = order;
                DialogResult = true;
            }
        }
    }
}
