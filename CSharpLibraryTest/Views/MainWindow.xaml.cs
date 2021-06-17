using CSharpLibraryTest.ViewModels;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using Reactive.Bindings;
using System;
using System.Windows;

namespace CSharpLibraryTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings
            {
                MajorVersion = 2,
                MinorVersion = 1
            };
            OpenTKControl.Start(settings);

            var vm = DataContext as MainWindowViewModel;
            vm.Initialize(()=>MessageBox.Show("読み込みに失敗しました", "画像読み込み失敗", MessageBoxButton.OK, MessageBoxImage.Warning));
        }

        private void OpenTkControl_OnRender(TimeSpan delta)
        {
            var hue = 0.15f;
            var c = Color4.FromHsv(new Vector4(hue, 0.75f, 0.75f, 1));
            GL.ClearColor(c);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(Color4.Red);
            GL.Vertex2(0.0f, 0.5f);

            GL.Color4(Color4.Green);
            GL.Vertex2(0.58f, -0.5f);

            GL.Color4(Color4.Blue);
            GL.Vertex2(-0.58f, -0.5f);

            GL.End();
            GL.Finish();
        }
    }
}
