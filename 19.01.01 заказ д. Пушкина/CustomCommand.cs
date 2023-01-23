using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _19._01._01_заказ_д._Пушкина
{
    public class CustomCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        Action action;
        public CustomCommand(Action action)
        {
            this.action = action;
        }
        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            action();
        }
    }
}
