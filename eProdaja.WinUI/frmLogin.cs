﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eProdaja.WinUI
{
    public partial class frmLogin : Form
    {

        private readonly APIService _api = new APIService("Korisnici");

        public frmLogin()
        {
            InitializeComponent();
        }

     

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            APIService.Username=txtUsername.Text;
            APIService.Password=txtPassword.Text;
            

            try
            {
                var result = await _api.Get<dynamic>();
                mdiMain frm= new mdiMain();
                frm.Show();
            }catch(Exception ex)
            {
                MessageBox.Show("Incorrect username or password");
            }
        }
    }
}
