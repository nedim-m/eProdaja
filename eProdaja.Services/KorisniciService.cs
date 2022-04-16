using AutoMapper;
using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : BaseCRUDService<Model.Korisnici,Database.Korisnici,KorisniciSearchObject,KorisniciInsertRequest,KorisniciUpdateRequest>,IKorisniciService
    {

        public KorisniciService(eProdajaContext db, IMapper mapper) : base(db, mapper)
        {

        }


        public override Model.Korisnici Insert(KorisniciInsertRequest insert)
        {
            var entity= base.Insert(insert);

            foreach(var ulogaId in insert.UlogeIdList)
            {
                KorisniciUloge KorisniciUloge= new KorisniciUloge();
                KorisniciUloge.UlogaId=ulogaId;
                KorisniciUloge.KorisnikId = entity.KorisnikId;
                KorisniciUloge.DatumIzmjene = DateTime.Now;
                _context.KorisniciUloges.Add(KorisniciUloge);
            }
            _context.SaveChanges();

            return entity;
        }

        public override void BeforeInsert(KorisniciInsertRequest insert, Database.Korisnici entitiy)
        {
            var salt = GenerateSalt();
            entitiy.LozinkaSalt = salt;
            entitiy.LozinkaHash = GenerateHash(salt, insert.Password);
            base.BeforeInsert(insert, entitiy);
        }

        //not implemented
        public static string GenerateSalt()
        {
            return Convert.ToBase64String(new byte[16]);//not working 

            //var buf = new byte[16];
            //(new RSACryptoServiceProvider()).GetBytes(buf);
            //return Convert.ToBase64String(buf);
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