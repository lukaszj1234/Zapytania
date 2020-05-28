using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace ViewModel.Items
{
    public class NavigationItemViewModel: VievModelBase
    {
        private string _displayInquiry;

        public NavigationItemViewModel(int id, string displayInquiry, string displayIndustry)
        {
            Id = id;
            DisplayInquiry = displayInquiry;
            DisplayIndustry = displayIndustry;
        }

        public int Id { get; }

        public string DisplayInquiry
        { 
            get { return _displayInquiry; }
            set {_displayInquiry = value;
                OnPropertyChanged();
            }
        }

        private string _displayIndustry;

        public string DisplayIndustry
        {
            get { return _displayIndustry; }
            set { _displayIndustry = value;
                OnPropertyChanged();
            }
        }


    }
}
