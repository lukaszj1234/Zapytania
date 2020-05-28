using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektInzynierski.DataAccess.Entities;

namespace ViewModel.Data.Repositories
{
    public interface ISendedInquiryRespository
    {
        int Add(SendedInquiry file);
        Task<SendedInquiry> GetByIdAsync(int fileId);
        List<SendedInquiry> GettAllSendedInquiriesByInquiryId(int? inquiryID);

        void Update(SendedInquiry file);
        bool HasChanges();
        void Remove(SendedInquiry model);
        void RemoveById(int Id);

        void RemoveByInquiryId(int id);
        Task SaveAsync();
    }
}