using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using finalProject.DAO;
using finalProject.Entity;
namespace finalProject
{
    public partial class Form1 : Form
    {
        List<Proposal> myProposal;
        List<Schedule> listLich;
        Schedule[] kq;
        bool ok;
        public Form1()
        {
            InitializeComponent();
            myProposal = new List<Proposal>();
            listLich = new List<Schedule>();
            List<Subject> list = SubjectDAO.selectSubject();
            comboBoxSubject.DisplayMember = "Name";
            comboBoxSubject.ValueMember = "Id";
            comboBoxSubject.DataSource = list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("SubjectID", "Subject ID");
            dataGridView1.Columns["SubjectID"].DataPropertyName = "SubjectID";

            dataGridView1.Columns.Add("Subject", "Subject Name");
            dataGridView1.Columns["Subject"].DataPropertyName = "Subject";

            dataGridView1.Columns.Add("ChuoiLich", "potential schedule");
            dataGridView1.Columns["ChuoiLich"].DataPropertyName = "ChuoiLich";

            DataGridViewButtonColumn delCol = new DataGridViewButtonColumn();
            delCol.Name = "Delete";
            delCol.Text = "Delete";
            delCol.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(delCol);
        }

        private void comboBoxSubject_SelectedIndexChanged(object sender, EventArgs e) // add mon
        {
            String s = comboBoxSubject.SelectedValue.ToString();
        }
        private void buttonAddSubject_Click(object sender, EventArgs e)
        {
            Subject l = comboBoxSubject.SelectedItem as Subject;
            string subject = l.Name;
            string subjectId = l.ID;
            List<string> lich = new List<string>();
            foreach(object i in listSchedule.CheckedItems)
            {
                lich.Add(i.ToString());
            }
            Proposal p = new Proposal(subjectId, subject, lich);
            myProposal.Add(p);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = myProposal;
        }
        private void checkKq()
        {
            string[] ca = new string[myProposal.Count];
            string res = "";
            for (int i = 0; i < myProposal.Count; i++)
            {
                ca[i] = kq[i].Lich;
                res = res + kq[i].Subject + " " + kq[i].Class + " " + kq[i].Lich + "\n";
            }
            for (int i = 0; i < myProposal.Count; i++) 
                for (int j = i; j < myProposal.Count; j++)
                    if (i != j)
                    {
                        if (ca[i].Equals(ca[j])) return;
                    }
            test.Text = res;
            ok = true;
        }
        private bool hop(int i, Schedule j)
        {
            Proposal p = myProposal.ElementAt(i);
            //MessageBox.Show(p.ToString() + "\n" + j.Lich + " " + j.Subject);
            if (p.Subject.Trim().Equals(j.Subject.Trim()))
            {
                foreach (string k in p.Lich)
                {
                    if (k.Equals(j.Lich))
                    {
                        //MessageBox.Show(p.ToString() + "\n" + j.Lich + " " + j.Subject);
                        return true;
                    }
                }
            }
            return false;
        }
        private void duyet(int i)
        {
            if (ok) return;
            if (i == myProposal.Count)
            {
                //test3();
                checkKq();
                return;
            }
            foreach(Schedule j in listLich)
            {
                if (hop(i, j))
                {
                    //MessageBox.Show(j.Subject + " " + j.Lich);
                    kq[i] = j;
                    duyet(i + 1);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //test1();
            listLich = ScheduleDAO.selectSchedule();
            //test2();
            kq = new Schedule[myProposal.Count];
            ok = false;
            duyet(0);
        }
        void test2()
        {
            string res = "";
            foreach(Schedule s in listLich)
            {
                res = res + s.ToString() + "\n";
            }
            MessageBox.Show(res);
        }
        void test1()
        {
            string res = "";
            foreach (Proposal p in myProposal)
            {
                res = res + p.ToString() + "\n";
            }
            MessageBox.Show(res);
        }
        void test3()
        {
            string res = "";
            for (int i = 0; i < myProposal.Count; i++)
                res = res + kq[i].Lich + "\n";
            MessageBox.Show(res);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                List<Proposal> list = (List<Proposal>)dataGridView1.DataSource;
                int index = e.RowIndex;
                DialogResult res = MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    myProposal.RemoveAt(index);
                    test.Text = "" + myProposal.Count;
                    MessageBox.Show("Delete Successful!");
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = myProposal;
                }
            }
        }
    }
}
