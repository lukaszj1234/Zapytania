
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public async Task<MahApps.Metro.Controls.Dialogs.MessageDialogResult> ShowOkCancelDialog(string text, string title)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            var result = await metroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);

            return result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative
                ? MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative
                : MahApps.Metro.Controls.Dialogs.MessageDialogResult.Canceled;
        }
        public enum MessageDialogResult
        {
            Ok,
            Cancel
        }

        public async Task ShowOkDialog(string text, string title)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            await metroWindow.ShowMessageAsync(title, text, MessageDialogStyle.Affirmative);
        }

        public async Task<string> ShowInputDialog(string text, string title)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            return await metroWindow.ShowInputAsync(title, text);
        }

     
    }
}
