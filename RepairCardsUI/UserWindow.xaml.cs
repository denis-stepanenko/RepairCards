using MahApps.Metro.Controls;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class UserWindow : MetroWindow
    {
        private readonly UserRepo _repo = new UserRepo();
        private readonly RoleRepo _roleRepo = new RoleRepo();
        private readonly WorkerRepo _workerRepo = new WorkerRepo();

        private int? _userId;
        private User _user;

        public UserWindow(int? userId)
        {
            InitializeComponent();
            _userId = userId;

            Refresh();

            var workers = _workerRepo.GetAll();
            nameComboBox.ItemsSource = workers;
        }

        string GeneratePwd()
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return string.Join("", Enumerable.Range(1, 6).Select(x => chars[random.Next(chars.Length)]));
        }

        private void generatePwdButton_Click(object sender, RoutedEventArgs e) => passwordTextBox.Text = GeneratePwd();

        void Refresh()
        {
            var roles = _roleRepo.GetAll();
            roleComboBox.ItemsSource = roles;

            if (_userId == null)
            {
                passwordTextBox.Text = GeneratePwd();
                return;
            }

            _user = _repo.Get((int)_userId);

            roleComboBox.SelectedItem = roles.Where(x => x.Id == _user.RoleId).SingleOrDefault();

            nameComboBox.Text = _user.Name;
            departmentNumericUpDown.Value = _user.Department;
            passwordTextBox.Text = _user.Password;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(nameComboBox.Text))
                errors += "Не указано поле \"Наименование\".\n";

            if (departmentNumericUpDown.Value == null)
                errors += "Не указано поле \"Подразделение\".\n";   

            if (roleComboBox.SelectedItem == null)
                errors += "Не указано поле \"Роль\".\n";

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
                errors += "Не указано поле \"Пароль\".\n";

            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            if (_userId == null)
            {
                var role = roleComboBox.SelectedItem as Role;
                var newUser = new User
                {
                    Name = nameComboBox.Text,
                    Department = Convert.ToInt32(departmentNumericUpDown.Value),
                    Password = passwordTextBox.Text,
                    RoleId = role.Id
                };

                _userId = _repo.Add(newUser);
            }
            else
            {
                _user.Name = nameComboBox.Text;
                _user.Department = Convert.ToInt32(departmentNumericUpDown.Value);
                _user.Password = passwordTextBox.Text;

                var role = roleComboBox.SelectedItem as Role;
                _user.RoleId = role.Id;

                _repo.Update(_user);
            }

            Close();
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            var name = nameComboBox.Text;
            var password = passwordTextBox.Text;

            Print($"{name}\n{password}");
        }

        public static void Print(string text)
        {
            var document = new PrintDocument();
            document.PrintPage += (s, e) =>
            {
                e.Graphics.DrawString(text, new Font("Arial", 16),
                    new SolidBrush(Color.Black),
                    new RectangleF(0, 0,
                    document.DefaultPageSettings.PrintableArea.Width, document.DefaultPageSettings.PrintableArea.Height));
            };

            var dialog = new System.Windows.Forms.PrintDialog
            {
                AllowSomePages = true,
                Document = document
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                document.Print();
        }

        private void copyPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var password = passwordTextBox.Text;
            Clipboard.SetDataObject(password);
        }
    }
}
