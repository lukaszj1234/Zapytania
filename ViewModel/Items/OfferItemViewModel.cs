using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace ViewModel.Items
{
    public class OfferItemViewModel : VievModelBase
    {

        private int _Id;
        public OfferItemViewModel(int id, string displayOffer)
        {
            Id = id;
            DisplayOffer = displayOffer;
        }
       

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _DisplayOffer;

        public string DisplayOffer
        {
            get { return _DisplayOffer; }
            set { _DisplayOffer = value; }
        }


    }
}
