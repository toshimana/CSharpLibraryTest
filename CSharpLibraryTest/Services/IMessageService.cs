using System.Windows;

namespace CSharpLibraryTest.Services
{
    public interface IMessageService
    {
        void ShowDialog(string message);
        MessageBoxResult Question(string message);
    }
}
