using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFApplication.Models;

namespace WPFApplication.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region fields
        private ICommand _nextButtonClick;
        private ICommand _previousButtonClick;
        private ICommand _saveButtonClick;
        private DbManager _dbManager;

        private Person _person;
        private Department _department;
        #endregion

        #region ctor
        public MainViewModel()
        {
            _dbManager = new DbManager();
            _person = _dbManager.InitialPerson();
            _department = _dbManager.LoadDepartment(Person.Department);
        }
        #endregion

        #region Properties
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public Department Department
        {
            get { return _department; }
            set
            {
                _department = value;
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
            Person = _dbManager.LoadNext(Person.Id);
            Department = _dbManager.LoadDepartment(Person.Department);
        }

        private void OnPreviousButtonClick()
        {
            Person = _dbManager.LoadPrevious(Person.Id);
            Department = _dbManager.LoadDepartment(Person.Department);
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
