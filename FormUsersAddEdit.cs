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
    public partial class FormUsersAddEdit : Form
    {
        Connection studentsaddeddit = new Connection();
        string username, action, edit_user, edit_idnumber;
        int errorCount = 0;
        public FormUsersAddEdit(string username, string action, string edit_user)
        {
            InitializeComponent();
            this.username = username;
            this.action = action;
            this.edit_user = edit_user;
        }
        int err = 0;
        public void validateform()
        {
            errorProvider1.Clear();


            if(pictureBox1.Image == null)
            {
                errorProvider1.SetError(pictureBox1, "insert image");
                err++;
            }
            if (string.IsNullOrEmpty(txtid.Text))
            {
                errorProvider1.SetError(txtid, "Scan a Card First");
                err++;
            }
            if (string.IsNullOrEmpty(txtschoolid.Text))
            {
                errorProvider1.SetError(txtschoolid, "School ID # is needed...");
                err++;
            }
            if (string.IsNullOrEmpty(txtlastname.Text))
            {
                errorProvider1.SetError(txtlastname, "Lastname is needed...");
                err++;
            }
            if (string.IsNullOrEmpty(txtfirstname.Text))
            {
                errorProvider1.SetError(txtfirstname, "Firstname is needed...");
                err++;
            }
            if (string.IsNullOrEmpty(txtmiddlename.Text))
            {
                errorProvider1.SetError(txtmiddlename, "Middlename is needed...");
                err++;
            }
            if (string.IsNullOrEmpty(txtguardian.Text))
            {
                errorProvider1.SetError(txtguardian, "Name of Guardian is needed...");
                err++;
            }
            if (string.IsNullOrEmpty(txtmobilenumber.Text))
            {
                errorProvider1.SetError(txtmobilenumber, "Mobile Number is needed...");
                err++;
            }
            if (string.IsNullOrEmpty(txtaddress.Text))
            {
                errorProvider1.SetError(txtaddress, "Address is needed...");
                err++;
            }

                if (cmbtype.SelectedIndex < 0)
                {
                    errorProvider1.SetError(cmbtype, "please select one...");
                    err++;
                }
                if (cmblevel.SelectedIndex < 0)
                {
                    errorProvider1.SetError(cmblevel, "please select one...");
                    err++;
                }
            if (cmbtype.SelectedIndex == 0)
            {
                if (cmbcourse.SelectedIndex < 0)
                {
                    errorProvider1.SetError(cmbcourse, "please select one...");
                    err++;
                }
            }

                try
                {
                DataTable dt = studentsaddeddit.GetData("SELECT * FROM tblrfid WHERE rfid = '" + txtid.Text + "'");
                if (dt.Rows.Count <= 0)
                {
                    errorProvider1.SetError(txtid, "This RFID tag is still not registered...");
                    err++;
                }

                if (action == "add")
                    {
                     dt = studentsaddeddit.GetData("SELECT * FROM tblstudents WHERE rfid = '" + txtid.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider1.SetError(txtid, "This RFID is already been paired");
                        err++;
                    }
                    
                }


                if (action == "add") {
                    dt = studentsaddeddit.GetData("SELECT * FROM tblstudents WHERE idnumber = '" + txtschoolid.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider1.SetError(txtschoolid, "School ID number is existing...");
                        err++;
                    }
                 }
                else
                {
                    if(txtid.Text == edit_user)
                    {

                    }
                    else
                    {
                        dt = studentsaddeddit.GetData("SELECT * FROM tblstudents WHERE rfid = '" + edit_user + "'");
                        if (dt.Rows.Count > 0)
                        {
                            errorProvider1.SetError(txtschoolid, "The RFID is already been paired...");
                            err++;
                        }
                    }
                    if(txtschoolid.Text == edit_idnumber)
                    {
                       
                    }
                    else
                    {
                        dt = studentsaddeddit.GetData("SELECT * FROM tblstudents WHERE idnumber = '" + edit_idnumber + "'");
                        if (dt.Rows.Count > 0)
                        {
                            errorProvider1.SetError(txtschoolid, "The School ID number is already been paired...");
                            err++;
                        }
                    }
                  }
                
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message, "Error on search of existing rfid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            validateform();
            if (err == 0)
            {
                if (action == "add")
                {
                    if (errorCount == 0)
                    {
                        DialogResult dr = MessageBox.Show("Are you sure you want to List this Card ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            try
                            {

                                File.Copy(tbpath.Text, Path.Combine(@"C:\Users\Wacky\source\repos\ArellanoUniversityRFIDGateMonitoringSystem\ArellanoUniversityRFIDGateMonitoringSystem\Resources\", txtschoolid.Text));
                                string imagepath = Path.Combine(@"C:\Users\Wacky\source\repos\ArellanoUniversityRFIDGateMonitoringSystem\ArellanoUniversityRFIDGateMonitoringSystem\Resources\", txtschoolid.Text);
                                //string imagepath = "";
                                if (cmbtype.Text == "STUDENT")
                                {
                                    studentsaddeddit.executeSQL("INSERT INTO tblstudents(rfid,idnumber,lastname,firstname,middlename,level,course,type,phonenumber,address,guardian,timeregistered,dateregistered,registeredby,imgpath,status)"
                                                              + "VALUES('" + txtid.Text + "','" + txtschoolid.Text + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtmiddlename.Text + "'," +
                                                              "'" + cmblevel.Text + "','" + cmbcourse.Text + "','" + cmbtype.Text + "','" + txtmobilenumber.Text + "','" + txtaddress.Text + "','" + txtguardian.Text + "'," +
                                                              "'" + txttimeregistered.Text + "','" + txtdateregistered.Text + "','" + txtregisteredby.Text + "', '" + tbpath.Text + "','OUTSIDE')");

                                    if (studentsaddeddit.rowAffected > 0)
                                    {
                                        studentsaddeddit.executeSQL("UPDATE tblrfid set status = 'Paired' WHERE rfid = '" + txtid.Text + "'");
                                        FormUsers refreshForm = (FormUsers)Application.OpenForms["FormUsers"];
                                        refreshForm.FormStudents_Load(refreshForm, EventArgs.Empty);
                                        this.Close();
                                        serialPort1.Close();
                                    }
                                }
                                else
                                {
                                    studentsaddeddit.executeSQL("INSERT INTO tblstudents(rfid,idnumber,lastname,firstname,middlename,level,course,type,phonenumber,address,guardian,timeregistered,dateregistered,registeredby,imgpath)"
                                                              + "VALUES('" + txtid.Text + "','" + txtschoolid.Text + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtmiddlename.Text + "'," +
                                                              "'" + cmblevel.Text + "','','" + cmbtype.Text + "','" + txtmobilenumber.Text + "','" + txtaddress.Text + "','" + txtguardian.Text + "'," +
                                                              "'" + txttimeregistered.Text + "','" + txtdateregistered.Text + "','" + txtregisteredby.Text + "','" + tbpath.Text + "')");

                                    if (studentsaddeddit.rowAffected > 0)
                                    {
                                        studentsaddeddit.executeSQL("UPDATE tblrfid set status = 'PAIRED' WHERE rfid = '" + txtid.Text + "'");
                                        FormUsers refreshForm = (FormUsers)Application.OpenForms["FormUsers"];
                                        refreshForm.FormStudents_Load(refreshForm, EventArgs.Empty);
                                        this.Close();
                                        serialPort1.Close();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error on Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else if (action == "edit")
                {
                    if (errorCount == 0)
                    {
                        DialogResult dr = MessageBox.Show("Are you sure you want to Update this Card ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                if (cmbtype.Text == "STUDENT")
                                {
                                    studentsaddeddit.executeSQL("UPDATE tblstudents SET idnumber = '" + txtschoolid.Text + "', lastname = '" + txtlastname.Text + "', firstname = '" + txtfirstname.Text + "', middlename = '" + txtmiddlename.Text + "'," +
                                    "level = '" + cmblevel.Text + "', course = '" + cmbcourse.Text + "', type = '" + cmbtype.Text + "', phonenumber = '" + txtmobilenumber.Text + "', address = '" + txtaddress.Text + "', guardian = '" + txtguardian.Text + "'" +
                                    "WHERE rfid = '" + edit_user + "'");

                                    if (studentsaddeddit.rowAffected > 0)
                                    {
                                        FormUsers refreshForm = (FormUsers)Application.OpenForms["FormUsers"];
                                        refreshForm.FormStudents_Load(refreshForm, EventArgs.Empty);
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    studentsaddeddit.executeSQL("UPDATE tblstudents SET idnumber = '" + txtschoolid.Text + "', lastname = '" + txtlastname.Text + "', firstname = '" + txtfirstname.Text + "', middlename = '" + txtmiddlename.Text + "'," +
                                    "level = '" + cmblevel.Text + "', course = '', type = '" + cmbtype.Text + "', phonenumber = '" + txtmobilenumber.Text + "', address = '" + txtaddress.Text + "', guardian = '" + txtguardian.Text + "'" +
                                    "WHERE rfid = '" + edit_user + "'");

                                    if (studentsaddeddit.rowAffected > 0)
                                    {
                                        FormUsers refreshForm = (FormUsers)Application.OpenForms["FormUsers"];
                                        refreshForm.FormStudents_Load(refreshForm, EventArgs.Empty);
                                        this.Close();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error on Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (action == "add")
            {
                serialPort1.Close();
            }
            this.Close();
        }

        private void txtregisteredby_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbjob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtype.SelectedIndex == 0)
            {
                lbltype.Text = "Grade Level , Section and Course";
                labellevel.Text = "Level";
                labelCourse.Text = "Course";
                labelCourse.Visible = true;
                cmbcourse.Visible = true;
            }
            else if(cmbtype.SelectedIndex == 1)
            {
                lbltype.Text = "Employee Position";
                labellevel.Text = "Job Title";
                labelCourse.Visible = false;
                cmbcourse.Visible = false;
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
                txtid.Text = id;     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                tbpath.Text = opf.FileName;
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(tbpath.Text, Path.Combine(@"C:\Users\Wacky\source\repos\ArellanoUniversityRFIDGateMonitoringSystem\ArellanoUniversityRFIDGateMonitoringSystem\Resources\ ", "wendy"));
                //File.Delete("your imgpath column")
                MessageBox.Show("SUCCESSFUL");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                      
        }

        private void tbpath_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
    
            txtschoolid.Clear();
            txtlastname.Clear();
            txtfirstname.Clear();
            txtmiddlename.Clear();
            txtguardian.Clear();
            txtmobilenumber.Clear();
            txtaddress.Clear();
            cmbcourse.SelectedIndex = -1;
            cmblevel.SelectedIndex = -1;


        }

        private void FormStudentsAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
               
                if (action == "add")
                {
                    serialPort1.Open();
                    txttimeregistered.Text = DateTime.Now.ToLongTimeString();
                    txtdateregistered.Text = DateTime.Now.ToLongDateString();
                    txtregisteredby.Text = username;

                   
                }
                if (action == "edit")
                {
                    label2.Text = "EDIT RFID USER";
                    button2.Text = "&Update";


                    txtid.Text = edit_user;                    

                    DataTable dt = studentsaddeddit.GetData("SELECT * FROM tblstudents WHERE rfid = '" + txtid.Text + "'");

                    txtschoolid.Text = dt.Rows[0].Field<string>("idnumber");
                    edit_idnumber = txtschoolid.Text;
                    txtlastname.Text = dt.Rows[0].Field<string>("lastname"); 
                    txtfirstname.Text = dt.Rows[0].Field<string>("firstname"); 
                    txtmiddlename.Text = dt.Rows[0].Field<string>("middlename"); 
                    txttimeregistered.Text = dt.Rows[0].Field<string>("timeregistered"); 
                    txtdateregistered.Text = dt.Rows[0].Field<string>("dateregistered"); 
                    txtregisteredby.Text = dt.Rows[0].Field<string>("registeredby"); 
                    txtguardian.Text = dt.Rows[0].Field<string>("guardian"); 
                    txtmobilenumber.Text = dt.Rows[0].Field<string>("phonenumber"); 
                    txtaddress.Text = dt.Rows[0].Field<string>("address"); 
                    cmbtype.Text = dt.Rows[0].Field<string>("type"); 
                    cmblevel.Text = dt.Rows[0].Field<string>("level");
                    if (cmbtype.Text == "STUDENT") {
                        cmbcourse.Text = dt.Rows[0].Field<string>("course"); 
                    }
                    // INITIALIZE SPECIFIC PICTURES
                    var name2 = txtschoolid.Text;
                    var name = @"C:\Users\Wacky\source\repos\ArellanoUniversityRFIDGateMonitoringSystem\ArellanoUniversityRFIDGateMonitoringSystem\Resources\" + name2;
                    tbpath.Text = name ;
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



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "There is no present Devices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
