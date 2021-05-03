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
    public partial class FormSMSBlaster : Form
    {
        string username;
        public FormSMSBlaster(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSMSBlaster_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
