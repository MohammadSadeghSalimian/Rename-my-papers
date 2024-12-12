using System;
using System.Threading.Tasks;

namespace PaperRename2.Wpf.Services
{
    public interface IMessageUnit
    {
        void SetParentObject(object mainVm);
        Task ErrorMessage(string message);
        Task ErrorMessage(Exception error);
        Task WarningMessage(string message);
        Task InformationMessage(string message);
    }
}