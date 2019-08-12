using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Models
{
    public class Person
    {
        public Person() : this("Max","Mustermann", 19) { }
        public Person(string fNameIn, string lNameIn, int capitalIn)
        {
            Capital = capitalIn;
            FirstName = fNameIn;
            LastName = lNameIn;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Capital { get; set; }

    }
}
