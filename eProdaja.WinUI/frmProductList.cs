using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eProdaja.Model;
using Flurl;
using Flurl.Http;

namespace eProdaja.WinUI
{
    public partial class frmProductList : Form
    {
        public APIService ProductService { get; set; } = new APIService("Proizvodi");

        public frmProductList()
        {
            InitializeComponent();
        }

        private async void btnShowProductList_Click(object sender, EventArgs e)
        {
            var list = await ProductService.Get<List<Proizvodi>>();

            dgvProduct.DataSource=list;
            

         
        }

        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    var result = await ProductService.GetById<Proizvodi>(12);

        //    result.Naziv="Updated product from WinUI";

        //   // var update = await ProductService.Put<Proizvodi>(result.ProizvodId, result); // Test for update
        //}
    }
}
