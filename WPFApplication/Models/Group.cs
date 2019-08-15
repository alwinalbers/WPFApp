using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group() {}
        public Group(int idIn, string nameIn)
        {
            Id = idIn;
            Name = nameIn;
        }
    }
}
