using MahApps.Metro.Controls.Dialogs;
using Model;
using Prism.Commands;
using Prism.Events;
using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using ViewModel.Data.Repositories;
using ViewModel.Event;
using ViewModel.Items;
using ViewModel.Services;
using ViewModel.ViewModelsInterafaces;
using ViewModel.Wrapper;

namespace ViewModel.ViewModels
{
    public class CompareOffersViewModel : VievModelBase, ICompareOffersViewModel
    {
        private InquiryWrapper _inquiry;
        private bool working;
        private IOfferRepository _offerRepository;
        private IXlsManager _xlsManager;
        private IReferenceOfferRepository _referenceOfferRepository;
        private IInquiryRepository _inquiryRepository;
        private IEventAggregator _eventAggregator;
        private System.Windows.Threading.Dispatcher Dispatcher { get; set; }
        public string collumns;
        private IMessageDialogService _messageDialogService;
        private IDialogCoordinator _dialogCoordinator;
        public DelegateCommand SaveCommand { get; }
        private bool _hasChanges;

        public DelegateCommand CompareOffersCommand { get; }
        public CompareOffersViewModel(IOfferRepository offerRepository, IXlsManager xlsManager,
            IReferenceOfferRepository referenceOfferRepository, IInquiryRepository inquiryRepository,
             IMessageDialogService messageDialogService,
            IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator)
        {
            _offerRepository = offerRepository;
            _xlsManager = xlsManager;
            _referenceOfferRepository = referenceOfferRepository;
            _inquiryRepository = inquiryRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _dialogCoordinator = dialogCoordinator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CompareOffersCommand = new DelegateCommand(OnCompareOffersExecute, OnCompareOffersCanExecute);
            _eventAggregator.GetEvent<AfterOfferChangeEvent>().Subscribe(AfterOfferChange);
            Dispatcher = Dispatcher.CurrentDispatcher;
        }

     

        private void AfterOfferChange()
        {
            CompareOffersCommand.RaiseCanExecuteChanged();
        }

        private async void OnSaveExecute()
        {
            await _inquiryRepository.SaveAsync();
            HasChanges = _inquiryRepository.HasChanges();
            OnPropertyChanged();
            ((DelegateCommand)CompareOffersCommand).RaiseCanExecuteChanged();
        }

        private bool OnSaveCanExecute()
        {
            return Inquiry != null;
        }


        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                    _hasChanges = value;
              SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool OnCompareOffersCanExecute()
        {
            if (Inquiry != null && working==false)
            {
                var referenceOffer = _referenceOfferRepository.GettAllReferenceOffersByInquiryId(Inquiry.Id);
                var offers = _offerRepository.GettAllOffersByInquiryId(Inquiry.Id);
                if (referenceOffer.Count != 0 && offers.Count != 0)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        private List<int> GetCollumns()
        {
           
            List<int> returnList = new List<int>();
            if (Collumns != null && Collumns != "")
            {
                string[] collumns = Collumns.Split(',');
                foreach (var item in collumns)
                {
                    try
                    {
                        returnList.Add(Int32.Parse(item));
                    }
                    catch
                    {
                        Dispatcher.Invoke((Action)delegate ()
                        {
                            _messageDialogService.ShowOkDialog("Wprowadzono błędne dane." +
                           " Należy wprowadzić liczby oddzielone przecinkiem.", "Błąd danych");
                        });
                        return null;
                    }
                }
            }
            return returnList;
        }

        private  void OnCompareOffersExecute()
        {
            //await _dialogCoordinator.ShowMessageAsync(this, "Proszę czekać", "Trwa porównyanie ofert...");
            //var controller = await _dialogCoordinator.ShowProgressAsync(this,"Proszę czekać"
            //    ,"Trwa porównywanie ofert");
           // controller.SetIndeterminate();
           Thread t = new Thread(Compare);
           t.Start();
            
           // await controller.CloseAsync();
        }

    private void Compare()
    {
            working = true;
            CompareOffersCommand.RaiseCanExecuteChanged();
            _eventAggregator.GetEvent<AfterNewThredStart>().Publish(true);
            var collumns =  GetCollumns();
            if (collumns != null)
            {
                List<string> offersStringList = new List<string>();
                var offersList = _offerRepository.GettAllOffersByInquiryId(Inquiry.Id);
                foreach (var item in offersList)
                {
                    offersStringList.Add(item.Path);
                }
                var inquiry = _inquiryRepository.GetByIdAsync(Inquiry.Id).Result;
                var result = _xlsManager.CompareOffers(_referenceOfferRepository.GetFilePathByInquiryId(Inquiry.Id),
                       offersStringList, Inquiry.Path, collumns);
                if (result == null)
                {
                    Dispatcher.Invoke((Action)delegate ()
                    {
                        _messageDialogService.ShowOkDialog("Zamknij wysztkie arkusze programu Microsoft " +
                   "Excel i spróbuj ponownie", "Bład dostępu do pliku");
                    });
                }
            }
            Dispatcher.Invoke((Action)delegate ()
            {
                _messageDialogService.ShowOkDialog("Zakończono wykonywanie obliczeń.", "Koniec obliczeń");
            });
            working = false;
            CompareOffersCommand.RaiseCanExecuteChanged();
            _eventAggregator.GetEvent<AfterNewThredStart>().Publish(false);
        }

        public async Task LoadAsync(int? inquiryId)
        {
            Inquiry inquiry;
            if (inquiryId.HasValue)
            {
                inquiry = await _inquiryRepository.GetByIdAsync(inquiryId.Value);
                Inquiry = new InquiryWrapper(inquiry);
            }
            CompareOffersCommand.RaiseCanExecuteChanged();
        }

        public InquiryWrapper Inquiry
        {
            get { return _inquiry;}
            private set
            {
                _inquiry = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

      
        private string _collumns;

        public string Collumns
        {
            get { return _collumns; }
            set { _collumns = value; }
        }

    }
}
