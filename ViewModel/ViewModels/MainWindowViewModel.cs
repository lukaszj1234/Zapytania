using MahApps.Metro.Controls.Dialogs;
using Model;
using Prism.Commands;
using Prism.Events;
using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ViewModel.Data.Repositories;
using ViewModel.Event;
using ViewModel.ViewModelsInterafaces;

namespace ViewModel.ViewModels
{

    public class MainWindowViewModel : VievModelBase
    {
        public ICommand ButtonCommand { get; set; }
        public ICommand CopyFileCommand { get; set;}
        public ICommand CreateNewInquiryCommand { get; }
        public ICommand SortByNameAscendingCommand { get; }
        public ICommand SortByNameDescendingCommand { get; }
        public ICommand SortByIndustryAscendingCommand { get; }
        public ICommand SortByIndustryDescendingCommand { get; }

        private IInquiryFilesViewModel _inquiryFilesViewModel;
        private IOfferViewModel _offerViewModel;
        private ICompareOffersViewModel _compareOfferViewModel;

        private IEventAggregator _eventAggregator;
        private Func<IInquiryFilesViewModel> _inquiryFilesViewModelCreator;
        private Func<IOfferViewModel> _offerViewModelCreator;
        private Func<ICompareOffersViewModel> _compareOfferViewModelCreator;
        private Func<ISendedInquiryViewModel> _sendedInquiryViewModelCreator;
        private IInquiryRepository _inquiryRepository;
        private IIndsutryRepository _indsutryRepository;
        private ISendedInquiryViewModel _sendedInquiryViewModel;
        public MainWindowViewModel(
          INavigationViewModel navigationViewModel,
          Func<IInquiryFilesViewModel> inquiryFilesViewModelCreator,
          Func<IOfferViewModel> offerViewModelCreator,
          Func<ICompareOffersViewModel> compareOfferViewModelCreator, Func<ISendedInquiryViewModel> sendedInquiryViewModelCreator,
          IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IInquiryRepository inquiryRepository,
          IIndsutryRepository indsutryRepository)
        {
            NavigationViewModel = navigationViewModel;
            _inquiryFilesViewModelCreator = inquiryFilesViewModelCreator;
            _offerViewModelCreator = offerViewModelCreator;
            _compareOfferViewModelCreator = compareOfferViewModelCreator;
            _sendedInquiryViewModelCreator = sendedInquiryViewModelCreator;
            _inquiryRepository = inquiryRepository;
            _indsutryRepository = indsutryRepository;
            CreateNewInquiryCommand = new DelegateCommand(OnCreateNewInquiryExecute);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DisplayInquiryFilesEvent>().Subscribe(SelectedInquiryFilesView);
            _eventAggregator.GetEvent<AfterInquiryDeletedEvent>().Subscribe(AfterInquiryDeleted);
            _eventAggregator.GetEvent<AfterNameChangedEvent>().Subscribe(AfterNameChanged);
        }




        private void AfterNameChanged(AfterNameChangedEventArgs obj)
        {
            SetMessage(obj.DisplayInquiry,obj.Id);
        }

       

        private void AfterInquiryDeleted(int inquiryId)
        {
            InquiryFilesViewModel = null;
            OfferViewModel = null;
            CompareOffersViewModel = null;
            SendedInquiryViewModel = null;
            NameMessage = null;
            DateMessage = null;
        }

        private void OnCreateNewInquiryExecute()
        {

            SelectedInquiryFilesView(null);
            _eventAggregator.GetEvent<AfterNewInquiryExecuteEvent>().Publish();
            NameMessage = null;
            DateMessage = null;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
        public INavigationViewModel NavigationViewModel { get; }

       

        public IInquiryFilesViewModel InquiryFilesViewModel
        {
            get { return _inquiryFilesViewModel; }
            set { _inquiryFilesViewModel = value;
                OnPropertyChanged();
            }
        }


        public IOfferViewModel OfferViewModel
        {
            get { return _offerViewModel; }
            set
            {
                _offerViewModel = value;
                OnPropertyChanged();
            }
        }


        public ICompareOffersViewModel CompareOffersViewModel
        {
            get { return _compareOfferViewModel; }
            set
            {
                _compareOfferViewModel = value;
                OnPropertyChanged();
            }
        }

        public ISendedInquiryViewModel SendedInquiryViewModel
        {
            get { return _sendedInquiryViewModel; }
            set
            {
                _sendedInquiryViewModel = value;
                OnPropertyChanged();
            }
        }

        private string _nameMessage;

        public string NameMessage
        {
            get { return _nameMessage; }
            set {
                _nameMessage = value;
                OnPropertyChanged();
            }
        }

        private string _dateMessage;

        public string DateMessage
        {
            get { return _dateMessage; }
            set { _dateMessage = value;
                OnPropertyChanged();
            }
        }



        private async void SelectedInquiryFilesView(int? inquiryId)
        {
            InquiryFilesViewModel=_inquiryFilesViewModelCreator();
            await InquiryFilesViewModel.LoadAsync(inquiryId);
            OfferViewModel = _offerViewModelCreator();
            await OfferViewModel.LoadAsync(inquiryId);
            CompareOffersViewModel = _compareOfferViewModelCreator();
            await CompareOffersViewModel.LoadAsync(inquiryId);
            SendedInquiryViewModel = _sendedInquiryViewModelCreator();
            await SendedInquiryViewModel.LoadAsync(inquiryId);
            if (inquiryId != null)
            {
                GetMessage(inquiryId);
            }
        }

        private async  void GetMessage(int? inquiryId)
        {
            var inquiry =  await _inquiryRepository.GetByIdAsync(inquiryId);
            NameMessage = "Zapytanie: " + inquiry.Name;
            DateMessage = "Utworzono dnia: " + inquiry.Time;
        }

        private async  void SetMessage(string name, int id)
        {
            var inquiry = await _inquiryRepository.GetByIdAsync(id);
            NameMessage = "Zapytanie: " + name;
            DateMessage = "Utworzono dnia: " + inquiry.Time;
        }

    }
}
