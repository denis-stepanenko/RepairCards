using AgileObjects.AgileMapper.Extensions;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace RepairCardsUI
{
    public partial class AddOperationsFromCard : Window
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardOperationRepo _cardOperationRepo = new CardOperationRepo();
        private readonly TemplateOperationRepo _templateOperationRepo = new TemplateOperationRepo();
        private readonly OperationRepo _operationRepo = new OperationRepo();

        private int _templateId;

        public AddOperationsFromCard(int templateId)
        {
            InitializeComponent();
            _templateId = templateId;

            cardsRadGridView.ItemsSource = _cardRepo.GetAll();
        }

        private void cardsRadGridView_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            var card = cardsRadGridView.SelectedItem as Card;
            if (card == null) return;

            operationsRadGridView.ItemsSource = _cardOperationRepo.GetAllByCard(card.Id);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            var operations = operationsRadGridView.SelectedItems.OfType<CardOperation>();
            if (operations.Count() == 0) return;

            var newOperations = operations.Select(x => new TemplateOperation
            {
                TemplateId = _templateId,
                Count = x.Count,
                OperationId = _operationRepo.FindByCode(x.Code).Id
            });

            newOperations.ForEach(x => _templateOperationRepo.Add(x));

            Close();
        }
    }
}
