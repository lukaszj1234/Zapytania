using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace ViewModel.Data.Lookups
{
    public interface ILookupDataService
    {
        Task<IEnumerable<LookupItem>> GetInquiryLookupAsync();
        Task<List<LookupItem>> GetSendedInquiryLookupAsync(int? inquiryId);
    }
}