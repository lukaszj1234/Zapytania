
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using ViewModel.ViewModels;
using ViewModel.ViewModels.InquiryViewModels;

namespace View
{

    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
