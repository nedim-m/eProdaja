using AutoMapper;
using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : BaseCRUDService<Model.Korisnici, Database.Korisnici, KorisniciSearchObject, KorisniciInsertRequest, KorisniciUpdateRequest>, IKorisniciService
    {

        public KorisniciService(eProdajaContext db, IMapper mapper) : base(db, mapper)
        {

        }


        public override Model.Korisnici Insert(KorisniciInsertRequest insert)
        {
            var entity = base.Insert(insert);

            foreach (var ulogaId in insert.UlogeIdList)
            {
                Database.KorisniciUloge KorisniciUloge = new Database.KorisniciUloge();
                KorisniciUloge.UlogaId=ulogaId;
                KorisniciUloge.KorisnikId = entity.KorisnikId;
                KorisniciUloge.DatumIzmjene = DateTime.Now;
                _context.KorisniciUloges.Add(KorisniciUloge);
            }
            _context.SaveChanges();

            return entity;
        }

        public override void BeforeInsert(KorisniciInsertRequest insert, Database.Korisnici entity)
        {
            var salt = GenerateSalt();
            entity.LozinkaSalt = salt;
            entity.LozinkaHash = GenerateHash(salt, insert.Password);
            base.BeforeInsert(insert, entity);
        }


        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            provider.GetBytes(byteArray);


            return Convert.ToBase64String(byteArray);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }


        public override IQueryable<Database.Korisnici> AddFilter(IQueryable<Database.Korisnici> query, KorisniciSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.KorisnickoIme))
            {
                filteredQuery = filteredQuery.Where(x => x.KorisnickoIme == search.KorisnickoIme);
            }
            if (!string.IsNullOrWhiteSpace(search?.NameFTS))
            {
                filteredQuery = filteredQuery.Where(x => x.KorisnickoIme.Contains(search.NameFTS) ||
                x.Ime.Contains(search.NameFTS) || 
                x.Prezime.Contains(search.NameFTS));
            }



            return filteredQuery;
        }

        public Model.Korisnici Login(string username, string password)
        {
            var entity = _context.Korisnicis.Include("KorisniciUloges.Uloga").FirstOrDefault(x => x.KorisnickoIme == username);
            if (entity == null)
            {
                return null;
            }

            var hash = GenerateHash(entity.LozinkaSalt, password);

            if (hash != entity.LozinkaHash)
            {
                return null;
            }

            return _mapper.Map<Model.Korisnici>(entity);
        }
    }
}