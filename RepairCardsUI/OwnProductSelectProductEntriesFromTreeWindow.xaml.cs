using RepairCardsDapperData.Data;
using RepairCardsDapperData.Models;
using RepairCardsUI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Data;

namespace RepairCardsUI
{
    public partial class OwnProductSelectProductEntriesFromTreeWindow : Window
    {
        private readonly ProductRepo _productRepo = new ProductRepo();
        private readonly ProductRelationRepo _productEntryRepo = new ProductRelationRepo();
        private readonly CardOwnProductRepo _cardOwnProductRepo = new CardOwnProductRepo();
        private readonly ProductOperationRepo _productOperationRepo = new ProductOperationRepo();
        private readonly CardOwnProductOperationRepo _cardOwnProductOperationRepo = new CardOwnProductOperationRepo();
        private readonly TreeBuilder _builder = new TreeBuilder();

        private List<TreeProduct> _roots;

        private readonly int _cardId;

        public OwnProductSelectProductEntriesFromTreeWindow(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;

            Refresh();
        }

        void Refresh()
        {
            var items = _productRepo.GetAll();
            productsRadDataPager.Source = items;
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            var product = productsDataGrid.SelectedItem as Product;
            if (product == null) return;

            var count = countNumericUpDown.Value;
            if (count == null) return;

            // Получаем состав
            var relations = _productEntryRepo.GetProductRelations(product.Code, (int)count);

            // Строим дерево
            _roots = _builder.Get(relations.ToList());
            treeListView.ItemsSource = _roots;

            // Выбираем все продукты
            SelectAll(_roots);
        }

        List<TreeProduct> productsWithIncompleteComposition;

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Находим узлы, состав которых изменился
            productsWithIncompleteComposition = GetProductsWithIncompleteComposition(_roots);

            // Удаляем невыбранные узлы в дереве
            RemoveNotSelectedParentsRecursively(_roots);

            // Добавляем то, что осталось от дерева
            AddTreeRecursively(_roots, null);

            Close();
        }

        #region Recursions

        void RemoveNotSelectedParentsRecursively(List<TreeProduct> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (!products[i].IsChecked)
                {
                    products.InsertRange(i + 1, products[i].Children);
                    products.RemoveAt(i);
                    i--;
                }
                else
                    RemoveNotSelectedParentsRecursively(products[i].Children);
            }
        }

        List<TreeProduct> GetProductsWithIncompleteComposition(List<TreeProduct> products)
        {
            var result = new List<TreeProduct>();

            void FindParents(TreeProduct item)
            {
                if (item == null) return;
                
                result.Add(item);

                FindParents(item.Parent);
            }

            void FindUnchekedProducts(List<TreeProduct> items)
            {
                foreach (var item in items)
                {
                    if (!item.IsChecked)
                        FindParents(item);

                    FindUnchekedProducts(item.Children);
                }
            }

            FindUnchekedProducts(products);

            return result;
        }

        void AddTreeRecursively(List<TreeProduct> products, int? parentId)
        {
            foreach (var product in products)
            {
                int id = _cardOwnProductRepo.Add(new CardOwnProduct
                {
                    CardId = _cardId,
                    ParentId = parentId,
                    Code = product.Code,
                    Name = product.Name,
                    Count = (int)product.CountAll,
                    Route = product.Route,
                    HasChangedComposition = productsWithIncompleteComposition.Contains(product)
                });

                var operations = _productOperationRepo.GetProductOperations(product.Code, product.Route);

                var newOpeartions = operations.Select(x => new CardOwnProductOperation
                {
                    CardOwnProductId = id,
                    Code = x.Code,
                    Name = x.Name,
                    Labor = x.Labor,
                    Type = 0,
                    Department = x.Department
                }).ToList();

                newOpeartions.ForEach(x => _cardOwnProductOperationRepo.Add(x));

                AddTreeRecursively(product.Children, id);
            }
        }

        #endregion

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var item = (e.Source as CheckBox).DataContext as TreeProduct;
            if (item.IsChecked)
                Check(item);
            else
                Uncheck(item);
        }

        void Check(TreeProduct item)
        {
            item.IsChecked = true;
            foreach (var child in item.Children)
            {
                child.IsChecked = true;
                Check(child);
            }
        }

        void Uncheck(TreeProduct item)
        {
            item.IsChecked = false;
            foreach (var child in item.Children)
            {
                child.IsChecked = false;
                Uncheck(child);
            }
        }

        void SelectAll(List<TreeProduct> items)
        {
            if (items.Count == 0) return;
            items.ForEach(x => x.IsChecked = true);
            SelectAll(items.SelectMany(x => x.Children).ToList());
        }
    }
}
