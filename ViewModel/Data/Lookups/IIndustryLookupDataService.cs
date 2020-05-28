using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace ViewModel.Data.Lookups
{
    public interface IIndustryLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetIndustryLookupAsync();
    }
}