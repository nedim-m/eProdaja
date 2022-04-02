using AutoMapper;
using eProdaja.Model;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : BaseService<Model.Korisnici, Database.Korisnici, KorisniciSearchObject>, IKorisniciService
    {

        public KorisniciService(eProdajaContext db, IMapper mapper) : base(db, mapper)
        {

        }

        public override IQueryable<Database.Korisnici> AddFilter(IQueryable<Database.Korisnici> query, KorisniciSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Ime))
            {
                filteredQuery = filteredQuery.Where(x => x.Ime == search.Ime);
            }
            if (!string.IsNullOrWhiteSpace(search?.Prezime))
            {
                filteredQuery = filteredQuery.Where(x => x.Prezime.Contains(search.Prezime));
            }



            return filteredQuery;
        }

    }
}