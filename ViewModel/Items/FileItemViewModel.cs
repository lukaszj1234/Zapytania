using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace ViewModel.Items
{
    public class FileItemViewModel : VievModelBase
    {
        private string _displayFile;
        private int _Id;

        //private string _displayInquiry;

        public FileItemViewModel (int id, string displayFile)
        {
            Id = id;
            DisplayFile = displayFile; 
            //DisplayInquiry = displayInquiry;
        }

        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        public string DisplayFile
        {
            get { return _displayFile; }
            set { _displayFile = value;
                OnPropertyChanged();
            }
        }


        //public string DisplayInquiry
        //{
        //    get { return _displayInquiry; }
        //    set
        //    {
        //        _displayInquiry = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}
