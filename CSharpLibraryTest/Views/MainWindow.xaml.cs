using CSharpLibraryTest.ViewModels;
using HelixToolkit.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Forms;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "OBJ File|*.obj";
            if (dialog.ShowDialog()
                == System.Windows.Forms.DialogResult.OK)
            {
                var stream = dialog.OpenFile();
                var path = dialog.FileName;
                var parentDir = Directory.GetParent(path);

                var objWriter = new ObjExporter();
                objWriter.TextureFolder = parentDir.FullName;
                objWriter.FileCreator = f => File.Create(System.IO.Path.Combine(parentDir.FullName, f));
                objWriter.MaterialsFile = System.IO.Path.ChangeExtension(Path.GetFileName(path), ".mtl");
                objWriter.Export(viewport, stream);

            }
        }
    }
}
