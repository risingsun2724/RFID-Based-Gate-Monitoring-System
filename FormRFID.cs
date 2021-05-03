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
    public partial class FormRFID : Form
    {
        Connection rfid = new Connection();
        string username; private int rowSelected = -1;
        public FormRFID(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        internal void FormRFID_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = rfid.GetData("SELECT * FROM tblrfid");
                dataGridView1.DataSource = dt;

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on FormRFID Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FormRFIDAddEdit rae = new FormRFIDAddEdit(username);
            rae.ShowDialog();
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedrfid = dataGridView1.Rows[rowSelected].Cells["rfid"].Value.ToString();
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this RFID\nwith a tag : " + selectedrfid, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    rfid.executeSQL("DELETE FROM tblrfid WHERE rfid = '" + selectedrfid + "'");
                    if (rfid.rowAffected > 0)
                    {
                        MessageBox.Show("RFID DELETED with an ID Tag: " + selectedrfid, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormRFID_Load(sender, e);
                        rowSelected = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Deleting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormRFID_Load(sender,e);
            rowSelected = -1;
        }
    }
}
