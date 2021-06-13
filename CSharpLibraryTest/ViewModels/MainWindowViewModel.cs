using Prism.Mvvm;
using Reactive.Bindings;

namespace CSharpLibraryTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("CSharpLibraryTest");

        public MainWindowViewModel()
        {

        }
    }
}
