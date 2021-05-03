using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication20;

namespace ArellanoUniversityRFIDGateMonitoringSystem
{
    public partial class FormUsers : Form
    {
        Connection conn = new Connection();
        string username, action,edit_user;
        public FormUsers(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //close FormStudents
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
            action = "edit";
            edit_user = dataGridView1.Rows[rowSelected].Cells["rfid"].Value.ToString();
            FormUsersAddEdit fae = new FormUsersAddEdit(username, action,edit_user);
            fae.ShowDialog();
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Getting Credntials for EDITTING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
}

        internal void FormStudents_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.GetData("SELECT rfid,idnumber,lastname,firstname,middlename,timeregistered,dateregistered,registeredby FROM tblstudents");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Equipments Load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selected_rfid = dataGridView1.Rows[rowSelected].Cells["rfid"].Value.ToString();
                string selected_user = dataGridView1.Rows[rowSelected].Cells["idnumber"].Value.ToString();
                DialogResult dr = MessageBox.Show("Are you sure you want to DELETE this OPERATOR with an RFID : " + selected_user + " ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    conn.executeSQL("DELETE FROM tblstudents WHERE rfid = '" + selected_user + "'");
                    if (conn.rowAffected > 0)
                    {
                        MessageBox.Show("DELETED an RFID USER with an ID NUMBER of " + selected_user, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        File.Delete(@"C:\Users\Wacky\source\repos\ArellanoUniversityRFIDGateMonitoringSystem\ArellanoUniversityRFIDGateMonitoringSystem\Resources\ "+ selected_user);

                        conn.executeSQL("UPDATE tblrfid SET status = 'Not Paired' WHERE rfid = '" + selected_rfid + "'");

                        FormStudents_Load(sender, e);
                        rowSelected = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Deleting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int rowSelected = -1;

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowSelected = e.RowIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Datagridview", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            action = "add";
            FormUsersAddEdit fae = new FormUsersAddEdit(username,action,"");
            fae.ShowDialog();
        }
    }
}
