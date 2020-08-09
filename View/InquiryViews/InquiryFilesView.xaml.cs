using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Events;
using ViewModel.Event;
using Autofac;

namespace View.InquiryViews
{
    /// <summary>
    /// Logika interakcji dla klasy InquiryFilesView.xaml
    /// </summary>
    public partial class InquiryFilesView : UserControl
    {
        public delegate void afterDoubleClickDelagate();
        public event afterDoubleClickDelagate DoubleClicked;

        public InquiryFilesView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // DoubleClicked();
        }
    }
}
