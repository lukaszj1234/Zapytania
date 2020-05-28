using System.Threading.Tasks;

namespace ViewModel.ViewModelsInterafaces
{
    public interface IInquiryFilesViewModel
    {
        Task LoadAsync(int? inquiryId);
    }
}