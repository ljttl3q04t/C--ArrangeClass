using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using finalProject.Entity;
namespace finalProject.DAO
{
    class ScheduleDAO
    {
        public static List<Schedule> selectSchedule()
        {
            List<Schedule> list = new List<Schedule>();
            string sql = "SELECT * FROM Schedule";
            DataTable dt = DataAccess.getData(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string lich = dr["Lich"].ToString();
                string lop = dr["Class"].ToString();
                string subject = dr["Subject"].ToString();
                string amount = dr["Amount"].ToString();

                Schedule nw = new Schedule(subject, lop, lich, amount);
                list.Add(nw);
            }
            return list;
        }
        public static List<Schedule> GetSchedules()
        {
            List<Schedule> list = new List<Schedule>;
            return list;
        }
    }
}
