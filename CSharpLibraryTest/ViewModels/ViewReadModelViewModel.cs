using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace CSharpLibraryTest.ViewModels
{
    public class ViewReadModelViewModel : BindableBase, IDialogAware
    {
        public ViewReadModelViewModel()
        {
            CancelCommand.Subscribe(CancelExecute);
            ReadFinishCommand.Subscribe(ReadFinishExecute);
        }

        public string Title => "Read Model";

        public event Action<IDialogResult> RequestClose;

        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();
        private ReactiveCommand<GeometryModel3D> ReadFinishCommand { get; } = new ReactiveCommand<GeometryModel3D>();

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var id = Task.CurrentId;

            string modelPath = parameters.GetValue<string>(nameof(ReadModel));
            _ = Task.Run(() =>
            {
                var id = Task.CurrentId;
                var m = ReadModel(modelPath);

                Application.Current.Dispatcher.Invoke(() =>
                this.ReadFinishCommand.Execute(m));
            });
        }

        private void CancelExecute()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void ReadFinishExecute(GeometryModel3D model)
        {
            var id = Task.CurrentId;

            var p = new DialogParameters();
            p.Add("Model", model);
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK, p));
        }

        static public GeometryModel3D ReadModel(string path)
        {
            var reader = new HelixToolkit.Wpf.StLReader();
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
