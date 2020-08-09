using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModel.Data.Repositories.Inqury
{
    public class InquiryRepository : IInquiryRepository
    {
        private InquiryContext _context;

        public InquiryRepository(InquiryContext context)
        {
            _context = context;
        }

        public void Add(Inquiry inquiry)
        {
            string path=Path.GetDirectoryName(System.Reflection.Assembly
                .GetExecutingAssembly().Location).ToString();
            path = System.IO.Path.Combine(path, "Zapytania Ofertowe");
            inquiry.Path = path;
            _context.Inquiries.Add(inquiry);
        }

        public async Task<Inquiry> GetByIdAsync(int inquiryId)
        {
            return await _context.Inquiries.SingleAsync(i => i.Id == inquiryId);
        }

        public  List<Inquiry> GetByNameAsync(string name)
        {
            return  _context.Inquiries.Where(i => i.Name == name).ToList();
        }

        public async Task<Inquiry> GetByIdAsync(int? inquiryId)
        {
            return await _context.Inquiries.SingleAsync(i => i.Id == inquiryId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Inquiry model)
        {
            _context.Inquiries.Remove(model);
        }

        public async Task SaveAsync()
        {       
                await _context.SaveChangesAsync();
        }

        public async Task<Inquiry> GetByIndustryIdAsync(int id)
        {
            return await _context.Inquiries.FirstOrDefaultAsync(p => p.IndustryId == id);
        }
        
    }
}
