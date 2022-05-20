using eProdaja.Model;
using eProdaja.Model.Requests;
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

        private Korisnici _model = null;

        public frmKorisniciDetails(Korisnici model = null)
        {
            InitializeComponent();
            _model=model;
        }

        private async void frmKorisniciDetails_Load(object sender, EventArgs e)
        {
            await LoadRoles();
            if (_model!=null)
            {
                txtIme.Text=_model.Ime;
                txtEmail.Text=_model.Email;
                txtUsername.Text=_model.KorisnickoIme;
                txtPrezime.Text=_model.Prezime;
                chkStatus.Checked=_model.Status.GetValueOrDefault(false);
            }
        }

        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {



                var roleList = clbUloge.CheckedItems.Cast<Uloge>().ToList();
                var roleIdList = roleList.Select(x => x.UlogaId).ToList();
                if (_model==null)
                {
                    KorisniciInsertRequest insertRequest = new KorisniciInsertRequest()
                    {

                        Ime=txtIme.Text,
                        Email=txtEmail.Text,
                        KorisnickoIme=txtUsername.Text,
                        Prezime=txtPrezime.Text,
                        Status=chkStatus.Checked,
                        Password=txtPassword.Text,
                        PasswordConfirmation=txtConfirmation.Text,
                        UlogeIdList=roleIdList

                    };
                    var user = await KorisniciService.Post<Korisnici>(insertRequest);
                }
                else
                {
                    KorisniciUpdateRequest updateRequest = new KorisniciUpdateRequest()
                    {
                        Ime=txtIme.Text,
                        Email=txtEmail.Text,
                        Prezime=txtPrezime.Text,
                        Status=chkStatus.Checked,
                        Password=txtPassword.Text,
                        PasswordConfirmation=txtConfirmation.Text,

                    };
                    _model = await KorisniciService.Put<Korisnici>(_model.KorisnikId, updateRequest);
                }
            }

        }
        private async Task LoadRoles()
        {
            var roles = await RoleService.Get<List<Uloge>>();
            clbUloge.DataSource=roles;
            clbUloge.DisplayMember="Naziv";
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                e.Cancel = true;
                txtIme.Focus();
                errorProvider.SetError(txtIme, "The Name is required!");
            }
            else
            {
                e.Cancel=false;
                errorProvider.SetError(txtIme, "");
            }
        }
    }
}
