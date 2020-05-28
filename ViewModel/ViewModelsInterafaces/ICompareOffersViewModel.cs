using System.Threading.Tasks;
using ViewModel.Wrapper;

namespace ViewModel.ViewModelsInterafaces
{
    public interface ICompareOffersViewModel
    {
        Task LoadAsync(int? value);
    }
}