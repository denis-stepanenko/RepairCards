using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardMaterialWindow : Window
    {
        private readonly CardMaterialRepo _repo = new CardMaterialRepo();

        private readonly int _cardMaterialId;
        private CardMaterial _material;

        public CardMaterialWindow(int cardMaterialId)
        {
            InitializeComponent();
            _cardMaterialId = cardMaterialId;

            Refresh();
        }

        void Refresh()
        {
            _material = _repo.Get(_cardMaterialId);

            codeTextBlock.Text = _material.Code;
            nameTextBlock.Text = _material.Name;
            sizeTextBlock.Text = _material.Size;
            typeTextBlock.Text = _material.Type;
            unitNameTextBlock.Text = _material.Unit?.Name;
            countNumericUpDown.Value = (double)_material.Count;
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

            _material.Count = (decimal)countNumericUpDown.Value;

            _repo.Update(_material);

            Close();
        }
    }
}
