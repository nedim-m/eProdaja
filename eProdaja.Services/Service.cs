using AutoMapper;
using eProdaja.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class Service<T, TDb, TSearch> : IService<T, TSearch> where T : class where TDb : class where TSearch : class
    {
        private readonly eProdajaContext _context;



        private readonly IMapper _mapper;
        public Service(eProdajaContext context, IMapper mapper)
        {
            _context = context;



            _mapper = mapper;
        }

        public virtual IEnumerable<T> Get(TSearch search=null)
        {
            var entity = _context.Set<TDb>().AsQueryable();

            entity = AddFilter(entity, search);


            var list = entity.ToList();
            return _mapper.Map<IList<T>>(list);


        }


        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb>query,TSearch search = null)
        {
            return query;

        }

        public T GetById(int id)
        {
            var set = _context.Set<TDb>();
            var entity = set.Find(id);

            return _mapper.Map<T>(entity);

        }


    }
}
