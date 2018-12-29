using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XepLich
{
    public class Schedule
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string Class { get; set; }
        public string Slot { get; set; }
        public string NumberStudent { get; set; }
        public Schedule()
        {

        }
        public Schedule(string courseID, string courseName, string classs, string slot, string numberStudent)
        {
            CourseID = courseID;
            CourseName = courseName;
            Class = classs;
            Slot = slot;
            NumberStudent = numberStudent;
        }
        public override string ToString()
        {
            return CourseID + " " + CourseName + " " + Class + " " + Slot + " " + NumberStudent;
        }
    }
}
