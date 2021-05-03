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
    public partial class FormEntranceScan : Form
    {
        Connection fe = new Connection();
        string username;
        public FormEntranceScan(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void FormEntranceScan_Load(object sender, EventArgs e)
        {
            try
            {             
                if(serialPort1.IsOpen == false)
                {
                    serialPort1.Open();
                }
            }
            catch (Exception ex)
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
        string status;
        private void ShowData(object sender, EventArgs e)
        {
            label1.Text = id;
            string datein, timein, timeout;
            try
            {
                DataTable dt = fe.GetData("SELECT * FROM tblrfid WHERE rfid = '" + label1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    datein = dt.Rows[0].Field<string>("datein");
                    timein = dt.Rows[0].Field<string>("timein");

                    timeout = dt.Rows[0].Field<string>("timeout");

                    if (datein == "Pending" && timein == "Pending")
                    {
                        timein = DateTime.Now.ToLongTimeString();
                        datein = DateTime.Now.ToLongDateString();

                        fe.executeSQL("UPDATE tblrfid SET datein = '" + datein + "', timein = '" + timein + "', timeout = 'Pending' WHERE rfid = '" + label1.Text + "'");
                        if (fe.rowAffected > 0)
                        {

                            Connection fee = new Connection();
                            fee.executeSQL("UPDATE tblstudents set status = 'INSIDE' WHERE rfid = '" + label1.Text +"'");
                            lbdatein.Text = datein;
                            lbtimein.Text = timein;
                            lbtimeout.Text = "Pending";
                        }
                    }else
                    {
                        timeout = DateTime.Now.ToLongTimeString();
                        fe.executeSQL("UPDATE tblrfid SET datein = '" + datein + "', timein = '" + timein + "', timeout = '" + timeout + "'WHERE rfid = '" + label1.Text + "' ");
                        if (fe.rowAffected > 0)
                        {
                            Connection fee = new Connection();
                            fee.executeSQL("UPDATE tblstudents set status = 'OUTSIDE' WHERE rfid = '" + label1.Text + "'");
                            lbdatein.Text = datein;
                            lbtimein.Text = timein;
                            lbtimeout.Text = timeout;
                            
                            fe.executeSQL("UPDATE tblrfid SET datein = 'Pending', timein = 'Pending', timeout = 'Pending'");
                        }
                    }



                        dt = fe.GetData("SELECT * FROM tblstudents WHERE rfid = '" + label1.Text + "'");
                        if (dt.Rows.Count > 0)
                        {
                            status = dt.Rows[0].Field<string>("status");


                            lblschoolid.Text = dt.Rows[0].Field<string>("idnumber");
                            lbllevel.Text = dt.Rows[0].Field<string>("level");
                            lblname.Text = dt.Rows[0].Field<string>("lastname") + ", " + dt.Rows[0].Field<string>("firstname") + " " + dt.Rows[0].Field<string>("middlename");
                            lblsection.Text = dt.Rows[0].Field<string>("course");

                            // INITIALIZE SPECIFIC PICTURES
                            var name2 = lblschoolid.Text;
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

                            pictureBox2.Image = imgFromStream;

                    }
                    
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
