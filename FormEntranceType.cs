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
    public partial class FormEntranceType : Form
    {
        Connection conn = new Connection();
        string username; string datein, timein, timeout;
        public FormEntranceType(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void FormEntranceType_Load(object sender, EventArgs e)
        {

        }

        private void txttime_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = conn.GetData("SELECT * FROM tblstudents WHERE idnumber = '" + txtschoolid.Text + "'");
            if (dt.Rows.Count > 0)
            {
                txtid.Text = dt.Rows[0].Field<string>("rfid");
                txtname.Text = dt.Rows[0].Field<string>("lastname") + ", " + dt.Rows[0].Field<string>("firstname") + " " + dt.Rows[0].Field<string>("middlename");

                // INITIALIZE SPECIFIC PICTURES
                var name2 = txtschoolid.Text;
                var name = @"C:\Users\Wacky\source\repos\ArellanoUniversityRFIDGateMonitoringSystem\ArellanoUniversityRFIDGateMonitoringSystem\Resources\" + name2;
                // CREATE AN IMAGE OBJECT FROM THE SPECIFIC FILE
                Image image = Image.FromFile(name);
                // CREATE A MEMORYSTREAM
                var ms = new MemoryStream();
                // SAVE BYTES TO MS
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                // TO GET THE BYTES WE TYPED OR FETCHED
                var bytes = ms.ToArray();
                // WE NEED TO CREATE A MEMORY STREAM WITH BYTE ARRAY
                var imageMemoryStream = new MemoryStream(bytes);
                // NOW WE CREATE AN IMAGE FROM STREAM
                Image imgFromStream = Image.FromStream(imageMemoryStream);

                pictureBox1.Image = imgFromStream;


                dt = conn.GetData("SELECT * FROM tblrfid WHERE rfid = '" + txtid.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    datein = dt.Rows[0].Field<string>("datein");
                    timein = dt.Rows[0].Field<string>("timein");
                    timeout = dt.Rows[0].Field<string>("timeout");

                  
                        txtdatein.Text = datein;
                        txttimein.Text = timein;
                        txttimeout.Text = timeout;               

                    
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //enter / exit
                DataTable dt = conn.GetData("SELECT * FROM tblrfid WHERE rfid = '" + txtid.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    datein = dt.Rows[0].Field<string>("datein");
                    timein = dt.Rows[0].Field<string>("timein");
                    timeout = dt.Rows[0].Field<string>("timeout");
                    if (datein == "Pending" && timein == "Pending")
                    {
                        timein = DateTime.Now.ToLongTimeString();
                        datein = DateTime.Now.ToLongDateString();
                        txttimein.Text = timein;
                        txtdatein.Text = datein;

                        conn.executeSQL("UPDATE tblrfid SET datein = '" + txtdatein.Text + "', timein = '" + txttimein.Text + "', timeout = 'Pending' WHERE rfid = '" + txtid.Text + "'");
                        if (conn.rowAffected > 0)
                        {
                            Connection fee = new Connection();
                            fee.executeSQL("UPDATE tblstudents set status = 'INSIDE' WHERE rfid = '" + txtid.Text + "'");
                        }
                    }
                    else if (timeout == "Pending")
                    {
                        timeout = DateTime.Now.ToLongTimeString();

                        conn.executeSQL("UPDATE tblrfid SET datein = '" + datein + "', timein = '" + timein + "', timeout = '" + timeout + "'WHERE rfid = '" + txtid.Text + "'");
                        if (conn.rowAffected > 0)
                        {
                            txttimeout.Text = timeout;

                            conn.executeSQL("UPDATE tblrfid SET datein = 'Pending', timein = 'Pending', timeout = 'Pending'");
                            if (conn.rowAffected > 0)
                            {
                                Connection fee = new Connection();
                                fee.executeSQL("UPDATE tblstudents set status = 'OUTSIDE' WHERE rfid = '" + label1.Text + "'");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error on Manual Entry Form",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

    }
}
