using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectTemplateWindow : Window
    {
        private readonly TemplateRepo _repo = new TemplateRepo();

        public SelectTemplateWindow()
        {
            InitializeComponent();

            Refresh();
        }

        public Template CurrentTemplate { get; set; }

        void Refresh()
        {
            IEnumerable<Template> templates;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                templates = _repo.GetAllByDepartment(AuthorizationService.User.Department);
            else
                templates = _repo.GetAll();

            templatesRadGridView.ItemsSource = templates;

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var template = (Template)templatesRadGridView.SelectedItem;
            if (template != null)
            {
                CurrentTemplate = template;
                DialogResult = true;
            }
        }
    }
}
