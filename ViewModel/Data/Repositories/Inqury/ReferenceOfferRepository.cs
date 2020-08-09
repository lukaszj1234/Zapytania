using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories.Inqury
{
    public class ReferenceOfferRepository : IReferenceOfferRepository
    {
        private InquiryContext _context;

        public ReferenceOfferRepository(InquiryContext context)
        {
            _context = context;
        }

        public void Add(ReferenceOffer file)
        {
            _context.ReferenceOffers.Add(file);
        }

        public void RemoveByInquiryId(int id)
        {
            var list = _context.ReferenceOffers.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                _context.ReferenceOffers.Remove(item);
            }
            _context.SaveChanges();
        }

        public List<ReferenceOffer> GettAllReferenceOffersByInquiryId(int inquiryID)
        {
            return _context.ReferenceOffers.Where(f => f.InquiryId == inquiryID).ToList();
        }
        public async Task<ReferenceOffer> GetByIdAsync(int fileId)
        {
            return await _context.ReferenceOffers.FindAsync(fileId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(ReferenceOffer model)
        {
            _context.ReferenceOffers.Remove(model);
        }

        public void RemoveByFileId(int fileId)
        {
            var file = _context.ReferenceOffers.Single(i => i.Id == fileId);
            _context.ReferenceOffers.Remove(file);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public string GetFilePathById(int fileid)
        {
            var file = _context.ReferenceOffers.Single(i => i.Id == fileid);
            return file.Path;
        }

        public string GetFilePathByInquiryId(int inquiryId)
        {
            var file = _context.ReferenceOffers.Single(i => i.InquiryId == inquiryId);
            return file.Path;
        }

        public void ChangePath(string newPath, int id)
        {
            var list = _context.ReferenceOffers.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                item.Path = Path.Combine(newPath, "Oferta Wzorcowa", item.Name);
            }
            _context.SaveChanges();
        }
    }
}
