using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RepairCardsUI
{
    public partial class SelectExecutorWindow : Window
    {
        private readonly ExecutorRepo _repo = new ExecutorRepo();
        
        public SelectExecutorWindow()
        {
            InitializeComponent();
            Refresh();
        }

        public Executor Executor { get; set; }

        void Refresh()
        {
            IEnumerable<Executor> executors;

            if (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department))
                executors = _repo.GetAllByDepartment(AuthorizationService.User.Department);
            else
                executors = _repo.GetAll();

            executorsRadGridView.ItemsSource = executors;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => Refresh();

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var executor = (Executor)executorsRadGridView.SelectedItem;
            if (executor != null)
            {
                Executor = executor;
                DialogResult = true;
            }
        }
    }
}
