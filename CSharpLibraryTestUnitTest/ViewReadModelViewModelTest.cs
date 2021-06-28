using CSharpLibraryTest.ViewModels;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CSharpLibraryTestUnitTest
{
    public class ViewReadModelViewModelTest
    {
        private readonly ViewReadModelViewModel _vm;
        public ViewReadModelViewModelTest()
        {
            _vm = new ViewReadModelViewModel();
        }

        [Fact]
        public void OnDialogOpenedTest()
        {
            DialogResult dialogResult = null;
            _vm.RequestClose += dr => dialogResult = dr as DialogResult;

            var p = new DialogParameters();
            p.Add(nameof(ViewReadModelViewModel.ReadModel), @"TestData\Hydrant.stl");
            _vm.OnDialogOpened(p);

            while (dialogResult == null)
            {
                Task.Delay(10);
            }

            Assert.Equal(ButtonResult.OK, dialogResult.Result);
        }
    }
}
