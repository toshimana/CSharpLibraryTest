using OpenCvSharp;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace CSharpLibraryTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("CSharpLibraryTest");
        public ReactiveProperty<string> ImagePath { get; } = new ReactiveProperty<string>("");
        public ReactiveCommand ImageLoadCommand { get; } = new ReactiveCommand();
        public ReactiveProperty<string> ModelPath { get; } = new ReactiveProperty<string>(@"C:\Users\toshi\Downloads\Hydrant\Hydrant.stl");
        public ReactiveCommand ModelLoadCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<Mat> Image { get; } = new ReactiveProperty<Mat>();
        public ReactiveProperty<Mat> WideImage { get; } = new ReactiveProperty<Mat>();

        private Func<MessageBoxResult> PathLoadFailDialog = () => MessageBoxResult.OK;

        public Model3DGroup TargetModel3DGroup = null;

        public MainWindowViewModel()
        {
            ImageLoadCommand.Subscribe(LoadImage);
            ModelLoadCommand.Subscribe(LoadModel);
        }

        public void Initialize(Func<MessageBoxResult> pathLoadFailDialog)
        {
            PathLoadFailDialog = pathLoadFailDialog;
        }

        private void LoadImage()
        {
            Mat m = new Mat(ImagePath.Value);
            if (m.Empty())
            {
                PathLoadFailDialog();
                return;
            }

            Image.Value = m;
            WideImage.Value = m;
        }
        
        private void LoadModel()
        {
            if (ModelPath == null) return;
            if (!File.Exists(ModelPath.Value)) return;

            var reader = new HelixToolkit.Wpf.StLReader();
            TargetModel3DGroup = reader.Read(ModelPath.Value);
            foreach (var model in TargetModel3DGroup.Children)
            {
                var m = model as GeometryModel3D;
                var meshs = m.Geometry as MeshGeometry3D;
            }
        }
    }
}
