using CSharpLibraryTest.Services;
using CSharpLibraryTest.ViewModels;
using CSharpLibraryTest.Views;
using Prism.Ioc;
using System.Windows;

namespace CSharpLibraryTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ViewReadModel, ViewReadModelViewModel>();
            containerRegistry.Register<IMessageService, MessageService>();
        }
    }
}
