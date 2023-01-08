using AgileObjects.AgileMapper.Extensions;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairCardsUI.Pages
{
    public partial class CardDocumentsPage : Page
    {
        private readonly CardDocumentRepo _cardDocumentRepo = new CardDocumentRepo();
        private readonly CardRepo _cardRepo = new CardRepo();

        private readonly int _cardId;

        public CardDocumentsPage()
        {
            InitializeComponent();

            _cardId = (int)PageNavigationHelper.Parameter;

            Refresh();

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        void Refresh()
        {
            var documents = _cardDocumentRepo.GetAllByCard(_cardId);
            bool isConfirmed = _cardRepo.IsConfirmed(_cardId, 1);
            documentsRadGridView.ItemsSource = documents;

            if (!new[] { 2, 3, 4, 5, 7, 9 }.Contains(AuthorizationService.User.RoleId) || isConfirmed)
            {
                addButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newCardDocumentWindow = new NewCardDocumentWindow(_cardId);
            newCardDocumentWindow.ShowDialog();
            Refresh();
        }

        void Open()
        {
            var document = (CardDocument)documentsRadGridView.SelectedItem;
            if (document != null)
            {
                var cardDocumentWindow = new CardDocumentWindow(document.Id);
                cardDocumentWindow.ShowDialog();
                Refresh();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => Open();
        private void documentsRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Open();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var documents = documentsRadGridView.SelectedItems.Cast<CardDocument>();
            if (documents.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            documents.ForEach(x => _cardDocumentRepo.Delete(x.Id));

            Refresh();
        }
    }
}
