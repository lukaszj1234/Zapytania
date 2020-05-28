using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.Entities
{
    public class SendedInquiry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InquiryId { get; set; }
    }
}
