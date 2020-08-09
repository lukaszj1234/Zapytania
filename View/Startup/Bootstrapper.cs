using Autofac;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Prism.Events;
using ProjektInzynierski.DataAccess.InquiryContext;
using ViewModel.Data.Lookups;
using ViewModel.Data.Repositories;
using ViewModel.Data.Repositories.Documentation;
using ViewModel.Data.Repositories.Inqury;
using ViewModel.Services;
using ViewModel.ViewModels;
using ViewModel.ViewModels.DocumentationViewModels;
using ViewModel.ViewModels.InquiryViewModels;
using ViewModel.ViewModels.ViewModelsInterafaces;
using ViewModel.ViewModelsInterafaces;
using ViewModel.ViewModelsInterafaces.Documentation;

namespace View.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap() {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<InquiryContext>().AsSelf();

            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<InquiryFilesViewModel>().As<IInquiryFilesViewModel>();
            builder.RegisterType<CompareOffersViewModel>().As<ICompareOffersViewModel>();
            builder.RegisterType<SendedInquiryViewModel>().As<ISendedInquiryViewModel>();
            builder.RegisterType<OfferViewModel>().As<IOfferViewModel>();
            builder.RegisterType<DocumentationNavigationViewModel>().As<IDocumentationNavigationViewModel>();

            builder.RegisterType<DrawingsViewModel>().As<IDrawingsViewModel>();
            builder.RegisterType<OutOfDateDrawingsViewModel>().As<IOutOfDateDrawingsViewModel>();
            
            builder.RegisterType<LookupDataService>().As<ILookupDataService>();
            builder.RegisterType<LookupDataService>().As<IFilesLookupService>();
            builder.RegisterType<LookupDataService>().As<IOfferLookupService>();
            builder.RegisterType<LookupDataService>().As<IReferenceOfferLookupService>();
            builder.RegisterType<LookupDataService>().As<IIndustryLookupDataService>();

            builder.RegisterType<LookupDataService>().As<IDrawingIndystryLookupService>();

            builder.RegisterType<InquiryRepository>().As<IInquiryRepository>();
            builder.RegisterType<ReferenceOfferRepository>().As<IReferenceOfferRepository>();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>();
            builder.RegisterType<AddedFilesRepository>().As<IAddedFilesRepository>();
            builder.RegisterType<SendedInquiryRespository>().As<ISendedInquiryRespository>();
            builder.RegisterType<IndsutryRepository>().As<IIndsutryRepository>();

            builder.RegisterType<DrawingsRepository>().As<IDrawingsRepository>();
            builder.RegisterType<DrawingIndustryRepository>().As<IDrawingIndustryRepository>();
            builder.RegisterType<OutOfDateDrwaingRepository>().As<IOutOfDateDrwaingRepository>();

            builder.RegisterType<FolderManager>().As<IFolderManager>();
            builder.RegisterType<FileManager>().As<IFileManager>();
            builder.RegisterType<XlsManager>().As<IXlsManager>();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<DialogCoordinator>().As<IDialogCoordinator>();
            

            return builder.Build();
        }
    }
}
