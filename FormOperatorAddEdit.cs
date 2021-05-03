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
    public partial class FormOperatorAddEdit : Form
    {
        Connection conn = new Connection();
        string username, action, edit_operator;
        string currentpass;
        public FormOperatorAddEdit(string username, string action,string edit_operator)
        {
            InitializeComponent();
            this.username = username;
            this.action = action;
            this.edit_operator = edit_operator;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void FormOperatorAddEdit_Load(object sender, EventArgs e)
        {
            if (action == "add")
            {
                txttime.Text = DateTime.Now.ToLongTimeString();
                txtdate.Text = DateTime.Now.ToLongDateString();
                txtuser.Text = username;
            }
            if (action == "edit")
            {
                label2.Text = "EDIT OPERATOR";
                txtid.Enabled = false;
                txtusername.Enabled = false;

                try
                {
                    DataTable dt = conn.GetData("SELECT * FROM tbltech WHERE username = '" + edit_operator + "'");
                    if(dt.Rows.Count > 0)
                    {
                        txtusername.Text = edit_operator;
                        txtpassword.Text = dt.Rows[0].Field<string>("password");
                        currentpass = dt.Rows[0].Field<string>("password");
                        txtid.Text = dt.Rows[0].Field<string>("idnumber");
                        txttime.Text = dt.Rows[0].Field<string>("timeregistered");
                        txtdate.Text = dt.Rows[0].Field<string>("dateregistered");
                        txtuser.Text = dt.Rows[0].Field<string>("registeredby");
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on EDIT Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validateform();
            if (action == "add")
            {
                if (errorCount == 0)
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to List this USER ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            conn.executeSQL("INSERT INTO tbltech (username, password, idnumber, timeregistered, dateregistered, registeredby)" +
                                "VALUES('" + txtusername.Text + "','" + txtpassword.Text + "','" + txtid.Text + "','" + txttime.Text + "','" + txtdate.Text + "','" +
                                txtuser.Text + "')");
                            if (conn.rowAffected > 0)
                            {
                                MessageBox.Show("New Operator Listed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);                               
                                FormOperator refreshForm = (FormOperator)Application.OpenForms["FormOperator"];
                                refreshForm.FormOperator_Load(refreshForm, EventArgs.Empty);
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                if (errorCount == 0)
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to Update this USER with a USERNAME : " + txtusername.Text + " ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            conn.executeSQL("UPDATE tbltech set password = '" + txtpassword.Text + "' WHERE username = '" + txtusername.Text + "'");
                            if (conn.rowAffected > 0)
                            {
                                MessageBox.Show("Operators's Information Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                FormOperator refreshForm = (FormOperator)Application.OpenForms["FormOperator"];
                                refreshForm.FormOperator_Load(refreshForm, EventArgs.Empty);
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error on Editting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        int errorCount;
        public void validateform()
        {
            errorProvider1.Clear();
            errorCount = 0;

            if (string.IsNullOrEmpty(txtusername.Text))
            {
                errorProvider1.SetError(txtusername, "Username is required...");
                errorCount++;
            }
            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                errorProvider1.SetError(txtpassword, "Password is required...");
                errorCount++;
            }
            if (string.IsNullOrEmpty(txtid.Text))
            {
                errorProvider1.SetError(txtid,"School ID is highly required");
                errorCount++;
            }   
            
            if (action == "add")
            {
                try
                {
                    DataTable dt = conn.GetData("SELECT * FROM tbltech WHERE username = '" + txtusername.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider1.SetError(txtusername, "The Serial Number is already Existing...");
                        errorCount++;
                    }
                    dt = conn.GetData("SELECT * FROM tbltech WHERE idnumber = '" + txtid.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider1.SetError(txtid, "Duplication of Operator Accionts is not allowed or The ID is either being used or is already Existing...");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on search serial or asset", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                try
                {
                   // FOR FUTURE PASSWORD CHANGE
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on search serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

    }
}
