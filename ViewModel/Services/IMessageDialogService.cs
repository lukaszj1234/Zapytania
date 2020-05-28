using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace ViewModel.Services
{
    public interface IMessageDialogService
    {
        Task<MessageDialogResult> ShowOkCancelDialog(string text, string title);
        Task ShowOkDialog(string text, string title);
        Task<string> ShowInputDialog(string text, string title);
    }
}