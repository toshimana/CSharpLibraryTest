using Prism.Mvvm;
using Reactive.Bindings;
using System;

namespace CSharpLibraryTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("CSharpLibraryTest");
        public ReactiveProperty<string> Path { get; } = new ReactiveProperty<string>("aaa");

        public MainWindowViewModel()
        {
        }
    }
}
