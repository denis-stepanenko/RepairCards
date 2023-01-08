using System.Collections.Generic;

namespace RepairCardsUI.Infrastructure
{
    public class CardDto
    {
        public CardDto()
        {
            Children = new List<CardDto>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string Direction { get; set; }
        public string Cipher { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FactoryNumber { get; set; }

        public List<CardDto> Children { get; set; }
    }
}
