using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace finalProject.Entity
{
    class Schedule
    {
        public string Subject { get; set; }
        public string Class { get; set; }
        public string Lich { get; set; }
        public string Amount { get; set; }
        public Schedule()
        {

        }
        public Schedule(string subject, string lop, string lich, string amount)
        {
            Subject = subject;
            Class = lop;
            Lich = lich;
            Amount = amount;
        }
        public override string ToString()
        {
            return Subject + "," + Class + "," + Lich;
        }
    }
}
