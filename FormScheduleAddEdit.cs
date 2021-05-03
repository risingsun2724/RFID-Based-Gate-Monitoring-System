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
    public partial class FormScheduleAddEdit : Form
    {
        string username, action;
        public FormScheduleAddEdit(string username, string action)
        {
            InitializeComponent();
            this.username = username;
            this.action = action;

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void FormScheduleAddEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
