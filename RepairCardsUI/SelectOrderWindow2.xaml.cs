using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectOrderWindow2 : Window
    {
        private readonly OrderRepo _repo = new OrderRepo();

        public SelectOrderWindow2()
        {
            InitializeComponent();

            var items = _repo.GetAll();
            itemsRadGridView.ItemsSource = items;
        }

        public Order Item { get; set; }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (Order)itemsRadGridView.SelectedItem;
            if (item == null) return;

            Item = item;
            DialogResult = true;
        }
    }
}
