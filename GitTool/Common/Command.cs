using System;
using System.Windows.Input;

namespace GitTool.Common
{
    public class Command : ICommand
    {
        readonly Action execute;
        readonly Func<bool> canExecute;

        public Command(Action action, Func<bool> canExecute = null)
        {
            execute = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object? parameter)
        {
            execute?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
