using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories
{

    public class IndsutryRepository : IIndsutryRepository
    {
        private InquiryContext _context;
        public IndsutryRepository(InquiryContext context)
        {
            _context = context;
        }

        public void DeleteByIdAsync(int id)
        {
            var item = _context.Industries.First(p => p.Id == id);
            _context.Industries.Remove(item);
           var list= _context.Inquiries.Where(p => p.IndustryId == id).ToList();
            foreach (var inquiry in list)
            {
                inquiry.IndustryId = 0;
            }
            _context.SaveChanges();
        }

        public void Add(string name)
        {
            _context.Industries.Add(new Industry() { Name = name });
            _context.SaveChanges();
        }

        public string GetIndustryNameByIdAsync(int? id)
        {
            var item = _context.Industries.First(p => p.Id == id);
            return item.Name;
        }

    }

}
