using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModelsInterafaces
{
    public interface ISendedInquiryViewModel
    {
        Task LoadAsync(int? inquiryId);
    }
}
