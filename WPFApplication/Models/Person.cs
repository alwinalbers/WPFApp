using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Models
{
    public class Person
    {
        public Person() : this(0,"Max","Mustermann", 19) { }
        public Person(int idIn, string fNameIn, string lNameIn, int capitalIn)
        {
            Capital = capitalIn;
            FirstName = fNameIn;
            LastName = lNameIn;
            Id = idIn;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Capital { get; set; }
        public int Id { get; set; }
    }
}
