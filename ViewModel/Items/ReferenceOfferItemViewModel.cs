using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace ViewModel.Items
{
    public class ReferenceOfferItemViewModel : VievModelBase
    {
        private int _Id;
        public ReferenceOfferItemViewModel(int id, string displayReferenceOffer)
        {
            Id = id;
            DisplayReferenceOffer = displayReferenceOffer;
        }


        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _DisplayReferenceOffer;

        public string DisplayReferenceOffer
        {
            get { return _DisplayReferenceOffer; }
            set { _DisplayReferenceOffer = value; }
        }
    }
}
