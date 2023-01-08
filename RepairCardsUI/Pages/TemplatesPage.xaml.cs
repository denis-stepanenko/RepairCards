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
    public partial class TemplatesPage : Page
    {
        private readonly TemplateRepo _repo = new TemplateRepo();

        public TemplatesPage()
        {
            InitializeComponent();

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId))
            {
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        void Refresh()
        {
            var templates = _repo.GetAll();
            templatesRadGridView.ItemsSource = templates;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newTemplateWindow = new NewTemplateWindow();
            newTemplateWindow.ShowDialog();
            Refresh();
        }

        void Open()
        {
            var template = (Template)templatesRadGridView.SelectedItem;
            if (template == null) return;

            var templateWindow = new TemplateWindow(template.Id);
            templateWindow.ShowDialog();
            Refresh();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => Open();
        private void templatesRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Open();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var templates = templatesRadGridView.SelectedItems.Cast<Template>();
            if (templates.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                if (templates.Any(x => x.Department != AuthorizationService.User.Department))
                {
                    MessageBox.Show("Удаление шаблонов невозможно, потому что среди удаляемых шаблонов есть шаблоны, не принадлежащие вашему цеху");
                    return;
                }

            templates.ForEach(x => _repo.Delete(x.Id));

            Refresh();
        }
    }
}
