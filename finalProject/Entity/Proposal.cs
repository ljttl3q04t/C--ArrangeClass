using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace finalProject.Entity
{
    class Proposal
    {
        public string Subject { get; set; }
        public List<string> Lich { get; set; }
        public Proposal()
        {

        }
        public Proposal(string subject, List<string> lich)
        {
            Subject = subject;
            Lich = lich;
        }
        public override string ToString()
        {
            string res = Subject;
            string tg = "";
            foreach(string i in Lich)
            {
                tg = tg + i + ", ";
            }
            res = res + "    " + tg;
            return res;
        }
    }
}
