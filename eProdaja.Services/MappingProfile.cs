using AutoMapper;
using eProdaja.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.Korisnici, Model.Korisnici>();
            CreateMap<Database.Proizvodi, Model.Proizvodi>();
            CreateMap<Database.JediniceMjere, Model.JediniceMjere>();
            CreateMap<Database.VrsteProizvodum, Model.VrsteProizvodum>();



            CreateMap<ProizvodiInsertRequest, Database.Proizvodi>();
            CreateMap<ProizvodiUpdateRequest, Database.Proizvodi>();

            CreateMap<KorisniciInsertRequest, Database.Korisnici>();
            CreateMap<KorisniciUpdateRequest, Database.Korisnici>();

            CreateMap<JediniceMjereUpsertRequest, Database.JediniceMjere>();

            CreateMap<VrsteProizvodumUpsertRequest, Database.VrsteProizvodum>();

        }
    }
}
