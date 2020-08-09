using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Data.Repositories.Documentation;
using ViewModel.Data.Repositories.Inqury;
using ViewModel.Event;
using ViewModel.ViewModels.ViewModelsInterafaces;
using ViewModel.ViewModelsInterafaces;
using ViewModel.ViewModelsInterafaces.Documentation;


namespace ViewModel.ViewModels.InquiryViewModels
{

    public class MainWindowViewModel : VievModelBase
    {
        public ICommand ButtonCommand { get; set; }
        public ICommand CopyFileCommand { get; set; }
        public ICommand CreateNewInquiryCommand { get; }
        public INavigationViewModel NavigationViewModel { get; }
        public IDocumentationNavigationViewModel DocumentationNavigationViewModel { get; }

        private IInquiryFilesViewModel _inquiryFilesViewModel;
        private IOfferViewModel _offerViewModel;
        private ICompareOffersViewModel _compareOfferViewModel;
        private IDrawingsViewModel _drawingsViewModel;
        private IOutOfDateDrawingsViewModel _outOfDateDrawingsViewModel;

        private IEventAggregator _eventAggregator;
        private Func<IInquiryFilesViewModel> _inquiryFilesViewModelCreator;
        private Func<IOfferViewModel> _offerViewModelCreator;
        private Func<ICompareOffersViewModel> _compareOfferViewModelCreator;
        private Func<ISendedInquiryViewModel> _sendedInquiryViewModelCreator;
        private Func<IDrawingsViewModel> _drawingsViewModelCreator;
        private Func<IOutOfDateDrawingsViewModel> _outOfDateDrawingsViewModelCreator;

        private IInquiryRepository _inquiryRepository;
        private IIndsutryRepository _indsutryRepository;
        private IDrawingsRepository _drawingRepository;
        private IDrawingIndustryRepository _drawingIndustryRepository;
        private ISendedInquiryViewModel _sendedInquiryViewModel;
        public MainWindowViewModel(
          INavigationViewModel navigationViewModel, IDocumentationNavigationViewModel documentationNavigationViewModel,
          Func<IInquiryFilesViewModel> inquiryFilesViewModelCreator,
          Func<IOfferViewModel> offerViewModelCreator, Func<IDrawingsViewModel> drawingsViewModelCreator,
          Func<ICompareOffersViewModel> compareOfferViewModelCreator, Func<ISendedInquiryViewModel> sendedInquiryViewModelCreator,
          Func<IOutOfDateDrawingsViewModel> outOfDateDrawingsViewModelCreator,
          IEventAggregator eventAggregator, IInquiryRepository inquiryRepository, IDrawingsRepository drawingsRepository,
          IIndsutryRepository indsutryRepository, IDrawingIndustryRepository drawingIndustryRepository)
        {
            DocumentationNavigationViewModel = documentationNavigationViewModel;
            NavigationViewModel = navigationViewModel;
            _inquiryFilesViewModelCreator = inquiryFilesViewModelCreator;
            _offerViewModelCreator = offerViewModelCreator;
            _drawingsViewModelCreator = drawingsViewModelCreator;
            _compareOfferViewModelCreator = compareOfferViewModelCreator;
            _sendedInquiryViewModelCreator = sendedInquiryViewModelCreator;
            _outOfDateDrawingsViewModelCreator = outOfDateDrawingsViewModelCreator;
            _inquiryRepository = inquiryRepository;
            _indsutryRepository = indsutryRepository;
            _drawingRepository = drawingsRepository;
            _drawingIndustryRepository = drawingIndustryRepository;
            CreateNewInquiryCommand = new DelegateCommand(OnCreateNewInquiryExecute);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DisplayInquiryFilesEvent>().Subscribe(SelectedInquiryFilesView);
            _eventAggregator.GetEvent<AfterInquiryDeletedEvent>().Subscribe(AfterInquiryDeleted);
            _eventAggregator.GetEvent<AfterNameChangedEvent>().Subscribe(AfterNameChanged);
            _eventAggregator.GetEvent<AfterDrawingIndustryClickedEvent>().Subscribe(AfterDrawingIndustryClicked);
            _eventAggregator.GetEvent<AfterDrawingIndustryDeletedEvent>().Subscribe(AfterDrawingIndustryDeleted);
            _eventAggregator.GetEvent<AfterDrawingClickedEvent>().Subscribe(AfterDrawingClicked);
            _eventAggregator.GetEvent<AfterDrawingDeletedEvent>().Subscribe(AfterDrawingDeleted);
            _eventAggregator.GetEvent<AfterDrawingAddEvent>().Subscribe(AfterDrawingAdd);
            _eventAggregator.GetEvent<AfterDrawnigUpdateEvent>().Subscribe(AfterDrawingUpdate);
        }

