using Prism.Events;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.Event;

namespace View.DocumentationViews
{
    /// <summary>
    /// Logika interakcji dla klasy NavigationView.xaml
    /// </summary>
    public partial class DrawingsView : UserControl
    {
        private IEventAggregator _eventAggregator;

        public DrawingsView()
        {
            _eventAggregator = new EventAggregator();
            InitializeComponent();
        }

        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            _eventAggregator.GetEvent<AfterDrawingDoubleClickEvent>().Publish();
        }
    }
}
