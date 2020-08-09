using Prism.Events;
using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Data.Repositories.Documentation;
using ViewModel.Event;
using ViewModel.ViewModelsInterafaces.Documentation;

namespace ViewModel.ViewModels.DocumentationViewModels
{
    public class OutOfDateDrawingsViewModel : VievModelBase, IOutOfDateDrawingsViewModel
    {
        private string _lastUpdateMessage;
        private string _dateMessage;
        private string _nameMessage;
        private IOutOfDateDrwaingRepository _outOfDateDrawingRepository;

        public ObservableCollection<DrawingOutOfDate> OutOfDateDrawing { get; }
        public ObservableCollection<Drawing> ActualDrawing { get; }

        private IDrawingsRepository _drawingsRepository;
        private IEventAggregator _eventAggregator;

        public OutOfDateDrawingsViewModel(IDrawingsRepository drawingsRepository, IEventAggregator eventAggregator,
            IOutOfDateDrwaingRepository outOfDateDrawingRepository)
        {
            _outOfDateDrawingRepository = outOfDateDrawingRepository;
            OutOfDateDrawing = new ObservableCollection<DrawingOutOfDate>();
            ActualDrawing = new ObservableCollection<Drawing>();
            _drawingsRepository = drawingsRepository;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterDrawingDeletedEvent>().Subscribe(AfterDrawingDeleted);
            _eventAggregator.GetEvent<AfterDrawnigUpdateEvent>().Subscribe(AfterDrawnigUpdate);
            _eventAggregator.GetEvent<AfterDrawingIndustryClickedEvent>().Subscribe(AfterDrawingIndustryClicked);
        }

        private void AfterDrawingIndustryClicked(int? obj)
        {
            ClearMessage();
        }

        private async void AfterDrawnigUpdate(int obj)
        {
            await LoadAsync(obj);
        }

        private void AfterDrawingDeleted(int? obj)
        {
            ClearMessage();
        }

        public async Task LoadAsync(int drawingId)
        {
            ActualDrawing.Clear();
            OutOfDateDrawing.Clear();
            Drawing actualDrawing;
            using (var context = new InquiryContext())
            {
                actualDrawing = context.Drawings.Single(f => f.Id == drawingId);
            }
            //var actualDrawing = await _drawingsRepository.GetByIdAsync(drawingId);
            ActualDrawing.Add(actualDrawing);
            var drawing = _outOfDateDrawingRepository.GettAllFilesByDrawingId(drawingId);
            foreach (var item in drawing)
            {
                OutOfDateDrawing.Add(item);
            }
            GetMessage(drawingId);
        }

        public string NameMessage
        {
            get { return _nameMessage; }
            set
            {
                _nameMessage = value;
                OnPropertyChanged();
            }
        }
        public string DateMessage
        {
            get { return _dateMessage; }
            set
            {
                _dateMessage = value;
                OnPropertyChanged();
            }
        }

        public string LastUpdateMessage
        {
            get { return _lastUpdateMessage; }
            set
            {
                _lastUpdateMessage = value;
                OnPropertyChanged();
            }
        }
        private void GetMessage(int drawingId)
        {
            Drawing actualDrawing;
            using (var context = new InquiryContext())
            {
                actualDrawing = context.Drawings.Single(f => f.Id == drawingId);
            }
            NameMessage = "Rysunek: " + actualDrawing.Name;
            DateMessage = "Dodano dnia: " + actualDrawing.AddDate;
            LastUpdateMessage = "Ostatnia aktualizacja: " + actualDrawing.LastUpdateDate;
        }
        private void ClearMessage()
        {
            NameMessage = null;
            DateMessage = null;
            LastUpdateMessage = null;
        }
    }
}
