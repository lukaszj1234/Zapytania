using Prism.Commands;
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
using ViewModel.Items;
using ViewModel.ViewModelsInterafaces;
using ViewModel.Wrapper;


namespace ViewModel.ViewModels.InquiryViewModels
{
    public class SendedInquiryViewModel : VievModelBase, ISendedInquiryViewModel
    {
        private InquiryWrapper _inquiry;
        private IInquiryRepository _inquiryRepository;
        private ISendedInquiryRespository _sendedInquiryRepository;
        private ILookupDataService _lookupDataService;
        public DelegateCommand AddSendedInquiry { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public ObservableCollection<SendedInquiryItem> SendedInquiries { get; }

        public SendedInquiryViewModel(IInquiryRepository inquiryRepository, 
            ISendedInquiryRespository sendedInquiryRespository, ILookupDataService lookupDataService)
        {
            _inquiryRepository = inquiryRepository;
            _sendedInquiryRepository = sendedInquiryRespository;
            _lookupDataService = lookupDataService;
            AddSendedInquiry = new DelegateCommand(OnAddSendedInquiry, OnAddSendedInquiryCanExeceute);
            SaveCommand = new DelegateCommand(OnSaveCommand, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteCommandExecute, OnDeleteCommandCanExecute);
            
            SendedInquiries = new ObservableCollection<SendedInquiryItem>();

        }

        private bool OnAddSendedInquiryCanExeceute()
        {
            if (Inquiry == null)
            {
                return false;
            }
            else if (Inquiry.Id == 0)
            {
                return false;
            }
            return true;
        }

        private bool OnDeleteCommandCanExecute()
        {
            if (SelectedSendedInquiry == null)
            {
                return false;
            }
            return true;
        }

        private async void OnDeleteCommandExecute()
        {
            _sendedInquiryRepository.RemoveById(SelectedSendedInquiry.Id);
            await _sendedInquiryRepository.SaveAsync();
            await ReLoadAsync(Inquiry.Id);
        }

        private bool OnSaveCanExecute()
        {
            if (SelectedSendedInquiry != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void OnSaveCommand()
        {
            if (SelectedSendedInquiry.Id == 0)
            {
                var inquiry = new SendedInquiry();
                inquiry.Description = SelectedSendedInquiry.DisplayDescription;
                inquiry.Name = SelectedSendedInquiry.DisplaySendedInquiry;
                inquiry.InquiryId = Inquiry.Id;
                var sendedInquiryId = _sendedInquiryRepository.Add(inquiry);
                await ReLoadAsync(sendedInquiryId);
            }
            else
            {
                SendedInquiry inquiry = new SendedInquiry();
                inquiry.Id = SelectedSendedInquiry.Id;
                inquiry.Name = SelectedSendedInquiry.DisplaySendedInquiry;
                inquiry.Description = SelectedSendedInquiry.DisplayDescription.ToString();
                _sendedInquiryRepository.Update(inquiry);
                await ReLoadAsync(inquiry.Id);
            }
        }

        private async Task ReLoadAsync(int selectedInquiryId)
        {
            Inquiry inquiry;
            inquiry = await _inquiryRepository.GetByIdAsync(Inquiry.Id);
            Inquiry = new InquiryWrapper(inquiry);
            SendedInquiries.Clear();
            var items = await _lookupDataService.GetSendedInquiryLookupAsync(Inquiry.Id);
            foreach (var item in items)
            {
                SendedInquiries.Add(new SendedInquiryItem(item.Id, item.DisplayInquiry, item.DisplayDescription));
            }
            SelectedSendedInquiry = SendedInquiries.FirstOrDefault(p => p.Id == selectedInquiryId);
            OnPropertyChanged();
        }

        private void OnAddSendedInquiry()
        {
            SelectedSendedInquiry = new SendedInquiryItem();
        }

        public async Task  LoadAsync (int? inquiryId)
        {
            if (inquiryId.HasValue)
            {
                Inquiry inquiry;
                inquiry = await _inquiryRepository.GetByIdAsync(inquiryId);
                Inquiry = new InquiryWrapper(inquiry);
                SendedInquiries.Clear();
                var items = await _lookupDataService.GetSendedInquiryLookupAsync(Inquiry.Id);
                foreach (var item in items)
                {
                    SendedInquiries.Add(new SendedInquiryItem(item.Id, item.DisplayInquiry, item.DisplayDescription));
                }
            }
        }

        public InquiryWrapper Inquiry
        {
            get { return _inquiry; }
            private set
            {
                _inquiry = value;
                OnPropertyChanged();
                AddSendedInquiry.RaiseCanExecuteChanged();
            }
        }

        private SendedInquiryItem _selectedSendedInquiry;

        public SendedInquiryItem SelectedSendedInquiry
        {
            get { return _selectedSendedInquiry; }
            set
            {
                _selectedSendedInquiry = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }


    }
}
