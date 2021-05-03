using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArellanoUniversityRFIDGateMonitoringSystem
{
    public partial class FormSchedule : Form
    {
        string username,action;
        public FormSchedule(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            action = "edit";
            FormScheduleAddEdit fs = new FormScheduleAddEdit(username,action);
            fs.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            action = "add";
            FormScheduleAddEdit fs = new FormScheduleAddEdit(username, action);
            fs.ShowDialog();
        }
    }
}
