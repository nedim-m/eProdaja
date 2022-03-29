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
    public class KorisniciService : IKorisniciService
    {
        private readonly eProdajaContext _db;
        private readonly IMapper _mapper;

        public KorisniciService(eProdajaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<Model.Korisnici> Get()
        {
            var result = _db.Korisnicis.ToList();

            return _mapper.Map<List<Model.Korisnici>>(result);
        }
    }
}
