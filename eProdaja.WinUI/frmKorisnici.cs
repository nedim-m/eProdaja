﻿using eProdaja.Model;
using eProdaja.Model.SearchObjects;
using System;
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
    public partial class frmKorisnici : Form
    {
        public APIService KorisniciService { get; set; } = new APIService("Korisnici");
        public frmKorisnici()
        {
            InitializeComponent();
            dgvKorisnici.AutoGenerateColumns=false;
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            var searchObject = new KorisniciSearchObject();
            searchObject.KorisnickoIme= txtUsername.Text;
            searchObject.NameFTS=txtName.Text;
            searchObject.InludeRoles=true;

            var list = await KorisniciService.Get<List<Korisnici>>(searchObject);

            dgvKorisnici.DataSource=list;
        }

       

        
    }
}
