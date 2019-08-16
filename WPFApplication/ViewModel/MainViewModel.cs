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
        private ICommand _nextButtonClick;
        private ICommand _previousButtonClick;

        private static List<Person> _personList = new List<Person>();
        private static List<Group> _groupList = new List<Group>();
        private Person _person = new Person();
        private Group _group = new Group();
        private readonly string _connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;";

        public MainViewModel()
        {
            PopulatePersonList();
            PopulateGroupList();
            _person = _personList[0];
            _group = _groupList[0];
        }
        
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

        public Group Group
        {
            get { return _group; }
            set
            {
                _group = value;
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

        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Filling Lists With Data
        private void PopulateGroupList()
        {
            string queryString = "SELECT * FROM TestDB.dbo.Groups";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AddGroupToList(reader);
                }
                reader.Close();
                connection.Close();
            }
        }

        private void PopulatePersonList()
        {
            string queryString = "SELECT * FROM TestDB.dbo.Persons";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AddPersonToList(reader);
                }
                reader.Close();
                connection.Close();
            }
        }

        private void AddGroupToList(IDataRecord record)
        {
            var group = new Group()
            {
                Id = record.GetInt32(0),
                Name = record.GetString(1)
            };
            _groupList.Add(group);
        }

        private void AddPersonToList(IDataRecord record)
        {
            var person = new Person()
            {
                Id = record.GetInt32(0),
                FirstName = record.GetString(1),
                LastName = record.GetString(2),
                Group = record.GetInt32(3)
            };
            _personList.Add(person);
        }

        private void EstablishPersonGroupRelation()
        {
            while (Person.Group != Group.Id)
            {
                Group = _groupList[Person.Group - 1];
            }
        }
        #endregion

        private void OnNextButtonClick()
        {
            if (_personList.Count == Person.Id)
            {
                Person = _personList[0];
            }
            else
            {
                Person = _personList[Person.Id];
            }
            EstablishPersonGroupRelation();
        }

        private void OnPreviousButtonClick()
        {
            if(Person.Id == 1)
            {
                Person = _personList[_personList.Count-1];
            }
            else
            {
                Person = _personList[Person.Id - 2];
            }
            EstablishPersonGroupRelation();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
