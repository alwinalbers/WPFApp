using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using WPFApplication.Models;

namespace WPFApplication.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {

        private static List<Person> personList = new List<Person>();
        private Person _person = new Person();
        private readonly string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;";

        public PersonViewModel()
        {
            PopulatePersonList();
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
        public int PersonGroup

        {
            get { return _person.Group; }
            set
            {
                _person.Group = value;
                OnPropertyChanged("PersonGroup");
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
                    ReadSingleRow((IDataRecord)reader);
                }
                reader.Close();
                connection.Close();
            }
        }

        private static void ReadSingleRow(IDataRecord record)
        {
            var fieldcount = record.FieldCount;
            for(int i = 0; i == fieldcount - 1; i++)
            {
                Person x = new Person();
                string ja = record.GetDataTypeName(0);
                Console.WriteLine(ja);
            }
        }

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
