using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using WPFApplication.Models;
using System.Windows;
using System.Windows.Input;

namespace WPFApplication.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region fields
        private ICommand _nextButtonClick;
        private ICommand _previousButtonClick;
        private ICommand _saveButtonClick;
        private DbManager _dbManager;
        #endregion

        #region ctor
        public MainViewModel()
        {
            _dbManager = new DbManager();
        }
        #endregion

        #region Properties
        public Person Person
        {
            get { return _dbManager.Person; }
            set
            {
                _dbManager.Person = value;
                OnPropertyChanged();
            }
        }

        public Department Department
        {
            get { return _dbManager.Department; }
            set
            {
                _dbManager.Department = value;
                OnPropertyChanged();
            }
        }

        public ICommand NextButtonClick
        {
            get
            {
                return _nextButtonClick ?? (_nextButtonClick = new CommandHandler(() => OnNextButtonClick(), () => CanExecute));
            }
        }

        public ICommand PreviousButtonClick
        {
            get
            {
                return _previousButtonClick ?? (_previousButtonClick = new CommandHandler(() => OnPreviousButtonClick(), () => CanExecute));
            }
        }

        public ICommand SaveButtonClick
        {
            get
            {
                return _saveButtonClick ?? (_saveButtonClick = new CommandHandler(() => OnSaveButtonClick(), () => CanExecute));
            }
        }

        public bool CanExecute
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region ButtonClicks

        private void OnNextButtonClick()
        {
            Person = _dbManager.LoadNext();
            Department = _dbManager.LoadDepartment();
        }

        private void OnPreviousButtonClick()
        {
            Person = _dbManager.LoadPrevious();
            Department = _dbManager.LoadDepartment();
        }

        private void OnSaveButtonClick()
        {
            _dbManager.UpdatePersons(Person);
            _dbManager.UpdateDepartments(Department);
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
