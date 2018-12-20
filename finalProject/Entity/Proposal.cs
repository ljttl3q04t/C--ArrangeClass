using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace finalProject.Entity
{
    class Proposal
    {
        public string Subject { get; set; }
        public string SubjectID { get; set; }
        public List<string> Lich { get; set; }
        public string ChuoiLich
        {
            get
            {
                if (Lich.Count == 0) return "";
                string res = "";
                for (int i = 0; i < Lich.Count - 1; i++)
                {
                    string s = Lich.ElementAt(i);
                    res = res + s + ", ";
                }
                res = res + Lich.ElementAt(Lich.Count - 1);
                return res;
            }
        }
        public Proposal()
        {

        }
        public Proposal(string subjectId, string subject, List<string> lich)
        {
            SubjectID = subjectId;
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
