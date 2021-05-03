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
    public partial class FormLogin : Form
    {
        Connection login = new Connection();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //fetch data and comparing username and pass
                DataTable dt = login.GetData("SELECT * FROM tbloperator WHERE username = '" + txtuser.Text + "'" +
                    "AND password = '" + txtpass.Text + "'");
                //check if there is a record retrieved
                if (dt.Rows.Count > 0)
                {
                    string username = txtuser.Text;
                    MessageBox.Show("Correct Credentials...", "Loggin in", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormMain fm = new FormMain(username);
                    fm.ShowDialog();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong Credentials...", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
