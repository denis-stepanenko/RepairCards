using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class CardChildCardsPage : Page
    {
        private readonly CardRepo _repo = new CardRepo();

        private readonly CardTreeBuilder _treeBuilder = new CardTreeBuilder();

        private readonly ServerFilteringHelper filteringHelper;

        private readonly int _cardId;

        public CardChildCardsPage()
        {
            InitializeComponent();

            _cardId = (int)PageNavigationHelper.Parameter;

            var card = _repo.Get(_cardId);

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId)
                || (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department) && card.Department != AuthorizationService.User.Department))
            {
                addButton.IsEnabled = false;
                addFromCardsButton.IsEnabled = false;
                removeButton.IsEnabled = false;
            }

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addDismantledCardButton.IsEnabled = false;
                removeDismantledCardButton.IsEnabled = false;
            }

            var isConfirmed = _repo.IsConfirmed((int)_cardId, 1);
            if (isConfirmed)
            {
                addButton.IsEnabled = false;
                addFromCardsButton.IsEnabled = false;
                removeButton.IsEnabled = false;
                addDismantledCardButton.IsEnabled = false;
                removeDismantledCardButton.IsEnabled = false;
            }

            filteringHelper = new ServerFilteringHelper(RefreshCards);

            Refresh();

            RefreshInstalledCards();

            RefreshCards();

            RefreshTrees();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var items = childCardsDataGrid.SelectedItems.OfType<Card>().ToList();
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                var parentCard = _repo.GetParentForInstalledProductCard(item);
                if (parentCard != null)
                {
                    MessageBox.Show($"Карта {item.Number} уже входит в {parentCard.Number} в качестве установленной.");
                    return;
                }
            }

            if (items.Any(x => x.Id == _cardId))
            {
                MessageBox.Show("Карта не может входить сама в себя");
                return;
            }

            items.ForEach(x => x.ParentId2 = _cardId);
            items.ForEach(x => _repo.Update(x));

            RefreshInstalledCards();
        }

        private void addFromCardsButton_Click(object sender, RoutedEventArgs e)
        {
            var items = cardsDataGrid.SelectedItems.OfType<Card>().ToList();
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                var parentCard = _repo.GetParentForInstalledProductCard(item);
                if (parentCard != null)
                {
                    MessageBox.Show($"Карта {item.Number} уже входит в {parentCard.Number} в качестве установленной.");
                    return;
                }
            }

            if (items.Any(x => x.Id == _cardId))
            {
                MessageBox.Show("Карта не может входить сама в себя");
                return;
            }

            items.ForEach(x => _repo.UpdateParentId2(x, _cardId));

            RefreshInstalledCards();
        }

        private void addDismantledCardButton_Click(object sender, RoutedEventArgs e)
        {
            var items = cardsDataGrid.SelectedItems.OfType<Card>().ToList();
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                var parentCard = _repo.GetParentForDismantledProductCard(item);
                if (parentCard != null)
                {
                    MessageBox.Show($"Карта {item.Number} уже входит в {parentCard.Number} в качестве демонтированной.");
                    return;
                }
            }

            if (items.Any(x => x.Id == _cardId))
            {
                MessageBox.Show("Карта не может входить сама в себя");
                return;
            }

            items.ForEach(x => _repo.UpdateParentId(x, _cardId));

            Refresh();
        }

        void Refresh()
        {
            var cards = _repo.GetAllByParent(_cardId);
            childCardsDataGrid.ItemsSource = cards;
        }

        void RefreshInstalledCards()
        {
            var items = _repo.GetAllInstalledCardsByParent(_cardId);
            installedCardsDataGrid.ItemsSource = items;
        }

        void RefreshCards()
        {
            var cards2 = _repo.GetAll(filterTextBox.Text);
            cardsDataGrid.ItemsSource = cards2;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var items = installedCardsDataGrid.SelectedItems.OfType<Card>().ToList();
            if (items.Count == 0) return;

            items.ForEach(x => _repo.UpdateParentId2(x, null));

            RefreshInstalledCards();
        }

        private void removeDismantledCardButton_Click(object sender, RoutedEventArgs e)
        {
            var items = childCardsDataGrid.SelectedItems.OfType<Card>().ToList();
            if (items.Count == 0) return;

            items.ForEach(x => _repo.UpdateParentId(x, null));

            Refresh();
        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e) => filteringHelper.Run();

        void RefreshTrees()
        {
            // Иерархия карт
            var cards = _repo.GetAllByParentRecursive(_cardId);
            var roots = _treeBuilder.GetTree(cards.ToList(), _cardId, x => x.ParentId);
            cardTreeTreeListView.ItemsSource = roots;

            var cards2 = _repo.GetAllInstalledProductCardsByParentRecursive(_cardId);
            var roots2 = _treeBuilder.GetTree(cards2.ToList(), _cardId, x => x.ParentId2);
            cardTreeWithSubtitutionsTreeListView.ItemsSource = roots2;
        }

        private void refreshTreesButton_Click(object sender, RoutedEventArgs e) => RefreshTrees();

        private void childCardsDataGrid_CopyingCellClipboardContent(object sender, Telerik.Windows.Controls.GridViewCellClipboardEventArgs e)
        {
            if (e.Cell != childCardsDataGrid.CurrentCellInfo)
                e.Cancel = true;
        }

        private void installedCardsDataGrid_CopyingCellClipboardContent(object sender, Telerik.Windows.Controls.GridViewCellClipboardEventArgs e)
        {
            if (e.Cell != installedCardsDataGrid.CurrentCellInfo)
                e.Cancel = true;
        }
    }
}
