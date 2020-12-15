using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class RenavigateCommand : ICommand
    {
        private readonly IRenavigator renavigator;

        public RenavigateCommand(IRenavigator renavigator)
        {
            this.renavigator = renavigator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            renavigator.Renavigate();
        }
    }
}
