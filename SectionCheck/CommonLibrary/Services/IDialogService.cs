using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Services
{
    public interface IDialogService
    {
        DialogResponse ShowException(String message, DialogImage image = DialogImage.Error);
        DialogResponse ShowMessage(String message, String caption, DialogButton button, DialogImage image);
    }
}
