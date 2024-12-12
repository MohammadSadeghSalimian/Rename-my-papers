using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace PaperRename2.Wpf.Services
{
    public class MessageUnit(IDialogCoordinator coordinator) : IMessageUnit
    {
        private readonly Subject<string> _newMessage = new();
        private object _parent;
        public IObservable<string> NewMessage => _newMessage.AsObservable();
        public void SetParentObject(object parent)
        {
            _parent = parent;
        }
        public async Task ErrorMessage(string message)
        {
            await ShowMessage(MessageType.Error, message);
        }
        public async Task ErrorMessage(Exception error)
        {
            await ShowMessage(MessageType.Error, error.Message);
        }
        public async Task WarningMessage(string message)
        {
            await ShowMessage(MessageType.Warning, message);
        }
        public async Task InformationMessage(string message)
        {
           await ShowMessage(MessageType.Information, message);
        }
        public void WriteMessage(string str)
        {
            _newMessage.OnNext($"{DateTime.Now:T}: {str}{Environment.NewLine}");
        }
        private async Task ShowMessage(MessageType type, string message)
        {
            switch (type)
            {
                case MessageType.Error:
                  await  coordinator.ShowMessageAsync(_parent, "Error!", message);
                    break;
                case MessageType.Warning:
                   await coordinator.ShowMessageAsync(_parent, "Warning!", message);
                    break;
                case MessageType.Information:
                   await coordinator.ShowMessageAsync(_parent, "Info", message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            _newMessage.OnNext(message);
        }
    }
}