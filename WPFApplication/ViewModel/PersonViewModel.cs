using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFApplication.Models;
using System.Data.SqlClient;

namespace WPFApplication.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private Person _person;
        private readonly string connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Integrated Security=true;";

        public PersonViewModel()
        {
            _person = new Person();
        }

        public int PersonId
        {
            get { return _person.Id; }
            set
            {
                _person.Id = value;
                OnPropertyChanged("PersonId");
            }
        }
        public int PersonCapital
        {
            get { return _person.Capital; }
            set
            {
                _person.Capital = value;
                OnPropertyChanged("PersonCapital");
            }
        }

        public string PersonFirstName
        {
            get { return _person.FirstName; }
            set
            {
                _person.FirstName = value;
                OnPropertyChanged("PersonFirstName");
            }
        }
        public string PersonLastName
        {
            get { return _person.LastName; }
            set
            {
                _person.LastName = value;
                OnPropertyChanged("PersonLastName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }
}
