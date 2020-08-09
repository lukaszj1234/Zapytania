using Model;
using Prism.Commands;
using Prism.Events;
using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Data.Lookups;
using ViewModel.Data.Repositories;
using ViewModel.Data.Repositories.Inqury;
using ViewModel.Event;
using ViewModel.Items;
using ViewModel.ViewModelsInterafaces;
using ViewModel.Wrapper;

namespace ViewModel.ViewModels.InquiryViewModels
{
    public class OfferViewModel : VievModelBase, IOfferViewModel
    {
        private IFolderManager _folderManager;
        private IFileManager _fileManager;
        private IReferenceOfferRepository _referenceOfferRepository;
        private IOfferRepository _offerRepository;
        private IEventAggregator _eventAggregator;
        private IInquiryRepository _inquiryRepository;
        private IReferenceOfferLookupService _referenceOfferLookupService;
        private IOfferLookupService _offerLookupService;
        private InquiryWrapper _inquiry;
        private OfferItemViewModel _selectedOffer;
        private bool working = false;
        

        public DelegateCommand AddOfferCommand { get; }
        public DelegateCommand AddReferenceOfferCommand { get; }
        public DelegateCommand DeleteOfferCommand { get; }
        public DelegateCommand DeleteReferenceOfferCommand { get; }
       
        public ObservableCollection<OfferItemViewModel> Offers { get; }
        public ObservableCollection<ReferenceOfferItemViewModel> ReferenceOffers { get; }

        public OfferViewModel(IFolderManager folderManager, IInquiryRepository inquiryRepository,
            IOfferLookupService offerLookupService, IReferenceOfferLookupService referenceOfferLookupService,
            IFileManager fileManager, IReferenceOfferRepository referenceOfferRepository, IOfferRepository offerRepository,
             IEventAggregator eventAggregator
            )
        {
            _folderManager = folderManager;
            _fileManager = fileManager; 
            _referenceOfferRepository = referenceOfferRepository;
            _inquiryRepository = inquiryRepository; 
            _referenceOfferLookupService = referenceOfferLookupService;
            _offerLookupService = offerLookupService;
            _offerRepository = offerRepository;
            _eventAggregator = eventAggregator;
            AddOfferCommand = new DelegateCommand(OnAddOffer, OnAddOfferCanExecute);
            AddReferenceOfferCommand = new DelegateCommand(OnAddReferenceOffer, OnAddReferenceOfferCanExecute);
            DeleteOfferCommand = new DelegateCommand(OnDeleteOffer, OnDeleteOfferCanExecute);
            DeleteReferenceOfferCommand = new DelegateCommand(OnDeleteReferenceOffer, OnDeleteReferenceOfferCanExecute);
            Offers = new ObservableCollection<OfferItemViewModel>();
            ReferenceOffers = new ObservableCollection<ReferenceOfferItemViewModel>();
            _eventAggregator.GetEvent<AfterNewThredStart>().Subscribe(ThreadWorkingEvent);
        }

        private void ThreadWorkingEvent(bool obj)
        {
            working = obj;
            DeleteReferenceOfferCommand.RaiseCanExecuteChanged();
            DeleteOfferCommand.RaiseCanExecuteChanged();
        }

        private bool OnAddOfferCanExecute()
        {
            if (Inquiry == null || Inquiry.Id == 0)
            {
                return false;
            }
            return true;
        }

        private bool OnAddReferenceOfferCanExecute()
        {
            
            if (ReferenceOffers.Count == 0 && Inquiry != null)
            {
                if (Inquiry.Id !=0)
                return true;
            }
            return false;
        }

        private bool OnDeleteReferenceOfferCanExecute()
        {
            
            if (ReferenceOffers.Count != 0 && working==false)
            {
                return true;
            }
            return false;
        }

