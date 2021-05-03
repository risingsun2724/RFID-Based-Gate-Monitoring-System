using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication20;
using MySql.Data.MySqlClient;

namespace ArellanoUniversityRFIDGateMonitoringSystem
{
    public partial class FormMain : Form
    {
        Connection main = new Connection();
        string username;
        public FormMain(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            currenttime.Text = DateTime.Now.ToLongTimeString();
            currentdate.Text = DateTime.Now.ToLongDateString();
        }

        private void b2_Click(object sender, EventArgs e)
        {
            FormRFID rf = new FormRFID(username);
            rf.ShowDialog();
        }

        private void b4_Click(object sender, EventArgs e)
        {
            FormUsers fs = new FormUsers(username);
            fs.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormOperator fo = new FormOperator(username);
            fo.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormSchedule fs = new FormSchedule(username);
            fs.ShowDialog();
        }

        private void b1_Click(object sender, EventArgs e)
        {
            FormEntranceScan fes = new FormEntranceScan(username);
            fes.Show();
        }

        private void b3_Click(object sender, EventArgs e)
        {
            FormEntranceType fet = new FormEntranceType(username);
            fet.Show();
        }

        private void b5_Click(object sender, EventArgs e)
        {
            FormLogs fl = new FormLogs(username);
            fl.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormSMSBlaster fs = new FormSMSBlaster(username);
            fs.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
