using System.Threading.Tasks;
using ViewModel.ViewModelsInterafaces;

namespace ViewModel.ViewModelsInterafaces
{
    public interface IMainInquiryViewModel
    {
        string DateMessage { get; set; }
        IInquiryFilesViewModel InquiryFilesViewModel { get; set; }
        INavigationViewModel NavigationViewModel { get; }

        Task LoadAsync();
    }
}