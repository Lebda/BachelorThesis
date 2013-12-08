using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Services;

namespace SectionCheck.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }
    }
}
