using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace finalProject.Entity
{
    class Subject
    { 
        public string ID { get; set; }
        public string Name { get; set; }
        public Subject()
        {

        }
        public Subject(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
