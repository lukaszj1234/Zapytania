using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Wrapper
{
    public class SendedInquiryWrapper : ModelWrapper<SendedInquiry>
    {
        public SendedInquiryWrapper(SendedInquiry model) : base(model)
        {

        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(nameof(Name)); }
            set
            {
                SetValue(value);
            }
        }

        public string Description
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

    }
}
