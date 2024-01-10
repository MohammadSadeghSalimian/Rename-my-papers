using Autofac;
using PaperRename2.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using ReactiveUI;
using Splat.Autofac;

namespace PaperRename2.Services
{
    public class AppBootStrapWpf
    {
        private ContainerBuilder _builder;
        public void Setup()
        {
            _builder = new ContainerBuilder();

            RegisterServices();
            RegisterViews();
            RegisterViewModels();

            SetAutofac();
        }
        protected void RegisterServices()
        {
            _builder.RegisterInstance(DialogCoordinator.Instance).As<IDialogCoordinator>().SingleInstance();
            _builder.RegisterType<PaperModel>().As<IPaperModel>().AsSelf().SingleInstance();
            _builder.RegisterType<PdfManagerPdfSharp>().As<IPdfManager>().AsSelf().SingleInstance();
            _builder.RegisterType<DialogBuilder>().As<ICommonDialogBuilder>().AsSelf().SingleInstance();
            _builder.RegisterType<MessageUnit>().As<IMessageUnit>().AsSelf().SingleInstance();
            _builder.RegisterType<TextController>().As<ITextController>().AsSelf().SingleInstance();
            _builder.RegisterType<KeyContainer>().As<IKeyContainer>().AsSelf().SingleInstance();
            _builder.RegisterType<EventContainer>().As<IEventContainer>().AsSelf().SingleInstance();
            _builder.RegisterType<SharedModel>().As<ISharedModel>().AsSelf().SingleInstance();
            _builder.RegisterType<FolderManager>().As<IFolderManager>().AsSelf().SingleInstance();

            var configuration = MediatRConfigurationBuilder
                .Create(typeof(AppBootStrapWpf).Assembly)
                .WithAllOpenGenericHandlerTypesRegistered() // Register all handlers by convention
                .WithRegistrationScope(RegistrationScope.Scoped) // Set the registration scope
                .Build();
            _builder.RegisterMediatR(configuration);
        }
        protected void RegisterViewModels()
        {
            _builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            _builder.RegisterType<FileListVm>().AsSelf().SingleInstance();
            _builder.RegisterType<EditVm>().AsSelf().SingleInstance();
        }
        protected void RegisterViews()
        {
        }
        private void SetAutofac()
        {
            var autofacResolver = _builder.UseAutofacDependencyResolver();
            _builder.RegisterInstance(autofacResolver);
            autofacResolver.InitializeReactiveUI();
            var container = _builder.Build();
            container.Resolve<AutofacDependencyResolver>();
            autofacResolver.SetLifetimeScope(container);
        }
    }
}