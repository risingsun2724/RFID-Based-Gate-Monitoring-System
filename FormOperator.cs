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
    public partial class FormOperator : Form
    {
        Connection conn = new Connection();
        string username, action;
        public FormOperator(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            action = "add";
            FormOperatorAddEdit foae = new FormOperatorAddEdit(username,action,"");
            foae.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                 string edit_operator = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();         
                 action = "edit";
                 FormOperatorAddEdit foae = new FormOperatorAddEdit(username, action, edit_operator);
                 foae.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Getting Credntials for EDITTING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selected_operator = dataGridView1.Rows[rowSelected].Cells["username"].Value.ToString(); 
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this OPERATOR with a USERNAME : " + selected_operator + " ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    conn.executeSQL("DELETE FROM tbltech WHERE username = '" + selected_operator + "'");
                    if (conn.rowAffected > 0)
                    {
                        MessageBox.Show("DELETED an Account with a USERNAME of " + selected_operator, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FormOperator_Load(sender, e);
                        rowSelected = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Deleting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        int rowSelected = -1;
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            FormOperator_Load(sender,e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        internal void FormOperator_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.GetData("SELECT * FROM tbltech");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Equipments Load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
