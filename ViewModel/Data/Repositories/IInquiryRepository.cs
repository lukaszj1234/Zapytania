using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ProjektInzynierski.DataAccess.Entities;

namespace ViewModel.Data.Repositories
{
    public interface IInquiryRepository
    {
        Task<Inquiry> GetByIdAsync(int inquiryId);
        Task SaveAsync();
        Task<Inquiry> GetByIdAsync(int? inquiryId);
        bool HasChanges();
        void Add(Inquiry inquiry);
        void Remove(Inquiry model);

        List<Inquiry> GetByNameAsync(string name);
        Task<Inquiry> GetByIndustryIdAsync(int id);

    }
}