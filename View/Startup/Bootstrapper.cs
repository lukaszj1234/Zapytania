using Autofac;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Prism.Events;
using ProjektInzynierski.DataAccess.InquiryContext;
using ViewModel.Data.Lookups;
using ViewModel.Data.Repositories;
using ViewModel.Services;
using ViewModel.ViewModels;
using ViewModel.ViewModelsInterafaces;

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
            

            builder.RegisterType<LookupDataService>().As<ILookupDataService>();
            builder.RegisterType<LookupDataService>().As<IFilesLookupService>();
            builder.RegisterType<LookupDataService>().As<IOfferLookupService>();
            builder.RegisterType<LookupDataService>().As<IReferenceOfferLookupService>();
            builder.RegisterType<LookupDataService>().As<IIndustryLookupDataService>();

            builder.RegisterType<InquiryRepository>().As<IInquiryRepository>();
            builder.RegisterType<ReferenceOfferRepository>().As<IReferenceOfferRepository>();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>();
            builder.RegisterType<AddedFilesRepository>().As<IAddedFilesRepository>();
            builder.RegisterType<SendedInquiryRespository>().As<ISendedInquiryRespository>();
            builder.RegisterType<IndsutryRepository>().As<IIndsutryRepository>();

            builder.RegisterType<FolderManager>().As<IFolderManager>();
            builder.RegisterType<FileManager>().As<IFileManager>();
            builder.RegisterType<XlsManager>().As<IXlsManager>();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<DialogCoordinator>().As<IDialogCoordinator>();
            

            return builder.Build();
        }
    }
}
