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
        public MainViewModel()
        {
            PopulatePersonList();
            PopulateDepartmentList();
            _person = _personList[0];
            _department = _departmentList[0];
        }

        #region fields
        private ICommand _nextButtonClick;
        private ICommand _previousButtonClick;
        private ICommand _saveButtonClick;

        private static List<Person> _personList = new List<Person>();
        private static List<Department> _departmentList = new List<Department>();
        private Person _person = new Person();
        private Department _department = new Department();
        private readonly string _connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=testDB;";
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

        #region Filling Lists With Data
        private void PopulateDepartmentList()
        {
            string queryString = "SELECT * FROM TestDB.dbo.Departments";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AddDepartmentToList(reader);
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

        private void AddDepartmentToList(IDataRecord record)
        {
            var Department = new Department()
            {
                Id = record.GetInt32(0),
                Name = record.GetString(1)
            };
            _departmentList.Add(Department);
        }

        private void AddPersonToList(IDataRecord record)
        {
            var person = new Person()
            {
                Id = record.GetInt32(0),
                FirstName = record.GetString(1),
                LastName = record.GetString(2),
                Department = record.GetInt32(3)
            };
            _personList.Add(person);
        }

        private void EstablishPersonDepartmentRelation()
        {
            while (Person.Department != Department.Id)
            {
                Department = _departmentList[Person.Department - 1];
            }
        }
        #endregion

        #region ButtonClicks
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
            EstablishPersonDepartmentRelation();
        }

        private void OnPreviousButtonClick()
        {
            if (Person.Id == 1)
            {
                Person = _personList[_personList.Count - 1];
            }
            else
            {
                Person = _personList[Person.Id - 2];
            }
            EstablishPersonDepartmentRelation();
        }

        private void OnSaveButtonClick()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE dbo.Persons SET FirstName = @FName , LastName = @LName , Department = @DepartmentId WHERE Id = @Id";
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters.Add("@FName", SqlDbType.VarChar);
                    command.Parameters.Add("@LName", SqlDbType.VarChar);
                    command.Parameters.Add("@DepartmentId", SqlDbType.Int);
                    command.Parameters["@Id"].Value = Person.Id;
                    command.Parameters["@FName"].Value = Person.FirstName;
                    command.Parameters["@LName"].Value = Person.LastName;
                    command.Parameters["@DepartmentId"].Value = Person.Department;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex);
                    }
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
