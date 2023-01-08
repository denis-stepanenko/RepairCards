using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsUI.Infrastructure
{
    public class CardTreeBuilder
    {
        private List<Card> _relations;
        private Func<Card, int?> _selector;

        public List<CardDto> GetTree(List<Card> relations, int rootId, Func<Card, int?> selector)
        {
            _relations = relations;
            _selector = selector;

            var roots = GetProductsByParent(rootId);

            Build(roots);
            return roots;
        }

        List<CardDto> GetProductsByParent(int? id) =>
            _relations.Where(x => _selector(x) == id).Select(x => new CardDto
            {
                Id = x.Id,
                Number = x.Number,
                Direction = x.Direction,
                Cipher = x.Cipher,
                Name = x.ProductName,
                FactoryNumber = x.FactoryNumber,
                Code = x.ProductCode,
                
            }).ToList();


        List<CardDto> Build(List<CardDto> items)
        {
            if (items.Count == 0) return null;

            var result = new List<CardDto>();

            foreach (var item in items)
            {
                var children = GetProductsByParent(item.Id);
                
                item.Children.AddRange(children);
                result.AddRange(item.Children);
            }

            return Build(result);
        }
    }
}
