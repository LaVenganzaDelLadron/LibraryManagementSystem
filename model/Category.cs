using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    

        public Category(){ }

        public Category(string name)
        {
           Id = Guid.NewGuid();
           Name = name;
        }

    }
}
