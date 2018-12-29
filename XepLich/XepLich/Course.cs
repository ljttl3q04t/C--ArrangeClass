using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XepLich
{
    public class Course
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Course()
        {

        }
        public Course(string id, string name)
        {
            ID = id;
            Name = name;
        }
        public override string ToString()
        {
            return ID + "-" + Name;
        }
    }
}
