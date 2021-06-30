using HelixToolkit.Wpf;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace CSharpLibraryTest.ViewModels
{
    public class ViewReadModelViewModel : BindableBase, IDialogAware
    {
        public ViewReadModelViewModel()
        {
            ReadFinishCommand = new ReactiveCommand<GeometryModel3D>();

            CancelCommand.Subscribe(CancelExecute);
            ReadFinishCommand.ObserveOnDispatcher().Subscribe(ReadFinishExecute);
        }

        public string Title => "Read Model";

        public event Action<IDialogResult> RequestClose;

        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();
        public ReactiveCommand<GeometryModel3D> ReadFinishCommand { get; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            string modelPath = parameters.GetValue<string>(nameof(ReadModel));
            _ = Task.Run(() =>
            {
                var id = Task.CurrentId;
                var m = ReadModel(modelPath);

                this.ReadFinishCommand.Execute(m);
            });
        }

        private void CancelExecute()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void ReadFinishExecute(GeometryModel3D model)
        {
            if (model == null) return;

            var id = Task.CurrentId;
            var p = new DialogParameters();
            p.Add("Model", model);
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK, p));
        }

        static public GeometryModel3D ReadModel(string path)
        {
            var reader = new StLReader();
            var ms = reader.Read(path);
            var gms = ms.Children[0] as GeometryModel3D;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"Image\Color.png", UriKind.Relative));
            DiffuseMaterial dm = new DiffuseMaterial(ib);

            gms.Material = dm;
            gms.Freeze();
            return gms;
        }
    }
}
