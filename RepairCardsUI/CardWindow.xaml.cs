using RepairCardsUI.Infrastructure;
using System;
using System.Windows;

namespace RepairCardsUI
{
    public partial class CardWindow : Window
    {
        public readonly int CardId;

        public CardWindow(int cardId)
        {
            InitializeComponent();
            CardId = cardId;
            PageNavigationHelper.Parameter = cardId;
            Navigate(@"Pages\CardPage.xaml");
        }

        private void cardHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardPage.xaml");
        private void DetailsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardDetailsPage.xaml");
        private void planOperationsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\PlanCardOperationsPage.xaml");
        private void FactOperationsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\FactCardOperationsPage.xaml");
        private void OwnProductsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardOwnProductsPage.xaml");
        private void repairProductsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardRepairProductsPage.xaml");
        private void PurchasedProductsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardPurchasedProductsPage.xaml");
        private void MaterialsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardMaterialsPage.xaml");
        private void DocumentsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardDocumentsPage.xaml");
        private void AgreementsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardConfirmationsPage.xaml");
        private void cardChildCardsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardChildCardsPage.xaml");

        void Navigate(string url)
        {
            mainFrame.Source = new Uri(url, UriKind.Relative);
        }

    }
}
