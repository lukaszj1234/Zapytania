using System.Threading.Tasks;

namespace ViewModel.ViewModelsInterafaces.Documentation
{
    public interface IOutOfDateDrawingsViewModel
    {
        Task LoadAsync(int drawingId);
    }
}