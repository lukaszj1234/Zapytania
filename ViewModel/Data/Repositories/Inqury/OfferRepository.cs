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
    public class OfferRepository : IOfferRepository
    {
        private InquiryContext _context;

        public OfferRepository(InquiryContext context)
        {
            _context = context;
        }

        public void RemoveByInquiryId(int id)
        {
            var list = _context.Offers.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                _context.Offers.Remove(item);
            }
            _context.SaveChanges();
        }

        public void Add(Offer file)
        {
            _context.Offers.Add(file);
        }

        public List<Offer> GettAllOffersByInquiryId(int inquiryID)
        {
            return _context.Offers.Where(f => f.InquiryId == inquiryID).ToList();
        }
        public async Task<Offer> GetByIdAsync(int fileId)
        {
            return await _context.Offers.FindAsync(fileId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Offer model)
        {
            _context.Offers.Remove(model);
        }

        public void RemoveByOfferId(int offerId)
        {
            var offer = _context.Offers.Single(i => i.Id == offerId);
            _context.Offers.Remove(offer);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public string GetOfferPathById(int offerId)
        {
            var file = _context.Offers.Single(i => i.Id == offerId);
            return file.Path;
        }

        public void ChangePath(string newPath, int id)
        {
            var list= _context.Offers.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                item.Path = Path.Combine(newPath, "Oferty", item.Name);
            }
            _context.SaveChanges();
        }
    }
}
