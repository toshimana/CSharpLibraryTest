using CSharpLibraryTest.Services;
using CSharpLibraryTest.ViewModels;
using Moq;
using Prism.Services.Dialogs;
using System;
using System.Windows.Media.Media3D;
using Xunit;

namespace CSharpLibraryTestUnitTest
{
    public class MainWindowViewModelTest
    {
        private MainWindowViewModel _mw;
        Mock<IDialogService> _dialogServiceMock;
        Mock<IMessageService> _messageServiceMock;
        public MainWindowViewModelTest()
        {
            _dialogServiceMock = new Mock<IDialogService>();
            _messageServiceMock = new Mock<IMessageService>();
            this._mw = new MainWindowViewModel(_dialogServiceMock.Object, _messageServiceMock.Object);
        }

        [Fact]
        public void LoadImageTest()
        {
            Assert.Null(_mw.Image.Value);

            var path = @"TestData\Lenna.jpg";
            _mw.ImagePath.Value = path;
            _mw.ImageLoadCommand.Execute();

            Assert.NotNull(_mw.Image.Value);
        }

        // https://stackoverflow.com/questions/64770095/testing-prism-dialogservice
        [Fact]
        public void LoadModelTest()
        {
            var dp = new DialogParameters();
            dp.Add("Model", new GeometryModel3D());

            _dialogServiceMock.Setup(x => x.ShowDialog(It.IsAny<string>(), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback<string, IDialogParameters, Action<IDialogResult>>((n, p, c) => c(new DialogResult(ButtonResult.OK, dp)));

            Assert.Null(_mw.ModelData.Value);

            var path = @"TestData\Hydrant.stl";
            _mw.ModelPath.Value = path;
            _mw.ModelLoadCommand.Execute();

            Assert.NotNull(_mw.ModelData.Value);
        }
    }
}
