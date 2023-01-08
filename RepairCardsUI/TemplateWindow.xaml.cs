using AgileObjects.AgileMapper.Extensions;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class TemplateWindow : Window
    {
        private readonly TemplateRepo _templateRepo = new TemplateRepo();
        private readonly TemplateOperationRepo _templateOperationRepo = new TemplateOperationRepo();

        private readonly int _templateId;
        private Template _template;
        private IEnumerable<TemplateOperation> _operations;

        public TemplateWindow(int templateId)
        {
            InitializeComponent();
            _templateId = templateId;

            RefreshTemplate();
        }

        void RefreshTemplate()
        {
            _template = _templateRepo.Get(_templateId);

            templateIdTextBlock.Text = _template.Id.ToString();
            departmentTextBlock.Text = _template.Department.ToString();
            nameTextBox.Text = _template.Name;
            descriptionTextBox.Text = _template.Description;

            if (!new[] { 2, 3, 4, 5, 7, 8 }.Contains(AuthorizationService.User.RoleId) || 
                (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department) && _template.Department != AuthorizationService.User.Department))
            {
                acceptButton.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                addFromCardButton.IsEnabled = false;
                nameTextBox.IsReadOnly = true;
                descriptionTextBox.IsReadOnly = true;
                moveUpButton.IsEnabled = false;
                moveDownButton.IsEnabled = false;
            }

            RefreshOperations();
        }

        void RefreshOperations()
        {
            _operations = _templateOperationRepo.GetAll(_templateId);

            operationsRadGridView.ItemsSource = _operations;
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

            _template.Name = nameTextBox.Text;
            _template.Description = descriptionTextBox.Text;

            _templateRepo.Update(_template);

            RefreshTemplate();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var templateChooseOperationWindow = new TemplateSelectOperationWindow(_templateId);
            templateChooseOperationWindow.ShowDialog();
            RefreshTemplate();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.Cast<TemplateOperation>();
            if (operations.Count() == 0) return;

            var dialog = MessageBox.Show("Удалить выбранные записи?", "Внимание", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes) return;

            operations.ForEach(x => _templateOperationRepo.Delete(x.Id));

            RefreshTemplate();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => RefreshOperations();

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var templateOperation = (TemplateOperation)operationsRadGridView.SelectedItem;
            if (templateOperation != null)
            {
                var templateOperationWindow = new TemplateOperationWindow(templateOperation.Id);
                templateOperationWindow.ShowDialog();
                RefreshTemplate();
            }
        }

        private void addFromCardButton_Click(object sender, RoutedEventArgs e)
        {
            new AddOperationsFromCard(_templateId).ShowDialog();
            RefreshOperations();
        }

        private void moveUpButton_Click(object sender, RoutedEventArgs e)
        {
            var item = operationsRadGridView.SelectedItem as TemplateOperation;
            if (item == null) return;

            var previousItem = _operations.ToArray().Reverse().SkipWhile(x => x != item).Skip(1).FirstOrDefault();
            if (previousItem == null) return;

            _templateOperationRepo.Swap(item, previousItem);

            RefreshOperations();
        }

        private void moveDownButton_Click(object sender, RoutedEventArgs e)
        {
            var item = operationsRadGridView.SelectedItem as TemplateOperation;
            if (item == null) return;

            var nextItem = _operations.SkipWhile(x => x != item).Skip(1).FirstOrDefault();
            if (nextItem == null) return;

            _templateOperationRepo.Swap(item, nextItem);

            RefreshOperations();
        }
    }
}
