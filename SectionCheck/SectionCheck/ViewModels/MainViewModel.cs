using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary;
using CommonLibrary.Infrastructure;
using CommonLibrary.Services;

namespace SectionCheck.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }

        /// <summary>
        /// The <see cref="MyProperty" /> property's name.
        /// </summary>
        public const string MyPropertyPropertyName = "MyProperty";

        private bool _myProperty = false;

        /// <summary>
        /// Sets and gets the MyProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool MyProperty
        {
            get
            {
                return _myProperty;
            }

            set
            {
                if (_myProperty == value)
                {
                    return;
                }

               // RaisePropertyChanging(MyPropertyPropertyName);
                _myProperty = value;
                RaisePropertyChanged(MyPropertyPropertyName);
            }
        }
    }
}
