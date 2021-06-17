using OpenCvSharp;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Windows;

namespace CSharpLibraryTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("CSharpLibraryTest");
        public ReactiveProperty<string> Path { get; } = new ReactiveProperty<string>("");
        public ReactiveCommand LoadCommandA { get; } = new ReactiveCommand();

        public ReactiveProperty<Mat> Image { get; } = new ReactiveProperty<Mat>();

        private Func<MessageBoxResult> PathLoadFailDialog = () => MessageBoxResult.OK;



        public MainWindowViewModel()
        {
            LoadCommandA.Subscribe(PathLoad);
        }

        public void Initialize(Func<MessageBoxResult> pathLoadFailDialog)
        {
            PathLoadFailDialog = pathLoadFailDialog;
        }

        private void PathLoad()
        {
            Mat m = new Mat(Path.Value);
            if (m.Empty())
            {
                PathLoadFailDialog();
                return;
            }

            Image.Value = m;
        }
    }
}
