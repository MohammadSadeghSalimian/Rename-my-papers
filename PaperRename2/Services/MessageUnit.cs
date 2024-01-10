using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using MahApps.Metro.Controls.Dialogs;

namespace PaperRename2.Services
{
    public class MessageUnit : IMessageUnit
    {
        private readonly IDialogCoordinator _coordinator;
        private readonly ISubject<string> _newMessage;
        private object _parent;
        public MessageUnit(IDialogCoordinator message)
        {
            _newMessage = new Subject<string>();
            _coordinator = message;
        }
        public IObservable<string> NewMessage => _newMessage.AsObservable();
        public void SetParentObject(object parent)
        {
            _parent = parent;
        }
        public void ErrorMessage(string message)
        {
            ShowMessage(MessageType.Error, message);
        }
        public void ErrorMessage(Exception error)
        {
            ShowMessage(MessageType.Error, error.Message);
        }
        public void WarningMessage(string message)
        {
            ShowMessage(MessageType.Warning, message);
        }
        public void InformationMessage(string message)
        {
            ShowMessage(MessageType.Information, message);
        }
        public void WriteMessage(string str)
        {
            _newMessage.OnNext($"{DateTime.Now:T}: {str}{Environment.NewLine}");
        }
        private void ShowMessage(MessageType type, string message)
        {
            switch (type)
            {
                case MessageType.Error:
                    _coordinator.ShowMessageAsync(_parent, "Error!", message);
                    break;
                case MessageType.Warning:
                    _coordinator.ShowMessageAsync(_parent, "Warning!", message);
                    break;
                case MessageType.Information:
                    _coordinator.ShowMessageAsync(_parent, "Info", message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            _newMessage.OnNext(message);
        }
    }
}