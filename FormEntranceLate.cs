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

namespace ArellanoUniversityRFIDGateMonitoringSystem
{
    public partial class FormEntranceLate : Form
    {
        Connection conn = new Connection();
        string username,rfid;
        public FormEntranceLate(string rfid, string username)
        {
            InitializeComponent();
            this.rfid = rfid;
            this.username = username;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void FormEntranceLate_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.GetData("SELECT * FROM tblstudents WHERE rfid = '" + rfid + "'");
                if(conn.rowAffected > 0)
                {

                }


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
