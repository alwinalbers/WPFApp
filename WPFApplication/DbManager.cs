using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFApplication.Models;

namespace WPFApplication
{
    class DbManager
    {

        #region fields
        private readonly string _connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=testDB;";
        private static List<Person> _personList;
        private static List<Department> _departmentList;
        #endregion

        #region ctor
        public DbManager()
        {
            _personList = new List<Person>();
            _departmentList = new List<Department>();
            PopulatePersonList();
            PopulateDepartmentList();
        }
        #endregion

        #region Populating Lists
        private void PopulateDepartmentList()
        {
            string queryString = "SELECT * FROM Departments";
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
                command.Dispose();
                connection.Close();
            }
        }

        private void PopulatePersonList()
        {
            string queryString = "SELECT * FROM Persons";
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
                command.Dispose();
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
        #endregion

        public Person InitialPerson()
        {
            return _personList[0];
        }

        public Person LoadNext(int personId)
        {
            Person person;
            if (_personList.Count == personId)
            {
                person = _personList[0];
            }
            else
            {
                person = _personList[personId];
            }
            return person;
        }

        public Person LoadPrevious(int personId)
        {
            Person person;
            int previousId = personId - 2;
            int listCount = _personList.Count - 1;

            if (personId == 1)
            {       
                person = _personList[listCount];
            }
            else
            {
                person = _personList[previousId];
            }
            return person;
        }

        public Department LoadDepartment(int personDepartment)
        {
            int departmentId = --personDepartment;
            return _departmentList[departmentId];
        }

        public void UpdateDepartments(Department department)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Departments SET Name = @DName WHERE Id = @Id";
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters.Add("@DName", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = department.Id;
                    command.Parameters["@DName"].Value = department.Name;

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

        public void UpdatePersons(Person person)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Persons SET FirstName = @FName , LastName = @LName , Department = @DepartmentId WHERE Id = @Id";
                conn.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters.Add("@FName", SqlDbType.VarChar);
                    command.Parameters.Add("@LName", SqlDbType.VarChar);
                    command.Parameters.Add("@DepartmentId", SqlDbType.Int);
                    command.Parameters["@Id"].Value = person.Id;
                    command.Parameters["@FName"].Value = person.FirstName;
                    command.Parameters["@LName"].Value = person.LastName;
                    command.Parameters["@DepartmentId"].Value = person.Department;

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
        
    }
}
