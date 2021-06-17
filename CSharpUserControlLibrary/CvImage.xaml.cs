using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
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
    /// CvImage.xaml の相互作用ロジック
    /// </summary>
    public partial class CvImage : UserControl
    {
        public static readonly DependencyProperty MatProperty =
            DependencyProperty.Register(nameof(MatImage), typeof(Mat), typeof(CvImage), new PropertyMetadata(null, OnPathChanged));
        public Mat MatImage
        {
            get { return (Mat)GetValue(MatProperty); }
            set { SetValue(MatProperty, value); }
        }
        public static void OnPathChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CvImage ctrl = obj as CvImage;
            if (ctrl != null)
            {
                Mat m = e.NewValue as Mat;
                BitmapSource source = BitmapSourceConverter.ToBitmapSource(m);
                ctrl.ImageControl.Source = source;
            }
        }

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(nameof(MatStretch), typeof(Stretch), typeof(CvImage), new PropertyMetadata(Stretch.Uniform));

        public Stretch MatStretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public CvImage()
        {
            InitializeComponent();
        }
    }
}
