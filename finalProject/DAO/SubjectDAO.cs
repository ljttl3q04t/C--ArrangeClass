using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using finalProject.Entity;

namespace finalProject.DAO
{
    class SubjectDAO
    {
        public static List<Subject> selectSubject()
        {
            List<Subject> list = new List<Subject>();
            string sql = "SELECT * FROM Subject";
            DataTable dt = DataAccess.getData(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string id = dr["ID"].ToString();
                string name = dr["Name"].ToString();

                Subject nw = new Subject(id, name);
                list.Add(nw);
            }
            return list;
        }
    }
}
