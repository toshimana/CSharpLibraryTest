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
        public ReactiveProperty<string> Path { get; } = new ReactiveProperty<string>("");

        public PathLoader()
        {
            InitializeComponent();
        }
    }
}
