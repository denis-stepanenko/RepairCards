using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RepairCardsUI
{
    public partial class AuthorizationWindow : Window
    {
        private readonly UserRepo _repo = new UserRepo();

        private readonly SettingsService _settingsService = new SettingsService();

        private IEnumerable<User> _unfilteredUsers;
        private CollectionView _filteredUsers;

        public AuthorizationWindow()
        {
            InitializeComponent();

            //if (Environment.UserName.Contains("asup20"))
            //{
            //    AuthorizationService.User = new User { Name = "Тест", Department = 40, RoleId = 2 };

            //    var mainWindow = new MainWindow();
            //    mainWindow.Show();
            //    Close();
            //}

            Refresh();
        }

        void Refresh()
        {
            _unfilteredUsers = _repo.GetAll();

            _filteredUsers = (CollectionView)CollectionViewSource.GetDefaultView(_unfilteredUsers);
            usersListBox.ItemsSource = _filteredUsers;

            // Загружаем настройки
            var settings = _settingsService.Load();

            var idx = usersListBox.Items.IndexOf(_unfilteredUsers.Where(x => x.Id == settings.LastUserId).FirstOrDefault());
            usersListBox.SelectedIndex = idx;
            usersListBox.ScrollIntoView(usersListBox.SelectedItem);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _filteredUsers.Filter = u => ((User)u).Name.IndexOf(filterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = (User)usersListBox.SelectedItem;

            if (user == null) return;

            if (string.IsNullOrWhiteSpace(passwordPasswordBox.Password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            bool isValidPassword = false;

            isValidPassword = _repo.IsCorrectPassword(user.Id, passwordPasswordBox.Password);

            passwordPasswordBox.Clear();

            if (!isValidPassword)
            {
                MessageBox.Show("Введен неверный пароль");
                return;
            }

            AuthorizationService.User = user;

            // Сохраняем настройки
            var settings = _settingsService.Load();
            settings.LastUserId = user.Id;
            _settingsService.Save(settings);

            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
