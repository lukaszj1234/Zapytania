using MahApps.Metro.Controls.Dialogs;
using Model;
using Prism.Commands;
using Prism.Events;
using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.Data.Lookups;
using ViewModel.Data.Repositories;
using ViewModel.Data.Repositories.Inqury;
using ViewModel.Event;
using ViewModel.Items;
using ViewModel.Services;
using ViewModel.ViewModelsInterafaces;
using ViewModel.Wrapper;

namespace ViewModel.ViewModels.InquiryViewModels
{
    public class InquiryFilesViewModel : VievModelBase, IInquiryFilesViewModel
    {
        private IFolderManager _folderManager;
        private IInquiryRepository _inquiryRepository;
        private IAddedFilesRepository _addedFileRepository;
        private IReferenceOfferRepository _referenceOfferRepository;
        private ISendedInquiryRespository _sendedInquiryRespository;
        private IOfferRepository _offerRepository;
        private IFilesLookupService _filesLookupService;
        private IIndsutryRepository _indstryRepository;
        private IIndustryLookupDataService _industryLookupDataService;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IFileManager _fileManager;
        private InquiryWrapper _inquiry;
        private bool _hasChanges;

        public InquiryFilesViewModel(IInquiryRepository inquiryRepository,IAddedFilesRepository addedFilesRepository,
            IReferenceOfferRepository referenceOfferRepository,IOfferRepository offerRepository,ISendedInquiryRespository sendedInquiryRespository, 
            IEventAggregator eventAggregator, IFolderManager folderManager, 
            IFilesLookupService filesLookupService, IFileManager fileManager,
            IMessageDialogService messageDialogService, IIndustryLookupDataService industryLookupDataService, 
            IIndsutryRepository indsutryRepository
           )
        {
            _folderManager = folderManager;
            _inquiryRepository = inquiryRepository;
            _addedFileRepository = addedFilesRepository;
            _referenceOfferRepository = referenceOfferRepository;
            _sendedInquiryRespository = sendedInquiryRespository;
            _offerRepository = offerRepository;
            _filesLookupService = filesLookupService;
            _indstryRepository = indsutryRepository;
            _industryLookupDataService = industryLookupDataService;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _fileManager = fileManager;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            AddFileCommand = new DelegateCommand(OnAddFileExecute, OnAddFileCanExecute);
            DeleteFileCommand = new DelegateCommand(OnDeleteFileExecute, OnDeleteFileCanExecute);
            SendInquiryCommand = new DelegateCommand(OnSendCommandExecute, OnSendCommandCanExecute);
            DeleteIndustryCommand = new DelegateCommand(OnDeleteIndustryExecute);
            AddIndustryCommand = new DelegateCommand(OnAddIndustryExecute);
            Files = new ObservableCollection<FileItemViewModel>();
            Industries = new ObservableCollection<LookupItem>();
            _eventAggregator.GetEvent<AfterDoubleClickEvent>().Subscribe(AfterDoubleClick);
        }

        private void AfterDoubleClick()
        {
            throw new NotImplementedException();
        }

        private async void OnAddIndustryExecute()
        {
            var name= await _messageDialogService.ShowInputDialog("Wprowadź nazwę", 
                "Dodawanie nowej branży");
            if (name != null)
            {
                _indstryRepository.Add(name);
                await LoadAsync(Inquiry.Id);
            }
        }

        private async void OnDeleteIndustryExecute()
        {

            int id = Inquiry.IndustryId.GetValueOrDefault();
            if (id != 0)
            {
                var inquiry = await _inquiryRepository.GetByIndustryIdAsync(id);
                if (inquiry != null)
                {
                    var result = await _messageDialogService.ShowOkCancelDialog("Branża jest przypisana do conajmniej jednego " +
                         "zapytania. Czy chcesz kontynuować?", "Potwierdź usunięcie branży");
                    if (result == MessageDialogResult.Affirmative)
                    {
                        _indstryRepository.DeleteByIdAsync(id);
                        await LoadAsync(Inquiry.Id);
                        _eventAggregator.GetEvent<AfterIndustryDeletedEvent>().Publish(Inquiry.Id);
                    }
                }
                else
                {
                    _indstryRepository.DeleteByIdAsync(id);
                    await LoadAsync(Inquiry.Id);
                    _eventAggregator.GetEvent<AfterIndustryDeletedEvent>().Publish(Inquiry.Id);
                }
            }
        }