        private async void OnDeleteReferenceOffer()
        {
            var referenceOffer = _referenceOfferRepository.GettAllReferenceOffersByInquiryId(Inquiry.Id);
            _fileManager.DeleteFile(referenceOffer[0].Path);
            _referenceOfferRepository.Remove(referenceOffer[0]);
            await _referenceOfferRepository.SaveAsync();
            await LoadAsync(Inquiry.Id);
            AddReferenceOfferCommand.RaiseCanExecuteChanged();
            DeleteReferenceOfferCommand.RaiseCanExecuteChanged();
            _eventAggregator.GetEvent<AfterOfferChangeEvent>().Publish();
        }

        private bool OnDeleteOfferCanExecute()
        {
            if (SelectedOffer == null || working==true)
            {
                return false;
            }
            else
            {
                if (SelectedOffer.Id == 0)
                {
                    return false;
                }
                return true;
            }
        }

        private void OnDeleteOffer()
        {
            var path = _offerRepository.GetOfferPathById(SelectedOffer.Id);
            _fileManager.DeleteFile(path);
            _offerRepository.RemoveByOfferId(SelectedOffer.Id);
            _offerRepository.SaveAsync();
            var offer = Offers.SingleOrDefault(i => i.Id == SelectedOffer.Id);
            if (offer != null)
            {
                Offers.Remove(offer);
            }
            _eventAggregator.GetEvent<AfterOfferChangeEvent>().Publish();
        }

        

        public OfferItemViewModel SelectedOffer
        {
            get { return _selectedOffer; }
            set { _selectedOffer = value;
                OnPropertyChanged();
               DeleteOfferCommand.RaiseCanExecuteChanged();
            }
        }


        private async void OnAddReferenceOffer()
        {
            var path = _folderManager.CreateNewFolder(Inquiry.Path, "Oferta Wzorcowa");
            var offer = _fileManager.AddReferenceOffer(path, Inquiry.Id);
            if (offer != null)
            {
                _referenceOfferRepository.Add(offer);
                await _referenceOfferRepository.SaveAsync();
                await LoadAsync(Inquiry.Id);
                AddReferenceOfferCommand.RaiseCanExecuteChanged();
                DeleteReferenceOfferCommand.RaiseCanExecuteChanged();
                _eventAggregator.GetEvent<AfterOfferChangeEvent>().Publish();
            }
        }

        private async void OnAddOffer()
        {
            var path = _folderManager.CreateNewFolder(Inquiry.Path, "Oferty");
            var existingOffers = _offerRepository.GettAllOffersByInquiryId(Inquiry.Id);
            var fileList = _fileManager.AddOffer(path, Inquiry.Id, existingOffers);
            foreach (var item in fileList)
            {
                _offerRepository.Add(item);
            }
            await _offerRepository.SaveAsync();
            await LoadAsync(Inquiry.Id);
            _eventAggregator.GetEvent<AfterOfferChangeEvent>().Publish();
        }

        public async Task LoadAsync(int? inquiryId)
        {
            Inquiry inquiry;
            if (inquiryId.HasValue)
            {
                inquiry = await _inquiryRepository.GetByIdAsync(inquiryId.Value);
                Inquiry = new InquiryWrapper(inquiry);
                var offerLookup = await _offerLookupService.GetOfferLookupAsync(inquiryId);
                Offers.Clear();
                foreach (var item in offerLookup)
                {
                    Offers.Add(new OfferItemViewModel(item.Id, item.DisplayInquiry));
                }
                var referenceOfferLookup = await _referenceOfferLookupService.GetReferenceOfferLookupAsync(inquiryId);
                ReferenceOffers.Clear();
                foreach (var item in referenceOfferLookup)
                {
                    ReferenceOffers.Add(new ReferenceOfferItemViewModel(item.Id, item.DisplayInquiry));
                }
            }
            AddReferenceOfferCommand.RaiseCanExecuteChanged();
            DeleteReferenceOfferCommand.RaiseCanExecuteChanged();
        }
        public InquiryWrapper Inquiry
        {
            get { return _inquiry; }
            private set
            {
                _inquiry = value;
                OnPropertyChanged();
                AddOfferCommand.RaiseCanExecuteChanged();
            }
        }
    }



}
