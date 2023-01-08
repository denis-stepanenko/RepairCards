using System.Collections.Generic;
using System.ComponentModel;

namespace RepairCardsUI.Infrastructure
{
    public class TreeProduct : INotifyPropertyChanged
    {
        public TreeProduct()
        {
            Children = new List<TreeProduct>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int Number { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal CountAll { get; set; }
        public decimal TechWaste { get; set; }
        public string Route { get; set; }
        public string AssemblyDepartment { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public bool HasChangedComposition { get; set; }
        public bool IsOvercoatingRequired { get; set; }
        public bool IsAssembly { get; set; }

        public string IsOvercoatingRequiredYesNo => IsOvercoatingRequired ? "Да" : "";

        public TreeProduct Parent { get; set; }
        public List<TreeProduct> Children { get; set; }

        private decimal _count;
        public decimal Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        private bool _isChecked;
        public bool IsChecked 
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
