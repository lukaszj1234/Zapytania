using System.Threading.Tasks;

namespace ViewModel.ViewModelsInterafaces.Documentation
{
    public interface IDrawingsViewModel
    {
        Task LoadAsync(int id);
    }
}