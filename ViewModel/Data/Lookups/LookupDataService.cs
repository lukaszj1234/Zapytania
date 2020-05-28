using Model;
using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Lookups
{
    public class LookupDataService : ILookupDataService, IFilesLookupService,
        IOfferLookupService, IReferenceOfferLookupService, ISendedInquiryLookupService, IIndustryLookupDataService
    {
        private Func<InquiryContext> _contextCreator;

        public LookupDataService(Func<InquiryContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<IEnumerable<LookupItem>> GetInquiryLookupAsync()
        {
            var list = new List<LookupItem>();
            using (var ctx = _contextCreator())
            {
                list = await ctx.Inquiries.AsNoTracking()
                      .Select(i => new LookupItem
                      {
                          Id = i.Id,
                          DisplayInquiry = i.Name,
                      }).ToListAsync();
            }
            return list;
        }

        public async Task<IEnumerable<LookupItem>> GetIndustryLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Industries.AsNoTracking()
                       .Select(i => new LookupItem
                       {
                           Id = i.Id,
                           DisplayIndustry = i.Name,
                       }).ToListAsync();
            }
        }


        public async Task<List<LookupItem>> GetFilesLookupAsync(int? inquiryId)
        {

            using (var ctx = _contextCreator())
            {
                var list = new List<AddedFile>();
                list = await ctx.Files.Where(s => s.InquiryId == inquiryId).ToListAsync();
                var lookupList = new List<LookupItem>();
                foreach (var i in list)
                {
                    lookupList.Add(new LookupItem()
                    {
                        Id = i.Id,
                        DisplayInquiry = i.Name
                    });
                }
                return lookupList;
            }
        }

        public async Task<List<LookupItem>> GetOfferLookupAsync(int? inquiryId)
        {
            using (var ctx = _contextCreator())
            {
                var list = new List<Offer>();
                list = await ctx.Offers.Where(s => s.InquiryId == inquiryId).ToListAsync();
                var lookupList = new List<LookupItem>();
                foreach (var i in list)
                {
                    lookupList.Add(new LookupItem()
                    {
                        Id = i.Id,
                        DisplayInquiry = i.Name
                    });
                }
                return lookupList;
            }
        }

        public async Task<List<LookupItem>> GetReferenceOfferLookupAsync(int? inquiryId)
        {
            using (var ctx = _contextCreator())
            {
                var list = new List<ReferenceOffer>();
                list = await ctx.ReferenceOffers.Where(s => s.InquiryId == inquiryId).ToListAsync();
                var lookupList = new List<LookupItem>();
                foreach (var i in list)
                {
                    lookupList.Add(new LookupItem()
                    {
                        Id = i.Id,
                        DisplayInquiry = i.Name
                    });
                }
                return lookupList;
            }
        }

        public async Task<List<LookupItem>> GetSendedInquiryLookupAsync(int? inquiryId)
        {
            using (var ctx = _contextCreator())
            {
                var list = new List<SendedInquiry>();
                list = await ctx.SendedInquiries.Where(s => s.InquiryId == inquiryId).ToListAsync();
                var lookupList = new List<LookupItem>();
                foreach (var i in list)
                {
                    lookupList.Add(new LookupItem()
                    {
                        Id = i.Id,
                        DisplayInquiry = i.Name,
                        DisplayDescription = i.Description
                    });
                }
                return lookupList;
            }
        }
    }
}
