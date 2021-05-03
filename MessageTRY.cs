using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ArellanoUniversityRFIDGateMonitoringSystem
{
    public partial class MessageTRY : Form
    {
        public MessageTRY()
        {
            InitializeComponent();
        }

        private void MessageTRY_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                // Find your Account Sid and Token at twilio.com/console
                // and set the environment variables. See http://twil.io/secure
                string accountSid = Environment.GetEnvironmentVariable("AC759b969d4991ff5b1eb4a9b6862fce55");
                string authToken = Environment.GetEnvironmentVariable("7c26005b18c0d7455c597f80dc2d1e70");

                TwilioClient.Init("AC759b969d4991ff5b1eb4a9b6862fce55", "7c26005b18c0d7455c597f80dc2d1e70");

                var message = MessageResource.Create(
                    body: "Pumasok si junjun ng 6:30 am",
                    from: new Twilio.Types.PhoneNumber("+19545194285"),
                    to: new Twilio.Types.PhoneNumber("+639174971698")
                );

                MessageBox.Show("asdas");
            
        }
    }
}
