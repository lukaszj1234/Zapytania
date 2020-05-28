using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.Entities
{
    public class Inquiry
    {
        [Key()]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? IndustryId { get; set; }
        public string Time { get; set; }
        public string Path { get; set; }
        public ICollection<AddedFile> Files { get; set; }
        public ICollection<SendedInquiry> SendedInquiries { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<ReferenceOffer> ReferenceOffers { get; set; }
    }
}

