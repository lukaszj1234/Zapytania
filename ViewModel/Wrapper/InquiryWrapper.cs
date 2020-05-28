using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ViewModel.Wrapper
{
    public class InquiryWrapper : ModelWrapper<Inquiry>
    {
        public InquiryWrapper(Inquiry model): base (model)
        {

        }
        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(nameof(Name)); }
            set {
                SetValue(value);
            }
        }

        public string Industry
        {
            get { return GetValue<string>(nameof(Industry)); }
            set
            {
                SetValue(value);
            }
        }

        public int? IndustryId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }


        public string Path
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public string Time
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public int RowNumber
        {
            get { return GetValue<int>();}
            set { SetValue(value); }
        }

        protected override IEnumerable<string> Validate(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.IsNullOrEmpty(Name))
                    {
                        yield return "Pole Wymagane";
                    }
                    break;
            }
        }
    }
}
