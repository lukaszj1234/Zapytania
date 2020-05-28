using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LookupItem
    {
        public int Id { get; set; }
        public string DisplayInquiry { get; set; }
        public string DisplayIndustry { get; set; }

        public string DisplayDescription { get; set; }
    }

    public class NullLookupItem : LookupItem 
    
    {
     public new int? Id { get { return null; } }
    }

}
