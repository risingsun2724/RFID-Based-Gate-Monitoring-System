﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArellanoUniversityRFIDGateMonitoringSystem
{
    public partial class FormLogs : Form
    {
        string username;
        public FormLogs(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void FormLogs_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
