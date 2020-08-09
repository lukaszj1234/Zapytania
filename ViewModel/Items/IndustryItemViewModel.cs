using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace ViewModel.Items
{
    class IndustryItemViewModel : VievModelBase
    {
        private string _displayIndustry;

        public IndustryItemViewModel(int id, string displayIndustry)
        {
            Id = id;
            DisplayIndustry = displayIndustry;
        }

        public int Id { get; }

        public string DisplayIndustry
        {
            get { return _displayIndustry; }
            set
            {
                _displayIndustry = value;
                OnPropertyChanged();
            }
        }
    }
}

