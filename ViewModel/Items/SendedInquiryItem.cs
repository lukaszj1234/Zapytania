using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace ViewModel.Items
{
    public class SendedInquiryItem : VievModelBase
    {
        private string _DisplaySendedInquiry;
        private int _Id;
        private string _DisplayDescription;

        public SendedInquiryItem()
        {

        }

        public SendedInquiryItem(int id, string displaySendedInquiry, string description)
        {
            Id = id;
            DisplaySendedInquiry = displaySendedInquiry;
            DisplayDescription = description;
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string DisplaySendedInquiry
        {
            get { return _DisplaySendedInquiry; }
            set { _DisplaySendedInquiry = value; }
        }

        public string DisplayDescription
        {
            get { return _DisplayDescription; }
            set { _DisplayDescription = value; }
        }
    }
}
