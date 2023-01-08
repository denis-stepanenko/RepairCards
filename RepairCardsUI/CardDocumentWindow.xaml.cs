using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardDocumentWindow : Window
    {
        private readonly CardDocumentRepo _repo = new CardDocumentRepo();

        private readonly int _cardDocumentId;
        private CardDocument _cardDocument;

        public CardDocumentWindow(int cardDocumentId)
        {
            InitializeComponent();
            _cardDocumentId = cardDocumentId;

            Refresh();
        }

        void Refresh()
        {
            _cardDocument = _repo.Get((int)_cardDocumentId);

            nameTextBox.Text = _cardDocument.Name;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                errors += "Не указано поле \"Наименование\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            _cardDocument.Name = nameTextBox.Text;

            _repo.Update(_cardDocument);

            Close();
        }
    }
}
