using GalaSoft.MvvmLight.Command;
using Model;
using Prism.Commands;
using Prism.Events;
using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using ViewModel.Data.Repositories.Documentation;
using ViewModel.Event;
using ViewModel.ViewModelsInterafaces.Documentation;

namespace ViewModel.ViewModels.DocumentationViewModels
{

    public class DrawingsViewModel : VievModelBase, IDrawingsViewModel
    {
        private IOutOfDateDrwaingRepository _outOfDateDrawingRepository;
        private IDrawingsRepository _drawingRepository;
        public DelegateCommand AddDrawingCommand { get; }
        public DelegateCommand DeleteDrawingCommand { get; }
        public DelegateCommand UpdateCommand { get; }
        public DelegateCommand DoubleClickCommand { get; }
        public ObservableCollection<Drawing> Drawing { get; }

        private int _industryId;

        private IEventAggregator _eventAggregator;
        private IFileManager _fileManager;
        private IFolderManager _folderManager;
        private IDrawingIndustryRepository _drawingIndustryRepository;

        public DrawingsViewModel()
        {

        }
        public DrawingsViewModel(IDrawingsRepository drawingRepository, IEventAggregator eventAggregator, IFileManager fileManager,
            IFolderManager folderManager, IDrawingIndustryRepository drawingIndustryRepository, IOutOfDateDrwaingRepository outOfDateDrwaingRepository)
        {
            _outOfDateDrawingRepository = outOfDateDrwaingRepository;
            _drawingRepository = drawingRepository;
            AddDrawingCommand = new DelegateCommand(OnAddDrawingExecute);
            DeleteDrawingCommand = new DelegateCommand(OnDeleteDrawingExecute);
            UpdateCommand = new DelegateCommand(OnUpdateCommandExecute);
            DoubleClickCommand = new DelegateCommand(OnDoubleClickExecute);
            Drawing = new ObservableCollection<Drawing>();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterDrawingDoubleClickEvent>().Subscribe(OnDoubleClickExecute);
            _fileManager = fileManager;
            _folderManager = folderManager;
            _drawingIndustryRepository = drawingIndustryRepository;
        }

        private void OnDoubleClickExecute()
        {
            throw new NotImplementedException();
        }

       

        private async void OnUpdateCommandExecute()
        {
            if (SelectedDrawing != null)
            {
                var existingOutOfDatDrawings = _outOfDateDrawingRepository.GettAllFilesByDrawingId(SelectedDrawing.Id);
                var drawing = await _drawingRepository.GetByIdAsync(SelectedDrawing.Id);
                string uptadeDrawingPath = _fileManager.GetUpdateDrawingPath();
                if (uptadeDrawingPath != null)
                {
                    var outOfDateFolderPath = _folderManager.CreateNewFolder(Path.GetDirectoryName(drawing.Path), "Archiwum");
                    var outOfDateFilePath = Path.Combine(outOfDateFolderPath, Path.GetFileName(drawing.Path));
                    if ((!existingOutOfDatDrawings.Exists(f => f.Path == outOfDateFilePath)) &&
                        Path.GetFileName(uptadeDrawingPath) != SelectedDrawing.LastUpdateName)
                    {
                        _outOfDateDrawingRepository.Add(new DrawingOutOfDate()
                        {
                            Name = drawing.LastUpdateName,
                            Path = outOfDateFilePath,
                            DrawingId = drawing.Id
                        });
                        _fileManager.CopyFiles(drawing.Path,outOfDateFolderPath);
                        _fileManager.DeleteFile(drawing.Path);
                        drawing.LastUpdateDate = DateTime.Now.ToString();
                        drawing.Path = Path.Combine(Path.GetDirectoryName(drawing.Path), Path.GetFileName(uptadeDrawingPath));
                        drawing.LastUpdateName = Path.GetFileName(drawing.Path);
                        await _drawingRepository.SaveAsync();
                        _fileManager.CopyFiles(uptadeDrawingPath, Path.GetDirectoryName(drawing.Path));
                        var drawingIndustry = await _drawingIndustryRepository.GetByIdAsync(_industryId);
                        drawingIndustry.LastUpdate = DateTime.Now.ToString();
                        await _drawingIndustryRepository.SaveAsync();
                    }
                }
                _eventAggregator.GetEvent<AfterDrawnigUpdateEvent>().Publish(SelectedDrawing.Id);
            }
        }

        private async void OnDeleteDrawingExecute()
        {
            if (SelectedDrawing!=null)
            {
                _folderManager.DeleteFolder(Path.GetDirectoryName(SelectedDrawing.Path));
                _outOfDateDrawingRepository.RemoveByDrawingId(SelectedDrawing.Id);
                _drawingRepository.RemoveById(SelectedDrawing.Id);
                _eventAggregator.GetEvent<AfterDrawingDeletedEvent>().Publish(SelectedDrawing.Id);
                await LoadAsync(_industryId);
            }
        }

        public async Task LoadAsync(int industryId)
        {
            Drawing.Clear();
            _industryId = industryId;
            var drawings = _drawingRepository.GettAllFilesByIndustryId(industryId);
            foreach (var item in drawings)
            {
                Drawing.Add(item);
            }
        }

        private async void OnAddDrawingExecute()
        {
            var existingFiles = _drawingRepository.GettAllFilesByIndustryId(_industryId);
            var industry =  await _drawingIndustryRepository.GetByIdAsync(_industryId);
            var addedDrawingsPaths = _fileManager.GetAddedDrawingsPath();
            foreach (var item in addedDrawingsPaths)
            {
                if (!existingFiles.Exists(f => f.Path == item))
                {
                    var dstPath = _folderManager.CreateNewFolder(industry.FolderPath, Path.GetFileName(item));
                    var drawingPath=_fileManager.CopyFiles(item, dstPath);
                    _drawingRepository.Add(new Drawing() { Name = Path.GetFileName(item), Path = drawingPath,
                        IndustryId = _industryId, LastUpdateName = Path.GetFileName(item)
                    });
                    var drawingIndustry = await _drawingIndustryRepository.GetByIdAsync(_industryId);
                    drawingIndustry.LastUpdate = DateTime.Now.ToString();
                    await _drawingIndustryRepository.SaveAsync();
                }
            }
            await LoadAsync(_industryId);
            _eventAggregator.GetEvent<AfterDrawingAddEvent>().Publish(_industryId);
        }

        private Drawing _selectedDrawing;
        public Drawing SelectedDrawing
        {
            get { return _selectedDrawing; }
            set
            {
                _selectedDrawing = value;
                OnPropertyChanged();
                if (_selectedDrawing != null)
                {
                    _eventAggregator.GetEvent<AfterDrawingClickedEvent>().Publish(_selectedDrawing.Id);
                }
            }
        }
    }
}
