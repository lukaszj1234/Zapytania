using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace ViewModel.Wrapper
{
    public class ModelWrapper<T>: DataErrorInfoBase
    {
        public ModelWrapper(T model)
        {
            Model = model;
        }
        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }
        protected virtual void SetValue<TValue>(TValue value,
            [CallerMemberName]string propertyName = null)
        {
           typeof(T).GetProperty(propertyName).SetValue(Model,value);
           OnPropertyChanged(propertyName);
           ValidateProperty(propertyName);
        }

        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            var errors = Validate(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        protected  virtual IEnumerable<string> Validate(string propertyName)
        {
            return null;
        }

        public T Model { get; }
    }
}
