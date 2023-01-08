using Microsoft.Reporting.WinForms;
using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Data;
using RepairCardsUI.Infrastructure;
using RepairCardsUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace RepairCardsUI
{
    public partial class PermissionCardWindow : Window
    {
        private readonly EFContext _db;

        public PermissionCard Card { get; set; }
        public List<int> Departments { get; set; }

        private readonly SalariedEmployeeLaborCoefficientRepo _salariedEmployeeLaborCoefficientRepo = new SalariedEmployeeLaborCoefficientRepo();
        private readonly OrderRepo _orderRepo = new OrderRepo();

        public PermissionCardWindow(PermissionCard card, EFContext db)
        {
            InitializeComponent();
            DataContext = this;
            _db = db;
            Card = card;
            Departments = new List<int> { 55, 48 };

            var items = _salariedEmployeeLaborCoefficientRepo.GetAll();
            coefficientsDataGrid.ItemsSource = items;

            if (card == null)
            {
                int department = AuthorizationService.User.Department;
                var newNumber = db.GetNewNumber(department);

                Card = new PermissionCard { Number = newNumber, Department = AuthorizationService.User.Department, Date = DateTime.Now };
                db.PermissionCards.Add(Card);
            }
            else
                Refresh();
        }

        void Refresh()
        {
            _db.Entry(Card).Reload();

            Card.Products.ToList().ForEach(x => _db.Entry(x).State = EntityState.Detached);
            Card.PurchasedProducts.ToList().ForEach(x => _db.Entry(x).State = EntityState.Detached);
            Card.Materials.ToList().ForEach(x => _db.Entry(x).State = EntityState.Detached);
            Card.Operations.ToList().ForEach(x => _db.Entry(x).State = EntityState.Detached);

            _db.Entry(Card).Collection(x => x.Products).Load();
            _db.Entry(Card).Collection(x => x.PurchasedProducts).Load();
            _db.Entry(Card).Collection(x => x.Materials).Load();
            _db.Entry(Card).Collection(x => x.Operations).Load();
        }

        void Save()
        {
            if (string.IsNullOrWhiteSpace(Card.Number))
            {
                MessageBox.Show("Введите номер карты");
                return;
            }

            if (!Regex.IsMatch(Card.Number, @"^[0-9]+/[0-9]+\.[0-9]{2}$"))
            {
                MessageBox.Show("Номер карты должен иметь формат - любые цифры/номер цеха.две цифры года (например 290/17.20)");
                return;
            }

            if (_db.PermissionCards.Any(x => x.Number == Card.Number && x.Id != Card.Id))
            {
                MessageBox.Show("Карта с таким номером уже существует");
                return;
            }

            _db.SaveChanges();
        }

        public RelayCommand SaveCommand => new RelayCommand((x) => Save(), (o) =>
        {
            var user = AuthorizationService.User;
            return _db.ChangeTracker.HasChanges() 
            && !Card.IsConfirmed 
            && (new[] { 2, 3, 4, 5, 7 }.Contains(user.RoleId) || (new[] { 4, 5, 6, 13, 17, 80, 82 }.Contains(user.Department) && Card.Department == user.Department)); 
        });

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_db.ChangeTracker.HasChanges())
                if (MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes) Save();
                else
                {
                    _db.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList().ForEach(x => x.State = EntityState.Detached);
                    _db.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Deleted).ToList().ForEach(x => x.Reload());
                }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new SelectPermissionCardProductWindow();
            if (w.ShowDialog() == true)
            {
                Card.Products.Add(new PermissionCardProduct
                {
                    Code = w.Product.Code,
                    Name = w.Product.Name,
                    Count = 1
                });
            }
        }

        private void addPurchasedProductButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new SelectProductWindow();
            if (w.ShowDialog() == true)
            {
                Card.PurchasedProducts.Add(new PermissionCardPurchasedProduct { Code = w.Product.Code, Name = w.Product.Name, Count = 1 });
            }
        }

        private void addMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new SelectMaterialWindow();
            if (w.ShowDialog() == true)
            {
                Card.Materials.Add(new PermissionCardMaterial
                {
                    Code = w.Material.Code,
                    Name = w.Material.Name,
                    Count = w.Count,
                    Size = w.Material.Size,
                    Type = w.Material.Type,
                    UnitId = w.Material.UnitId,
                    Department = AuthorizationService.User.Department
                });
            }
        }

        private void addOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new SelectOperationWindow();
            if (w.ShowDialog() == true)
            {
                Card.Operations.Add(new PermissionCardOperation
                {
                    Code = w.Operation.Code,
                    Name = w.Operation.Name,
                    Labor = w.Operation.Labor,
                    UnitName = w.Operation.UnitName,
                    GroupName = w.Operation.GroupName,
                    Count = 1,
                    Price = w.Operation.Price,
                    Department = w.Operation.Department
                });
            }
        }

        private void removeMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var items = materialsDataGrid.SelectedItems.OfType<PermissionCardMaterial>();
            _db.PermissionCardMaterials.RemoveRange(items);
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            // Удаление объекта из коллекции в родительском объекте по умолчанию расценивается, как разрыв связи, а не как удаление
            var items = productsDataGrid.SelectedItems.OfType<PermissionCardProduct>();
            _db.PermissionCardProducts.RemoveRange(items);
        }

        private void removePurchasedProductButton_Click(object sender, RoutedEventArgs e)
        {
            var items = purchasedProductsDataGrid.SelectedItems.OfType<PermissionCardPurchasedProduct>();
            _db.PermissionCardPurchasedProducts.RemoveRange(items);
        }

        private void removeOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var items = operationsDataGrid.SelectedItems.OfType<PermissionCardOperation>();
            _db.PermissionCardOperations.RemoveRange(items);
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            var card = _db.PermissionCards
                .AsNoTracking()
                .Include(x => x.Products)
                .Include(x => x.PurchasedProducts)
                .Include(x => x.Materials)
                .Include(x => x.Operations)
                .FirstOrDefault(x => x.Id == Card.Id);

            var coefficient = coefficientsDataGrid.SelectedItem as SalariedEmployeeLaborCoefficient;

            if (coefficient == null) return;

            card.Operations.ToList().ForEach(x => {
                decimal coef = 1;
                
                switch (x.Department)
                {
                    case 4: coef = coefficient.Department4; break;
                    case 5: coef = coefficient.Department5; break;
                    case 6: coef = coefficient.Department6; break;
                    case 13: coef = coefficient.Department13; break;
                    case 17: coef = coefficient.Department17; break;
                    case 80: coef = coefficient.Department80; break;
                    case 82: coef = coefficient.Department82; break;
                };

                x.LaborWithCoefficient = x.Labor * x.Count / 100 * coef;
            
            });

            double civilianCHTS = civilianCHTSNumericUpDown.Value ?? 0;
            double militaryCHTS = militaryCHTSNumericUpDown.Value ?? 0;

            int? specificationType = _orderRepo.GetSpecificationType(Card.Cipher ?? Card.Direction);

            if (specificationType == null)
            {
                MessageBox.Show("Не найден тип спецификации направления деятельности");
                return;
            }

            var parameters = new ReportParameterCollection
            {
                new ReportParameter("CivilianCHTS", civilianCHTS.ToString()),
                new ReportParameter("MilitaryCHTS", militaryCHTS.ToString()),
                new ReportParameter("SpecificationType", specificationType.ToString()),
            };

            reportViewer.Reset();

            reportViewer.LocalReport.ReportEmbeddedResource = "RepairCardsUI.Reports.PermissionCardReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Card", new List<PermissionCard> { card }));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PurchasedProducts", card.PurchasedProducts));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Materials", card.Materials));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Products", card.Products));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Operations", card.Operations));
            reportViewer.LocalReport.SetParameters(parameters);

            reportViewer.RefreshReport();
        }

        private void selectOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new SelectOrderWindow2();
            if (w.ShowDialog() != true) return;

            Card.Direction = w.Item.Direction;
            Card.Cipher = w.Item.Cipher;
        }
    }
}
