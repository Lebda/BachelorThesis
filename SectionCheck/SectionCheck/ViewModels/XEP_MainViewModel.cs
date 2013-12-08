using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Services;

namespace XEP_SectionCheck.ViewModels
{
    public class XEP_MainViewModel : ObservableObject
    {
        readonly IDialogService _dialogService;

        public XEP_MainViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }
    }
}
