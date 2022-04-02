using AutoMapper;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class ProizvodiService : BaseService<Model.Proizvodi, Database.Proizvodi,ProizvodiSearchObject>, IProizvodiService
    {
        public ProizvodiService(eProdajaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Proizvodi> AddFilter(IQueryable<Proizvodi> query, ProizvodiSearchObject search = null)
        {
            var filteredQuery= base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Sifra))
            {
                filteredQuery = filteredQuery.Where(x => x.Sifra == search.Sifra);
            }
            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filteredQuery = filteredQuery.Where(x => x.Naziv.Contains(search.Naziv));
            }

          

            return filteredQuery;
        }

    }
}
