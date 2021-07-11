using System;
using System.Windows.Input;

namespace ModbusGui.MvvmHelpers
{


    public class AppCommand<T> : ICommand
    {
        private readonly Action<T> _targetExecuteMethod;
        private readonly Func<T, bool> _targetCanExecuteMethod;

        public AppCommand(Action<T> executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }

        public AppCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_targetCanExecuteMethod != null)
            {
                return _targetCanExecuteMethod((T)parameter);
            }

            return _targetExecuteMethod != null;
        }

        // Beware - should use weak references if command instance lifetime 
        // is longer than lifetime of UI objects that get hooked up to command

        // Prism commands solve this in their implementation 
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            _targetExecuteMethod?.Invoke((T)parameter);
        }

    }
}