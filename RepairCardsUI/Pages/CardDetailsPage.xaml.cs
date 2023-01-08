using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Pages
{
    public partial class CardDetailsPage : Page
    {
        private readonly CardRepo _cardRepo = new CardRepo();
        private readonly CardDetailsRepo _cardDetailsRepo = new CardDetailsRepo();

        private readonly int _cardId;
        private Card _card;
        private CardDetails _cardDetails;

        public CardDetailsPage()
        {
            InitializeComponent();
            _cardId = (int)PageNavigationHelper.Parameter;

            Refresh();
        }

        void Refresh()
        {
            _card = _cardRepo.Get(_cardId);
            _cardDetails = _cardDetailsRepo.Get(_cardId);

            bool isConfirmed = _cardRepo.IsConfirmed(_cardId, 1);
            bool isInternalDefectsConfirmed = _cardRepo.IsConfirmed(_cardId, 2);
            bool isMalfunctionsConfirmed = _cardRepo.IsConfirmed(_cardId, 3);
            bool isExternalDefectsConfirmed = _cardRepo.IsConfirmed(_cardId, 4);

            if (!new[] { 2, 3, 4, 5, 7 }.Contains(AuthorizationService.User.RoleId) ||
                (new [] { 4, 5, 6, 13, 17, 80, 82 }.Contains(AuthorizationService.User.Department) 
                && _card.Department != AuthorizationService.User.Department))
            {
                internalDefectsTextBox.IsReadOnly = true;
                externalDefectsTextBox.IsReadOnly = true;
                malfunctionsTextBox.IsReadOnly = true;
                causeOfProductFailure.IsReadOnly = true;
                scopeOfRepair.IsReadOnly = true;
                commissionReport.IsReadOnly = true;
                saveButton.IsEnabled = false;
            }

            internalDefectsTextBox.Text = _cardDetails.InternalDefects;
            externalDefectsTextBox.Text = _cardDetails.ExternalDefects;
            malfunctionsTextBox.Text = _cardDetails.Malfunctions;
            causeOfProductFailure.Text = _cardDetails.CauseOfProductFailure;
            scopeOfRepair.Text = _cardDetails.ScopeOfRepair;
            commissionReport.Text = _cardDetails.CommissionReport;


            if (isConfirmed)
            {
                internalDefectsTextBox.IsReadOnly = true;
                externalDefectsTextBox.IsReadOnly = true;
                malfunctionsTextBox.IsReadOnly = true;
                causeOfProductFailure.IsReadOnly = true;
                scopeOfRepair.IsReadOnly = true;
                commissionReport.IsReadOnly = true;
                saveButton.IsEnabled = false;
            }

            if (isInternalDefectsConfirmed)
                internalDefectsTextBox.IsReadOnly = true;

            if (isMalfunctionsConfirmed)
                malfunctionsTextBox.IsReadOnly = true;

            if (isExternalDefectsConfirmed)
                externalDefectsTextBox.IsReadOnly = true;

            if (isInternalDefectsConfirmed && isExternalDefectsConfirmed && isMalfunctionsConfirmed)
                saveButton.Visibility = Visibility.Collapsed;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _cardDetails.InternalDefects = internalDefectsTextBox.Text;
            _cardDetails.ExternalDefects = externalDefectsTextBox.Text;
            _cardDetails.Malfunctions = malfunctionsTextBox.Text;
            _cardDetails.CauseOfProductFailure = causeOfProductFailure.Text;
            _cardDetails.ScopeOfRepair = scopeOfRepair.Text;
            _cardDetails.CommissionReport = commissionReport.Text;

            _cardDetailsRepo.Update(_cardDetails);
            
            Refresh();
        }
    }
}
