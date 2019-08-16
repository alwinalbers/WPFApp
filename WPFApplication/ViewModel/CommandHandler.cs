﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApplication.ViewModel
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;
        private Action onNextButtonClick;
        private Action onPreviousButtonClick;
        private bool canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        //public CommandHandler(Action onNextButtonClick, bool canExecute)
        //{
        //    this.onNextButtonClick = onNextButtonClick;
        //    this.canExecute = canExecute;
        //}
        //
        //public CommandHandler(Action onPreviousButtonClick, bool canExecute)
        //{
        //    this.onPreviousButtonClick = onPreviousButtonClick;
        //    this.canExecute = canExecute;
        //}

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
