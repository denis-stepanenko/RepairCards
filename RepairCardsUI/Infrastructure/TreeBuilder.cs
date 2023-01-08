using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsUI.Infrastructure
{
    public class TreeBuilder
    {
        private int _counter = 0;

        private List<ProductRelation> _relations;

        public List<TreeProduct> Get(List<ProductRelation> relations)
        {
            _relations = relations;

            var roots = GetChildren(null);
            Build(roots);
            CalculateCountAll(roots);
            return roots;
        }

        void Build(List<TreeProduct> products)
        {
            foreach (var product in products)
            {
                var children = GetChildren(product);
                children.ForEach(x => x.Parent = product);
                product.Children.AddRange(children);

                Build(product.Children);
            }
        }

        List<TreeProduct> GetChildren(TreeProduct product)
        {
            return _relations.Where(x => x.ParentCode == product?.Code && x.ParentType == product?.Type)
                            .Select(x => new TreeProduct
                            {
                                Id = _counter++,
                                Code = x.Code,
                                Name = x.Name,
                                Count = x.Count,
                                TechWaste = x.TechWaste,
                                AssemblyDepartment = x.AssemblyDepartment?.PadLeft(3, '0'),
                                Route = x.Route,
                                IsAssembly = x.IsAssembly,
                                TypeName = x.TypeName,
                                Type = x.Type
                            })
                            .ToList();
        }

        void CalculateCountAll(List<TreeProduct> products)
        {
            if (products.Count == 0) return;

            products.ForEach(x => x.CountAll = GetCountAll(x));

            var children = products.SelectMany(x => x.Children).ToList();

            CalculateCountAll(children);
        }

        decimal GetCountAll(TreeProduct product)
        {
            if (product == null) return 1;

            return product.Count * (1 + product.TechWaste) * GetCountAll(product.Parent);
        }
    }
}
