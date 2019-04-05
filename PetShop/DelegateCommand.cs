using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop {
    public class DelegateCommand : ICommand {

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public DelegateCommand(Action<object> exe, Predicate<object> canExe) {
            if(exe == null) { throw new ArgumentNullException("excuse"); }
            _execute = exe;
            _canExecute = canExe;
        }

        public DelegateCommand(Action<object> exe) : this(exe, null) {}

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return (_canExecute == null) ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }
    }
}
