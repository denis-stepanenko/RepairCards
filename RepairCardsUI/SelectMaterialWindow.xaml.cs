using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;
using System.Windows.Input;

namespace RepairCardsUI
{
    public partial class SelectMaterialWindow : Window
    {
        private readonly MaterialRepo _repo = new MaterialRepo();
        
        public SelectMaterialWindow()
        {
            InitializeComponent();

            Refresh();
        }

        public Material Material { get; set; }
        public decimal Count { get; set; }

        void Refresh()
        {
            var materials = _repo.GetAll();
            materialsRadDataPager.Source = materials;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();


        void Ok()
        {
            var material = (Material)materialsRadGridView.SelectedItem;
            if (material != null)
            {
                Material = material;
                Count = (decimal)countNumericUpDown.Value;
                DialogResult = true;
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e) => Ok();
        private void materialsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Ok();
    }
}
