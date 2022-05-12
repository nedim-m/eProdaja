using eProdaja.Model;
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
    public partial class frmKorisniciDetails : Form
    {
        //public APIService KorisniciService = new APIService("Korisnici");
        public APIService KorisniciService { get; set; } = new APIService("Korisnici");

        //public APIService RoleService { get; set; } = new APIService("Uloge");

        public APIService RoleService = new APIService("Uloge");

        public frmKorisniciDetails()
        {
            InitializeComponent();
        }

        private async void frmKorisniciDetails_Load(object sender, EventArgs e)
        {
            await LoadRoles();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {

        }
        private async Task LoadRoles()
        {
            var roles = await RoleService.Get<List<Uloge>>();
            clbUloge.DataSource=roles;
            clbUloge.DisplayMember="Naziv";
        }
    }
}
