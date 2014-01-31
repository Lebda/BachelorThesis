using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

//Event Design: http://msdn.microsoft.com/en-us/library/ms229011.aspx

namespace XEP_CommonLibrary.Infrastructure
{
    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        protected bool SetMember<T>(ref T newValue, ref T member, bool areEqual, params string[] names4Raised)
        {
            if (areEqual) 
            { 
                return false; 
            }
            member = newValue;
            foreach (string item in names4Raised)
            {
                RaisePropertyChanged(item);
            }
            return true;
        }

        protected bool SetMember<T>(ref T newValue, ref T member, bool areEqual, Action fce, params string[] names4Raised)
        {
            if (areEqual)
            {
                return false;
            }
            fce();
            member = newValue;
            foreach (string item in names4Raised)
            {
                RaisePropertyChanged(item);
            }
            return true;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpresssion);
            this.RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(String propertyName)
        {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Warns the developer if this Object does not have a public property with
        /// the specified name. This method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(String propertyName)
        {
            // verify that the property name matches a real,  
            // public, instance property on this Object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                Debug.Fail("Invalid property name: " + propertyName);
            }
        }
    }
}
