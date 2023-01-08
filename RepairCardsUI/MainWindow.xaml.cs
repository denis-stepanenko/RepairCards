using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace RepairCardsUI
{
    public partial class MainWindow : Window
    {
        private readonly NotificationRepo _repo = new NotificationRepo();

        public MainWindow()
        {
            InitializeComponent();
            Title += $" ({AuthorizationService.User.Name})";
            
            Navigate(@"Pages\CardsPage.xaml");

            if (AuthorizationService.User.RoleId == 2)
            {
                usersTextBlock.Visibility = Visibility.Visible;
            }

            if (new[] { 2, 4 }.Contains(AuthorizationService.User.RoleId))
            {
                unlockedPeriodsTextBlock.Visibility = Visibility.Visible;
            }

            var notifications = _repo.Get(Environment.UserName.ToString()).ToList();

            foreach (Notification notification in notifications)
            {
                var dialog = MessageBox.Show(notification.MessageBody.ToString(), "Информация об изменениях", MessageBoxButton.YesNo);

                if (dialog == MessageBoxResult.Yes)
                {
                    _repo.WriteStatusReadNotification(Environment.UserName.ToString(), notification.Id);
                }
            }
        }

        private void CardsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\CardsPage.xaml");
        private void OperationsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\OperationsPage.xaml");
        private void ExecutorsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\ExecutorsPage.xaml");
        private void TemplatesHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\TemplatesPage.xaml");
        private void RequestsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\RequestsPage.xaml");
        private void ReportsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\ReportsPage.xaml");
        private void utilitiesHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\UtilitiesPage.xaml");
        private void usersHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\UsersPage.xaml");
        private void unlockedPeriodsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\UnlockedPeriodsPage.xaml");
        private void exportRequestsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\ExportRequestsPage.xaml");
        private void requestsToCreateStagesIn1CHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\RequestsToCreateStagesIn1CPage.xaml");
        private void permissionCardsHyperlink_Click(object sender, RoutedEventArgs e) => Navigate(@"Pages\PermissionCardsPage.xaml");

        void Navigate(string url)
        {
            mainFrame.Source = new Uri(url, UriKind.Relative);
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string name);

        private void helpHyperlink_Click(object sender, RoutedEventArgs e)
        {
            string path = "help.chm";
            DeleteFile(path + ":Zone.Identifier");
            Process.Start(path);
        }
    }
}
