﻿using System;

namespace RepairCardsDapperData.Models
{
    public class CardRepairProductOperation
    {
        public int Id { get; set; }
        public int CardRepairProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Count { get; set; }
        public decimal Labor { get; set; }
        public decimal LaborAll { get; set; }
        public DateTime Date { get; set; }
        public int Department { get; set; }
        public string UnitName { get; set; }
        public string GroupName { get; set; }

        public int ExecutorId { get; set; }
        public Executor Executor { get; set; }
    }
}
