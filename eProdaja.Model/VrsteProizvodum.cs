using System;
using System.Collections.Generic;

namespace eProdaja.Model
{
    public partial class VrsteProizvodum
    {
        //public VrsteProizvodum()
        //{
        //    Proizvodis = new HashSet<Proizvodi>();
        //}

        public int VrstaId { get; set; }
        public string Naziv { get; set; }

        //public virtual ICollection<Proizvodi> Proizvodis { get; set; }
    }
}
