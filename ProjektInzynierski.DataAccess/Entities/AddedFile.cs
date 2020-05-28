using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.Entities
{
    public class AddedFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int InquiryId { get; set; }
    }
}
