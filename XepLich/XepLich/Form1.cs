using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XepLich
{
    public partial class Form1 : Form
    {
        public List<Course> courses;
        public List<Schedule> schedules, yourSchedules, validSchedule, proposals;
        public Schedule[] kq;
        public List<List<Schedule>> result;
        public bool canAssign;

        public Form1()
        {
            InitializeComponent();
            init();
            
        }
        void init()
        {
            courses = Data.getAllCourse();
            List<string> tg = new List<string>();
            foreach (Course x in courses)
            {
                tg.Add(x.ToString());
            }
            courseComboBox.DataSource = tg;
            schedules = Data.getAllSchedule(courses);
            yourSchedules = Data.getYourCurrentSchedule(courses);
            //string s = "";

            foreach (Schedule x in yourSchedules)
            {
                //s = s + getIndexSlot(x.Slot) + "\n";
                checkedListBox1.SetItemChecked(getIndexSlot(x.Slot), true);
            }
            proposals = yourSchedules;
            //test.Text = s;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("CourseID", "Course ID");
            dataGridView1.Columns["CourseID"].DataPropertyName = "CourseID";

            dataGridView1.Columns.Add("CourseName", "Course Name");
            dataGridView1.Columns["CourseName"].DataPropertyName = "CourseName";

            dataGridView1.Columns.Add("Class", "Class");
            dataGridView1.Columns["Class"].DataPropertyName = "Class";

            dataGridView1.Columns.Add("Slot", "Slot");
            dataGridView1.Columns["Slot"].DataPropertyName = "Slot";

            DataGridViewButtonColumn delCol = new DataGridViewButtonColumn();
            delCol.Name = "Delete";
            delCol.Text = "Delete";
            delCol.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(delCol);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = proposals;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string c = courseComboBox.SelectedItem.ToString();
            int index = c.IndexOf('-');
            string courseName = c.Substring(index + 1);
            string courseID = c.Substring(0, index);
            proposals.Add(new Schedule(courseID, courseName, "", "", ""));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = proposals;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                List<Schedule> list = (List<Schedule>)dataGridView1.DataSource;
                int index = e.RowIndex;
                proposals.RemoveAt(index);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = proposals;
            }
        }
        public int  getIndexSlot(string slot)
        {
            if (slot.Equals("M1")) return 0;
            if (slot.Equals("M2")) return 1;
            if (slot.Equals("M3")) return 2;
            if (slot.Equals("M4")) return 3;
            if (slot.Equals("M5")) return 4;
            if (slot.Equals("E1")) return 5;
            if (slot.Equals("E2")) return 6;
            if (slot.Equals("E3")) return 7;
            if (slot.Equals("E4")) return 8;
            if (slot.Equals("E5")) return 9;
            return 0;
        }
        //********
        //*********** most important
        private void makeButton_Click(object sender, EventArgs e)
        {
            List<string> reset = new List<string>();
            comboBox2.DataSource = reset;
            kq = new Schedule[proposals.Count];
            validSchedule = getValidSchedule();
            result = new List<List<Schedule>>();
            canAssign = false;
            //test.Text = test.Text + "\n" + validSchedule.Count;
            //test.Text = test.Text + "\n" + proposals.Count;
            duyet(0);
            if (canAssign)
            {
                test.Text = "Xếp được";
                List<string> cach = new List<string>();
                for (int i = 0; i < result.Count; i++)
                    cach.Add("Cách " + i);
                comboBox2.DataSource = cach;
            } else
            {
               test.Text = test.Text + "\n" + "Chịu! Không xếp được lịch nào phù hợp.";
            }
        }
        //*****
        //********
        //********
        public void duyet(int i)
        {
            if (i == proposals.Count)
            {
                checkKq();
                return;
            }
            foreach(Schedule s in validSchedule)
            {
                if(hop(s, i))
                {
                    kq[i] = s;
                    duyet(i + 1);
                }
            }
        }

        public bool hop(Schedule s, int i)
        {
            Schedule p = proposals.ElementAt(i);
            if (p.CourseID.Trim().Equals(s.CourseID.Trim())) return true;
            return false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            List<Schedule> myProposal = result.ElementAt(index);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = myProposal;
        }

        public void checkKq()
        {
            for (int i = 0; i < proposals.Count; i++)
                for (int j = 0; j < proposals.Count; j++) if (i != j)
                    {
                        if (kq[i].Slot.Equals(kq[j].Slot)) return;
                    }
            List<Schedule> res = new List<Schedule>();
            for (int i = 0; i < proposals.Count; i++) res.Add(kq[i]);
            result.Add(res);
            canAssign = true;
        }

        public List<Schedule> getValidSchedule()
        {
            string ss = "";
            List<string> desireSchedule = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                desireSchedule.Add("" + checkedListBox1.Items.IndexOf(itemChecked));
                //ss = ss + checkedListBox1.Items.IndexOf(itemChecked) + "\n";
            }
            List<Schedule> list = new List<Schedule>();
           
            foreach(Schedule s in schedules)
            {
                bool ok = false;
                foreach(Schedule p in proposals)
                {
                    if (p.CourseID.Trim().Equals(s.CourseID.Trim()))
                    {
                        ok = true;
                        break;
                    }
                }
                if (!ok) continue;
                if (Convert.ToInt32(s.NumberStudent) >= 30)
                {
                    //ss = ss + " qua si so " + s.ToString() + "\n";
                    continue;
                }
                if (!checkValidSlot(s.Slot, desireSchedule))
                {
                    //ss = ss + " khac slot " + s.ToString() + "\n";
                    continue;
                }
                ss = ss  + s.ToString() + "\n";
                list.Add(s);
            }
            foreach (Schedule s in yourSchedules)
            {
                bool ok = true;
                foreach(Schedule x in list)
                {
                    if (x.CourseID.Equals(s.CourseID) && x.CourseName.Equals(s.CourseName))
                        if (x.Slot.Equals(s.Slot))
                        {
                            ok = false;
                            break;
                        }
                }
                if (!ok) continue;
                if (!checkValidSlot(s.Slot, desireSchedule))
                {
                    //ss = ss + " khac slot " + s.ToString() + "\n";
                    continue;
                }
                ss = ss + s.ToString() + "\n";
                list.Add(s);
            }
            test.Text = ss;
            return list;
        }
        public bool checkValidSlot(string slot, List<string> list)
        {
            slot = "" + getIndexSlot(slot);
            foreach(string s in list)
            {
                if (s.Equals(slot)) return true;
            }
            return false;
        }
    }
}
