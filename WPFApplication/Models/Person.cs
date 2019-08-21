using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Models
{
    public class Person 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Department { get; set; }
        public int Id { get; set; }

        public Person() { }
    }
}
