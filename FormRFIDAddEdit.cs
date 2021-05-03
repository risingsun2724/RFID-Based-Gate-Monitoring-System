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
    public partial class FormRFIDAddEdit : Form
    {
        Connection rfidaddedit = new Connection();
        string username;
        int err = 0;


        public FormRFIDAddEdit(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validateform();
            if (err == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to List this Card ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        rfidaddedit.executeSQL("INSERT INTO tblrfid(rfid,timeregistered,dateregistered,registeredby,status)" +
                            "VALUES('" + txtid.Text +"','" + txttime.Text +"','" + txtdate.Text +"','" + txtuser.Text +"','UNPAIRED')");

                        if(rfidaddedit.rowAffected > 0)
                        {
                            this.Hide();
                            FormRFID refreshForm = (FormRFID)Application.OpenForms["FormRFID"];
                            refreshForm.FormRFID_Load(refreshForm, EventArgs.Empty);
                            this.Close();
                            serialPort1.Close();
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error on Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtid.Clear();
        }

        private void FormRFIDAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                txttime.Text = DateTime.Now.ToLongTimeString();
                txtdate.Text = DateTime.Now.ToLongDateString();
                txtuser.Text = username;

                serialPort1.Open();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "There is no present Devices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
           
        }

        string id;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            id = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {        
            try
            {
                txtid.Text = id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message");
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            serialPort1.Close();
            this.Close();
        }

        public void validateform()
        {
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txtid.Text))
            {
                errorProvider1.SetError(txtid, "Scan a Card First");
                err++;
            }
            try
            {
                DataTable dt = rfidaddedit.GetData("SELECT * FROM tblrfid WHERE rfid = '" + txtid.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtid, "The RFID Tag is already Existing...");
                    err++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on search of existing rfid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void metroContextMenu1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtdate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttime_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
