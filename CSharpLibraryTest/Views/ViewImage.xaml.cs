using CSharpLibraryTest.ViewModels;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace CSharpLibraryTest.Views
{
    /// <summary>
    /// Interaction logic for ViewImage
    /// </summary>
    public partial class ViewImage : UserControl
    {
        public ViewImage()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings
            {
                MajorVersion = 2,
                MinorVersion = 1
            };
            OpenTKControl.Start(settings);
        }

        private void OpenTkControl_OnRender(TimeSpan delta)
        {
            var vm = DataContext as ViewImageViewModel;
            if (vm == null) return;
            if (vm.TargetModel3DGroup == null) return;

            var hue = 0.15f;
            var c = Color4.FromHsv(new Vector4(hue, 0.75f, 0.75f, 1));
            GL.ClearColor(c);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Viewport(0, 0, (int)OpenTKControl.ActualWidth, (int)OpenTKControl.ActualHeight);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float aspect = (float)(OpenTKControl.ActualWidth / OpenTKControl.ActualHeight);
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(1.5708f, aspect, 0.01f, 10000.0f);
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            foreach (var model in vm.TargetModel3DGroup.Children)
            {
                var m = model as GeometryModel3D;
                var meshs = m.Geometry as MeshGeometry3D;

                Vector3 eye = new Vector3(0.0f, 0.0f, 500.0f);
                Point3D tp = meshs.Bounds.Location;
                Vector3 target = new Vector3((float)tp.X, (float)tp.Y, (float)tp.Z);
                Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
                Matrix4 look = Matrix4.LookAt(eye, target, up);
                GL.LoadMatrix(ref look);

                GL.Begin(PrimitiveType.Points);
                GL.Color4(Color4.Blue);
                GL.Vertex3(target);
                GL.End();

                Point3DCollection point3s = meshs.Positions;

                //int VertexBufferObject = GL.GenBuffer();
                //GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                //GL.BufferData(BufferTarget.ArrayBuffer, point3s.Count, point3s);
                GL.Begin(PrimitiveType.Points);
                foreach (Point3D p in point3s)
                {
                    GL.Color4(Color4.Red);
                    GL.Vertex3(p.X, p.Y, p.Z);
                }
                GL.End();
            }
            GL.Finish();
        }

    }
}
