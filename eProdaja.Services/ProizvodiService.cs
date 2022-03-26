using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class ProizvodiService : IProizvodiService
    {
        public List<Proizvodi>ProizvodiList = new List<Proizvodi>() { new Proizvodi() { Id = 1, Name = "Laptop" }, new Proizvodi() { Id = 2, Name = "Televizor" } };

        public IEnumerable<Proizvodi> Get()
        {
            return ProizvodiList;
        }

        public Proizvodi GetById(int id)
        {

            return ProizvodiList.FirstOrDefault(i => i.Id == id);
        }
    }
}
