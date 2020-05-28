using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories
{
    public class SendedInquiryRespository : ISendedInquiryRespository
    {
        private InquiryContext _context;

        public SendedInquiryRespository(InquiryContext context)
        {
            _context = context;
        }


        public void RemoveByInquiryId(int id)
        {
            var list = _context.SendedInquiries.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                _context.SendedInquiries.Remove(item);
            }
            _context.SaveChanges();
        }

        public int Add(SendedInquiry file)
        {
            _context.SendedInquiries.Add(file);
            _context.SaveChanges();
            return file.Id;
        }

        public void Update(SendedInquiry file)
        {
           var inquiry=_context.SendedInquiries.Find(file.Id);
            inquiry.Name = file.Name;
            inquiry.Description = file.Description;
            _context.SaveChanges();
        }

        public  List<SendedInquiry> GettAllSendedInquiriesByInquiryId(int? inquiryID)
        {
            return _context.SendedInquiries.Where(f => f.InquiryId == inquiryID).ToList();
        }
        public async Task<SendedInquiry> GetByIdAsync(int fileId)
        {
            return await _context.SendedInquiries.FindAsync(fileId);
        }


     

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(SendedInquiry model)
        {
            _context.SendedInquiries.Remove(model);
        }

        public void RemoveById(int Id)
        {
            var offer = _context.SendedInquiries.Single(i => i.Id == Id);
            _context.SendedInquiries.Remove(offer);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
