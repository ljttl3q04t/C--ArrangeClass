using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XepLich
{
    public class Data
    {
        public static List<string> ReadDataFromFile(String fileName)
        {
            List<string> list = new List<string>();
            try
            {
                StreamReader reader = new StreamReader(fileName);
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR at read data!\n" + ex.Message);
            }
            return list;
        }

        public static List<Schedule> getAllSchedule(List<Course> courses)
        {
            string fileName = "../../../../megafyk/Record.txt";
            List<string> alu = ReadDataFromFile(fileName);
            List<Schedule> list = new List<Schedule>();
            foreach (string x in alu)
            {
                if (x.Length < 1) continue;
                string[] c = x.Split('|');
                string courseName = "";
                foreach (Course co in courses)
                {
                    if (c[0].Equals(co.ID))
                    {
                        courseName = co.Name;
                        break;
                    }
                }
                list.Add(new Schedule(c[0], courseName, c[1], c[2], c[3]));
            }
            return list;
        }
        public static List<Course> getAllCourse()
        {
            string fileName = "../../../../megafyk/SubjectCode.txt";
            List<string> alu = ReadDataFromFile(fileName);
            List<Course> list = new List<Course>();
            int index = 0;
            string id = "";
            string name = "";
            foreach (string x in alu)
            {
               
                if (x.Length < 1) continue;
                if (index % 2 == 0) id = x;
                else
                {
                    name = x;
                    list.Add(new Course(id, name));
                }
                index++;
            }
            return list;
        }
        public static List<Schedule> getYourCurrentSchedule(List<Course> courses)
        {
            string fileName = "../../../../megafyk/YourSchedule.txt";
            List<string> alu = ReadDataFromFile(fileName);
            List<Schedule> list = new List<Schedule>();
            foreach (string x in alu)
            {
                if (x.Length < 1) continue;
                string[] c = x.Split('|');
                string courseName = "";
                foreach (Course co in courses)
                {
                    if (c[0].Equals(co.ID))
                    {
                        courseName = co.Name;
                        break;
                    }
                }
                list.Add(new Schedule(c[0], courseName, c[1], c[2], ""));
            }
            return list;
        }
    }
}
