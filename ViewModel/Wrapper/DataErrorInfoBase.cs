using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ViewModel.ViewModels;

namespace ViewModel.Wrapper
{
        public class DataErrorInfoBase : VievModelBase, INotifyDataErrorInfo
        {

            private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

            public bool HasErrors => _errors.Any();

            public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

            public IEnumerable GetErrors(string propertyName)
            {
                return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
            }

            protected virtual void OnError(string propertyName)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                base.OnPropertyChanged(nameof(HasErrors));
            }

            protected void AddError(string propertyName, string error)
            {
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors[propertyName] = new List<String>();
                }
                if (!_errors[propertyName].Contains(error))
                {
                    _errors[propertyName].Add(error);
                    OnError(propertyName);
                }
            }

            protected void ClearErrors(string propertyName)
            {
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                    OnError(propertyName);
                }
            }
        }
    }
