using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektInzynierski.DataAccess.Entities;

namespace ViewModel.Data.Repositories.Inqury
{
    public interface IReferenceOfferRepository
    {
        void Add(ReferenceOffer file);
        Task<ReferenceOffer> GetByIdAsync(int fileId);
        string GetFilePathById(int fileid);
        List<ReferenceOffer> GettAllReferenceOffersByInquiryId(int inquiryID);
        bool HasChanges();
        void Remove(ReferenceOffer model);
        void RemoveByFileId(int fileId);
        Task SaveAsync();
        void RemoveByInquiryId(int id);


        string GetFilePathByInquiryId(int inquiryId);
        void ChangePath(string newPath, int id);
    }
}