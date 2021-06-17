using Reactive.Bindings;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace CSharpUserControlLibrary
{
    /// <summary>
    /// PathLoader.xaml の相互作用ロジック
    /// </summary>
    public partial class PathLoader : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(PathLoader), new PropertyMetadata(""));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path), typeof(string), typeof(PathLoader), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathFilterProperty =
            DependencyProperty.Register(nameof(PathFilter), typeof(string), typeof(PathLoader), new PropertyMetadata("*|*.*"));
        public string PathFilter
        {
            get { return (string)GetValue(PathFilterProperty); }
            set { SetValue(PathFilterProperty, value); }
        }

        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register(nameof(LoadCommand), typeof(ICommand), typeof(PathLoader), new PropertyMetadata(null));
        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }

        public PathLoader()
        {
            InitializeComponent();

            IObservable<RoutedEventArgs> observable = Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(
                h => (s, e) => h(e),
                h => PathFindButton.Click += h,
                h => PathFindButton.Click -= h);

            observable.Subscribe(FindPath);
        }

        private void FindPath(RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            string currentPath = PathTextBox.Text;
            if (currentPath.Length == 0) currentPath = ".";
            string fullPath = System.IO.Path.GetFullPath(currentPath);
            dialog.InitialDirectory = Directory.GetParent(fullPath).FullName;
            dialog.Filter = PathFilter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PathTextBox.Text = dialog.FileName;
            }
        }
    }
}