        private void OnSendCommandExecute()
        {
            _folderManager.AddToZipAndSend(Inquiry.Path, _addedFileRepository.GettAllFilesByInquiryId(Inquiry.Id));
        }

        private bool OnSendCommandCanExecute()
        {
            if (Inquiry == null || Inquiry.Id == 0)
            {
                return false;
            }
            return true;
        }

        private bool OnAddFileCanExecute()
        {
            if (Inquiry==null || Inquiry.Id==0)
                {
                return false;
            }
            return true;
        }

        private bool OnDeleteFileCanExecute()
        {
            if (SelectedFile == null)
            {
                return false;
            }
            else
            {
                if (SelectedFile.Id ==0) {
                    return false;
                }
                return true;
            }
        }

        private void OnDeleteFileExecute()
        {
            var path = _addedFileRepository.GetFilePathById(SelectedFile.Id);
            _fileManager.DeleteFile(path);
            _addedFileRepository.RemoveByFileId(SelectedFile.Id);
            _addedFileRepository.SaveAsync();
            var file = Files.SingleOrDefault(i => i.Id == SelectedFile.Id);
            if (file != null)
            {
                Files.Remove(file);
            }

        }

        private FileItemViewModel _selectedFile;

       

        public FileItemViewModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                OnPropertyChanged();
                ((DelegateCommand)DeleteFileCommand).RaiseCanExecuteChanged(); 
            }
        }

        private async void OnAddFileExecute()
        {
            var existingFiles = _addedFileRepository.GettAllFilesByInquiryId(Inquiry.Id);
            var path = _folderManager.CreateNewFolder(Inquiry.Path, "Pliki");
            var fileList=_fileManager.AddFile(path, Inquiry.Id, existingFiles);
            foreach (var item in fileList)
            {
                _addedFileRepository.Add(item);
            }
            await _addedFileRepository.SaveAsync();
            await LoadAsync(Inquiry.Id);
        }

        private async void OnDeleteExecute()
        {
            var result = await _messageDialogService.ShowOkCancelDialog("Czy usunąć wybrane zapytanie?", "Potwierdź usunięcie zapytania");
            if (result == MessageDialogResult.Affirmative)
            {
                _folderManager.DeleteFolder(Inquiry.Path);
                _addedFileRepository.RemoveByInquiryId(Inquiry.Id);
                _referenceOfferRepository.RemoveByInquiryId(Inquiry.Id);
                _offerRepository.RemoveByInquiryId(Inquiry.Id);
                _sendedInquiryRespository.RemoveByInquiryId(Inquiry.Id);
                _inquiryRepository.Remove(Inquiry.Model);
                await _inquiryRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterInquiryDeletedEvent>().Publish(Inquiry.Id);
            } 
        }

        public ObservableCollection<FileItemViewModel> Files { get; }
        public ObservableCollection<LookupItem> Industries { get; }

        public async Task LoadAsync(int? inquiryId)
        {
            var inquiry = inquiryId.HasValue ?
                await _inquiryRepository.GetByIdAsync(inquiryId.Value) : CreateNewInquiry();
            Inquiry = new InquiryWrapper(inquiry);

            if (inquiryId.HasValue)
            {
                var lookup = await _filesLookupService.GetFilesLookupAsync(inquiryId);
                Files.Clear();
                foreach (var item in lookup)
                {
                    Files.Add(new FileItemViewModel(item.Id, item.DisplayInquiry));
                }
            }
            Inquiry.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _inquiryRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Inquiry.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Inquiry.Id == 0)
            {
                Inquiry.Name = "";
            }
            Industries.Clear();
            var industryLookup = await _industryLookupDataService.GetIndustryLookupAsync();
            Industries.Add(new NullLookupItem());
            foreach (var item in industryLookup)
            {
                Industries.Add(item);
            }
        }

        private Inquiry CreateNewInquiry()
        {
            var inquiry = new Inquiry();
            _inquiryRepository.Add(inquiry);
            return inquiry;
        }

        public InquiryWrapper Inquiry
        {
            get { return _inquiry; }
            private set
            {
                _inquiry = value;
                OnPropertyChanged();
                ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                SendInquiryCommand.RaiseCanExecuteChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                    _hasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }


        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddFileCommand { get; }
        public ICommand DeleteFileCommand { get; }
        public DelegateCommand SendInquiryCommand { get; }
        public DelegateCommand DeleteIndustryCommand { get; }
        public DelegateCommand AddIndustryCommand { get; }

        private bool OnSaveCanExecute()
        {
            return Inquiry != null && !Inquiry.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            var check =  _inquiryRepository.GetByNameAsync(Inquiry.Name);
            if (check.Count == 0 && Inquiry.Id == 0)
            {
               await CreateInquiry();
            } else if (Inquiry.Id != 0 && check.Count == 0)
            {
               await RenameInquiry();
            }
            else if (Inquiry.Id != 0  && _inquiryRepository.HasChanges())
            {
                await ChangeIndustry();
            }
            else
            {
                await _messageDialogService.ShowOkDialog("Zapytanie o podanej nazwie już istnieje. Prosze wybrać inną nazwę", "Zmień nazwę");
            }
        }

        private async Task ChangeIndustry()
        {
            var inquiry = await _inquiryRepository.GetByIdAsync(Inquiry.Id);
            inquiry.IndustryId = Inquiry.IndustryId;
            await _inquiryRepository.SaveAsync();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            PublishAfterNameChangedEvent();
        }

        private async Task RenameInquiry()
        {
            var inquiry = await _inquiryRepository.GetByIdAsync(Inquiry.Id);
            inquiry.Name = Inquiry.Name;
            var newPath= _folderManager.RenameFolder(Inquiry.Path, Inquiry.Name);
            Inquiry.Path = newPath;
            await _inquiryRepository.SaveAsync();
            _referenceOfferRepository.ChangePath(newPath, Inquiry.Id);
            _addedFileRepository.ChangePath(newPath, Inquiry.Id);
            _offerRepository.ChangePath(newPath, Inquiry.Id);
            PublishAfterNameChangedEvent();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private async Task CreateInquiry()
        {
            _folderManager.CreateMainFolder();
            var path = _folderManager.CreateNewInquiryFolder(Inquiry.Name);
            Inquiry.Path = path;
            Inquiry.Time = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            //Inquiry.Industry = Inquiry.Industry;
            await _inquiryRepository.SaveAsync();
            HasChanges = _inquiryRepository.HasChanges();
            ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
            PublishAfterNameChangedEvent();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void PublishAfterNameChangedEvent()
        {
            if (Inquiry.IndustryId != null)
            {
                _eventAggregator.GetEvent<AfterNameChangedEvent>().Publish(
                    new AfterNameChangedEventArgs
                    {
                        Id = Inquiry.Id,
                        DisplayInquiry = Inquiry.Name,
                        DisplayIndustry = Industries.FirstOrDefault(p => p.Id == Inquiry.IndustryId).DisplayIndustry
                    }
                    );
            }
            else
            {
                _eventAggregator.GetEvent<AfterNameChangedEvent>().Publish(
                        new AfterNameChangedEventArgs
                        {
                            Id = Inquiry.Id,
                            DisplayInquiry = Inquiry.Name,
                        }
                        );
            }
        }
    }
}
