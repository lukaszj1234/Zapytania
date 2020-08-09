using Amazon.ElasticMapReduce.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Data.Lookups;
using ViewModel.Data.Repositories.Documentation;
using ViewModel.Items;
using ViewModel.ViewModels.ViewModelsInterafaces;
using Prism.Commands;
using ViewModel.Services;
using MahApps.Metro.Controls.Dialogs;
using ViewModel.Event;
using Prism.Events;

namespace ViewModel.ViewModels.DocumentationViewModels
{
    public class DocumentationNavigationViewModel : VievModelBase, IDocumentationNavigationViewModel
    {
        public ObservableCollection<LookupItem> Industry { get; }

        private IDrawingIndystryLookupService _industryLookupDataService;
        private IDrawingIndustryRepository _drawingIndustryRepository;

        public DelegateCommand RenameIndustryCommand { get; }
        public DelegateCommand DeleteIndustryCommand { get; }
        public DelegateCommand AddIndustryCommand { get; }

        private IMessageDialogService _messageDialogService;
        private IFolderManager _folderManager;
        private IEventAggregator _eventAggregator;
        private IDrawingsRepository _drawingsRepository;
        private IOutOfDateDrwaingRepository _outOfDateDrawingRepository;

        public DocumentationNavigationViewModel(IDrawingIndystryLookupService drawingIndustryLookup,
            IDrawingIndustryRepository drawingIndustryRepository, IMessageDialogService messageDialogService,
            IFolderManager folderManager, IEventAggregator eventAggregator, IDrawingsRepository drawingsRepository,
            IOutOfDateDrwaingRepository outOfDateDrawingRepository)
        {
            Industry = new ObservableCollection<LookupItem>();
            _industryLookupDataService = drawingIndustryLookup;
            _drawingIndustryRepository = drawingIndustryRepository;
            RenameIndustryCommand = new DelegateCommand(OnRenameIndustryCommand);
            DeleteIndustryCommand = new DelegateCommand(OnDeleteIndustryCommand);
            AddIndustryCommand = new DelegateCommand(OnAddIndustryCommand);
            _messageDialogService = messageDialogService;
            _folderManager = folderManager;
            _eventAggregator = eventAggregator;
            _drawingsRepository = drawingsRepository;
            _outOfDateDrawingRepository = outOfDateDrawingRepository;
        }

        private async void OnDeleteIndustryCommand()
        {
            int? id = SelectedIndustry?.Id;
            int newId = id ?? default(int);
            if (newId != 0)
            {
                var indsutry = await _drawingIndustryRepository.GetByIdAsync(newId);
                var result = await _messageDialogService.ShowOkCancelDialog("Usunięcie branży spowoduje usunięcie wszystkich " +
                    "przypisanych rysunków. Czy chcesz kontynuować?", "Potwierdź usunięcie branży");
                if (result == MessageDialogResult.Affirmative)
                {
                    _folderManager.DeleteFolder(indsutry.FolderPath);
                    var drawingList = _drawingsRepository.RemoveByIndustryId(indsutry.Id);
                    foreach (var item in drawingList)
                    {
                        _outOfDateDrawingRepository.RemoveByDrawingId(item.Id);
                    }
                    _drawingIndustryRepository.RemoveById(newId);
                    _eventAggregator.GetEvent<AfterDrawingIndustryDeletedEvent>().Publish(SelectedIndustry.Id);
                    await LoadAsync();
                }
            }
        }

        private async void OnRenameIndustryCommand()
        {
            if (SelectedIndustry != null)
            {
                var name = await _messageDialogService.ShowInputDialog("Wprowadź nazwę",
                   "Zmień nazwę");
                if (!String.IsNullOrEmpty(name))
                {
                    var check = _drawingIndustryRepository.GetByNameAsync(name);
                    if (check.Count == 0)
                    {
                        var industry = await _drawingIndustryRepository.GetByIdAsync(SelectedIndustry.Id);
                        industry.Name = name;
                        var newPath = _folderManager.RenameFolder(industry.FolderPath, name);
                        industry.FolderPath = newPath;
                        await _drawingIndustryRepository.SaveAsync();
                        var drawingList = _drawingsRepository.ChangePath(newPath, industry.Id);
                        _outOfDateDrawingRepository.ChangePaths(drawingList);
                        await LoadAsync();
                    }
                    else
                    {
                        await _messageDialogService.ShowOkDialog("Branża o podanej nazwie już istnieje. Prosze wybrać inną nazwę", "Zmień nazwę");
                    }
                }
            }
        }

        public DocumentationNavigationViewModel()
        {

        }
        private async void OnAddIndustryCommand()
        {
            var name = await _messageDialogService.ShowInputDialog("Wprowadź nazwę",
               "Dodawanie nowej branży");
            if (!String.IsNullOrEmpty(name))
            {
                var check = _drawingIndustryRepository.GetByNameAsync(name);
                if (check.Count == 0)
                {
                    var mainPath = _folderManager.CreateMainDocumentationFolder();
                    var folderPath = _folderManager.CreateNewFolder(mainPath, name);
                    _drawingIndustryRepository.Add(name, folderPath);
                    await LoadAsync();
                }
                else
                {
                    await _messageDialogService.ShowOkDialog("Branża o podanej nazwie już istnieje. Prosze wybrać inną nazwę", "Zmień nazwę");
                }
            }
        }

        public async Task LoadAsync()
        {
            Industry.Clear();
            var industryLookup = await _industryLookupDataService.GetDrawingIndustryLookupAsync();
            foreach (var item in industryLookup)
            {
                Industry.Add(item);
            }
        }

        private LookupItem _selectedIndustry;
        public LookupItem SelectedIndustry
        {
            get { return _selectedIndustry; }
            set
            {
                _selectedIndustry = value;
                OnPropertyChanged();
                if (_selectedIndustry != null)
                {
                    _eventAggregator.GetEvent<AfterDrawingIndustryClickedEvent>().Publish(_selectedIndustry.Id);
                }
            }
        }
    }
}
