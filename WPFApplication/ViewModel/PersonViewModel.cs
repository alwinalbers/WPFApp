using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using WPFApplication.Models;
using System.Windows;

namespace WPFApplication.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {

        private static List<Person> personList = new List<Person>();
        private Person person = new Person();
        private readonly string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;";

        public PersonViewModel()
        {
            PopulatePersonList();
            person = personList[0];
        }

        #region Properties
        public int PersonId
        {
            get { return person.Id; }
            set
            {
                person.Id = value;
                OnPropertyChanged("PersonId");
            }
        }
        public int PersonGroup
        {
            get { return person.Group; }
            set
            {
                person.Group = value;
                OnPropertyChanged("PersonGroup");
            }
        }

        public string PersonFirstName
        {
            get { return person.FirstName; }
            set
            {
                person.FirstName = value;
                OnPropertyChanged("PersonFirstName");
            }
        }
        public string PersonLastName
        {
            get { return person.LastName; }
            set
            {
                person.LastName = value;
                OnPropertyChanged("PersonLastName");
            }
        }
        #endregion

        public void PopulatePersonList()
        {
            string queryString = "SELECT * FROM TestDB.dbo.Persons";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        
        private void AddPersonToList(IDataRecord record)
        {
            var person = new Person()
            {
                Id = record.GetInt32(0),
                FirstName = record.GetString(1),
                LastName = record.GetString(2),
                Group = record.GetInt32(3)
            };
            personList.Add(person);
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
