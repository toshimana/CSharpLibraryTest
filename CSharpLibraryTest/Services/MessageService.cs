using System.Windows;

namespace CSharpLibraryTest.Services
{
    internal sealed class MessageService : IMessageService
    {
        public MessageBoxResult Question(string message)
        {
            return MessageBox.Show(message, "確認", MessageBoxButton.OK);
        }

        public void ShowDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
