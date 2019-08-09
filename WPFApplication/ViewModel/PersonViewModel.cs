using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFApplication.Models;

namespace WPFApplication.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private Person _person;

        public PersonViewModel()
        {
            _person = new Person { Age = 19 ,  Name = "Max Mustermann" };
        }

        public int PersonAge
        {
            get { return _person.Age; }
            set
            {
                _person.Age = value;
                OnPropertyChanged("PersonBirthday");
            }
        }

        public string PersonName
        {
            get { return _person.Name; }

            set
            {
                _person.Name = value;
                OnPropertyChanged("PersonName");
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
