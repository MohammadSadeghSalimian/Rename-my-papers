﻿using System;

namespace PaperRename2.Services
{
    public interface IMessageUnit
    {
        void SetParentObject(object mainVm);
        void ErrorMessage(string message);
        void ErrorMessage(Exception error);
        void WarningMessage(string message);
        void InformationMessage(string message);
    }
}