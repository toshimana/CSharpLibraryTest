using HelixToolkit.Wpf;
using OpenCvSharp;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        public ReactiveProperty<Camera> Camera { get; } = new ReactiveProperty<Camera>();
        public ReactiveProperty<Model3D> Light { get; } = new ReactiveProperty<Model3D>();

        public ReactiveProperty<Model3D> ModelData { get; } = new ReactiveProperty<Model3D>();

        public ReactiveProperty<Model3D> MarkerData { get; } = new ReactiveProperty<Model3D>();

        // https://stackoverflow.com/questions/32885077/draw-point-where-mouse-clicked
        public ReactiveCommand<Point3D> ClickPoint { get; } = new ReactiveCommand<Point3D>();

        public MainWindowViewModel()
        {
            ImageLoadCommand.Subscribe(LoadImage);
            ModelLoadCommand.Subscribe(LoadModel);
            ClickPoint.Subscribe(ClickModelPoints);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = new Point3D(0, 0, 2000);
            camera.LookDirection = new Vector3D(0, 0, -1);
            camera.FieldOfView = 60;

            Camera.Value = camera;

            DirectionalLight light = new DirectionalLight();
            light.Color = Colors.White;
            light.Direction = new Vector3D(0, 0, -1);
            Light.Value = light;
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
            var ms = reader.Read(ModelPath.Value);
            ModelData.Value = ms.Children[0];
        }


        private void ClickModelPoints(Point3D p)
        {
            var sphere = new SphereVisual3D();
            sphere.Center = p;
            sphere.Radius = 10.0;
            sphere.Fill = new SolidColorBrush(Colors.Red);
            sphere.UpdateModel();

            MarkerData.Value = sphere.Model;
        }
    }
}