        private void AfterDrawingUpdate(int obj)
        {
            GetDrawingMessage(obj);
        }

        private void AfterDrawingAdd(int obj)
        {
            GetDrawingMessage(obj);
        }
        #region Drawing
        private void AfterDrawingDeleted(int? obj)
        {
            OutOfDateDrawingsViewModel = null;
        }

        private async void AfterDrawingClicked(int? id)
        {
            int? id1 = id;
            int newId = id1 ?? default(int);
            OutOfDateDrawingsViewModel = _outOfDateDrawingsViewModelCreator();
            await OutOfDateDrawingsViewModel.LoadAsync(newId);
        }

        private void AfterDrawingIndustryDeleted(int obj)
        {
            DrawingsViewModel = null;
            OutOfDateDrawingsViewModel = null;
        }

        private async void AfterDrawingIndustryClicked(int? id)
        {
            int? id1 = id;
            int newId = id1 ?? default(int);
            GetDrawingMessage(newId);
            OutOfDateDrawingsViewModel = null;
            DrawingsViewModel = _drawingsViewModelCreator();
            await DrawingsViewModel.LoadAsync(newId);
        }
        public IDrawingsViewModel DrawingsViewModel
        {
            get { return _drawingsViewModel; }
            set
            {
                _drawingsViewModel = value;
                OnPropertyChanged();
            }
        }

        public IOutOfDateDrawingsViewModel OutOfDateDrawingsViewModel
        {
            get { return _outOfDateDrawingsViewModel; }
            set
            {
                _outOfDateDrawingsViewModel = value;
                OnPropertyChanged();
            }
        }
        private string _documentationNameMessage;

        public string DocumentationNameMessage
        {
            get { return _documentationNameMessage; }
            set
            {
                _documentationNameMessage = value;
                OnPropertyChanged();
            }
        }

        private string _documentationDateMessage;

        public string DocumentationDateMessage
        {
            get { return _documentationDateMessage; }
            set
            {
                _documentationDateMessage = value;
                OnPropertyChanged();
            }
        }
        private async void GetDrawingMessage(int inquiryId)
        {
            var industry = await _drawingIndustryRepository.GetByIdAsync(inquiryId);
            DocumentationNameMessage = "Branża: " + industry.Name;
            DocumentationDateMessage = "Ostatnia aktualizacja: " + industry.LastUpdate;
        }

        #endregion
        #region Inquiry
        private void OnAddDrawingExecute()
        {
            throw new NotImplementedException();
        }

        private void AfterNameChanged(AfterNameChangedEventArgs obj)
        {
            SetMessage(obj.DisplayInquiry, obj.Id);
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
            await DocumentationNavigationViewModel.LoadAsync();

        }

        public IInquiryFilesViewModel InquiryFilesViewModel
        {
            get { return _inquiryFilesViewModel; }
            set
            {
                _inquiryFilesViewModel = value;
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
            set
            {
                _nameMessage = value;
                OnPropertyChanged();
            }
        }

        private string _dateMessage;

        public string DateMessage
        {
            get { return _dateMessage; }
            set
            {
                _dateMessage = value;
                OnPropertyChanged();
            }
        }

        private async void SelectedInquiryFilesView(int? inquiryId)
        {
            InquiryFilesViewModel = _inquiryFilesViewModelCreator();
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

        private async void GetMessage(int? inquiryId)
        {
            var inquiry = await _inquiryRepository.GetByIdAsync(inquiryId);
            NameMessage = "Zapytanie: " + inquiry.Name;
            DateMessage = "Utworzono dnia: " + inquiry.Time;
        }

        private async void SetMessage(string name, int id)
        {
            var inquiry = await _inquiryRepository.GetByIdAsync(id);
            NameMessage = "Zapytanie: " + name;
            DateMessage = "Utworzono dnia: " + inquiry.Time;
        }
        #endregion
    }
}
