using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektInzynierski.DataAccess.Entities;

namespace ViewModel.Data.Repositories.Inqury
{
    public interface IOfferRepository
    {
        void Add(Offer file);
        Task<Offer> GetByIdAsync(int fileId);
        string GetOfferPathById(int offerId);
        List<Offer> GettAllOffersByInquiryId(int inquiryID);
        bool HasChanges();
        void Remove(Offer model);
        void RemoveByOfferId(int offerId);
        void RemoveByInquiryId(int id);
        Task SaveAsync();
        void ChangePath(string newPath, int id);
    }
}