using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Lookups
{
    public interface IOfferLookupService
    {
        Task<List<LookupItem>> GetOfferLookupAsync(int? inquiryId);
    }
}
