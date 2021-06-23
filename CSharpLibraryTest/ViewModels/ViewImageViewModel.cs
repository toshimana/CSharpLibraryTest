using OpenCvSharp;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace CSharpLibraryTest.ViewModels
{
    public class ViewImageViewModel : BindableBase
    {
        public ReactiveProperty<Mat> Image { get; } = new ReactiveProperty<Mat>();
        public ReactiveProperty<Mat> WideImage { get; } = new ReactiveProperty<Mat>();

        public Model3DGroup TargetModel3DGroup = null;

        public ViewImageViewModel()
        {

        }
    }
}
