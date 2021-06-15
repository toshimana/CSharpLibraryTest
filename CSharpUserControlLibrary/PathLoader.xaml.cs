using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpUserControlLibrary
{
    /// <summary>
    /// PathLoader.xaml の相互作用ロジック
    /// </summary>
    public partial class PathLoader : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(PathLoader), new PropertyMetadata("", OnTitleChanged));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PathLoader ctrl = obj as PathLoader;
            if (ctrl != null)
            {
                ctrl.Title = e.NewValue as string;
            }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path), typeof(string), typeof(PathLoader), new PropertyMetadata("", OnPathChanged));
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        public static void OnPathChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PathLoader ctrl = obj as PathLoader;
            if (ctrl != null)
            {
                ctrl.Path = e.NewValue as string;
            }
        }

        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register(nameof(LoadCommand), typeof(ICommand), typeof(PathLoader), new PropertyMetadata(null));

        public PathLoader()
        {
            InitializeComponent();
        }


    }
}
