using AutoMapper;
using eProdaja.Model;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : Service<Model.Korisnici, Database.Korisnici, object>, IKorisniciService
    {

        public KorisniciService(eProdajaContext db, IMapper mapper) : base(db, mapper)
        {

        }

       
    }
}