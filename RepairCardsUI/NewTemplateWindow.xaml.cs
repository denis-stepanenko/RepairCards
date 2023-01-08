using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Windows;

namespace RepairCardsUI
{
    public partial class NewTemplateWindow : Window
    {
        private readonly TemplateRepo _repo = new TemplateRepo();

        public NewTemplateWindow()
        {
            InitializeComponent();
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

            var newTemplate = new Template
            {
                Name = nameTextBox.Text,
                Department = AuthorizationService.User.Department,
                Description = descriptionTextBox.Text
            };

            int id = _repo.Add(newTemplate);
            var templateWindow = new TemplateWindow(id);
            Close();
            templateWindow.ShowDialog();
        }
    }
}
