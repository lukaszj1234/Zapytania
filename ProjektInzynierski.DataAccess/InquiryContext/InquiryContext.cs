using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.InquiryContext
{
   public class InquiryContext : DbContext 
    {
        public InquiryContext() : base("ProjektInzynierskiDb")
        {
        }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<AddedFile> Files { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ReferenceOffer> ReferenceOffers { get; set; }
        public DbSet<SendedInquiry> SendedInquiries { get; set; }
        public DbSet<DrawingOutOfDate> DrawingsOutOfDate { get; set; }
        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<DrawingIndustry> DrawingIndustries { get; set; }
    }
}
