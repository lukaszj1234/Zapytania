using Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Data;
using ViewModel.Data.Lookups;
using ViewModel.Data.Repositories;
using ViewModel.Data.Repositories.Inqury;
using ViewModel.Event;
using ViewModel.Items;
using ViewModel.ViewModelsInterafaces;

namespace ViewModel.ViewModels
{
    public class NavigationViewModel : VievModelBase, INavigationViewModel
    {
        private ILookupDataService _lookupDataService;
        private IIndustryLookupDataService _industryLookupDataService;
        private IEventAggregator _eventAggregator;
        private IInquiryRepository _inquiryRepository;

        public ObservableCollection<NavigationItemViewModel> Inquiry { get; }
        public NavigationViewModel(ILookupDataService lookupDataService,
            IEventAggregator eventAggregator, IIndustryLookupDataService industryLookupDataService,
            IInquiryRepository inquiryRepository)
        {
            _lookupDataService = lookupDataService;
            _industryLookupDataService = industryLookupDataService;
            _eventAggregator = eventAggregator;
            _inquiryRepository = inquiryRepository;
            Inquiry = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterNameChangedEvent>().Subscribe(AfterNameChanged);
            _eventAggregator.GetEvent<AfterInquiryDeletedEvent>().Subscribe(AfterInquiryDeleted);
            _eventAggregator.GetEvent<AfterNewInquiryExecuteEvent>().Subscribe(AfterCreateNewInquiry);
            _eventAggregator.GetEvent<AfterIndustryDeletedEvent>().Subscribe(AfterIndustryDeleted);
        }

        private async void AfterIndustryDeleted(int obj)
        {
            await LoadAsync();
        }

        private void AfterCreateNewInquiry()
        {
            SelectedInquiry = null;
        }

        private void AfterNameChanged(AfterNameChangedEventArgs obj)
        {
            var lookupItem = Inquiry.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                var newNavigationItemViewModel = new NavigationItemViewModel(obj.Id, obj.DisplayInquiry, obj.DisplayIndustry);
                Inquiry.Add(newNavigationItemViewModel);
            }
            else
            {
                lookupItem.DisplayInquiry = obj.DisplayInquiry;
                lookupItem.DisplayIndustry = obj.DisplayIndustry;

            }

        }

        public async Task LoadAsync()
        {
            var lookup = await _lookupDataService.GetInquiryLookupAsync();
            var industryLookups = await _industryLookupDataService.GetIndustryLookupAsync();
            Inquiry.Clear();
            foreach (var item in lookup)
            {
                var inquiry = await _inquiryRepository.GetByIdAsync(item.Id);
                var industry = industryLookups.FirstOrDefault(p => p.Id == inquiry.IndustryId);
                if (industry != null)
                {
                    Inquiry.Add(new NavigationItemViewModel(item.Id, item.DisplayInquiry,
                       industry.DisplayIndustry));
                }
                else
                {
                    Inquiry.Add(new NavigationItemViewModel(item.Id, item.DisplayInquiry,
                           ""));
                }
            }

        }

        private NavigationItemViewModel _selectedInquiry;

        public NavigationItemViewModel SelectedInquiry
        {
            get { return _selectedInquiry; }
            set { _selectedInquiry = value;
                OnPropertyChanged();
                if (_selectedInquiry != null)
                {
                    _eventAggregator.GetEvent<DisplayInquiryFilesEvent>().Publish(_selectedInquiry.Id);
                }
            }
            
        }

        private void AfterInquiryDeleted(int inquiryId)
        {
            var inquiry = Inquiry.SingleOrDefault(i => i.Id == inquiryId);
            if (inquiry!=null)
                {
                Inquiry.Remove(inquiry);
            }
        }

    }
}
