using CSharpLibraryTest.ViewModels;
using Reactive.Bindings.Interactivity;
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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

            var vm = DataContext as MainWindowViewModel;
            vm.Initialize(() => MessageBox.Show("読み込みに失敗しました", "画像読み込み失敗", MessageBoxButton.OK, MessageBoxImage.Warning));
        }


    }
}
