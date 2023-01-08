using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class NewCardDocumentWindow : Window
    {
        private readonly CardDocumentRepo _repo = new CardDocumentRepo();

        private readonly int _cardId;

        public NewCardDocumentWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
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

            var newCardDocument = new CardDocument
            {
                CardId = _cardId,
                Name = nameTextBox.Text
            };

            _repo.Add(newCardDocument);

            Close();
        }
    }
}
