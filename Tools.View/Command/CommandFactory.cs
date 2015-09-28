using System;
using System.Windows.Input;
using PeletonSoft.Tools.Model.Dependency;

namespace PeletonSoft.Tools.View.Command
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(Action<object> execute)
        {
            return new RelayCommand(execute);
        }

        public ICommand CreateCommand<T>(Action<T> execute)
        {
            return CreateCommand(obj => execute((T)obj));
        }

        public ICommand CreateCommand(Action execute)
        {
            return new RelayCommand(o => execute());
        }

        public ICommand CreateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            return new RelayCommand(execute,canExecute);
        }


        public ICommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new RelayCommand(o => execute(), o => canExecute());
        }
    }
}
