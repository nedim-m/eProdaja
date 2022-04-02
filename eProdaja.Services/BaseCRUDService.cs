﻿using AutoMapper;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate> :BaseService<T, TDb, TSearch>,
        ICRUDService<T, TSearch, TInsert, TUpdate> where T : class where TDb : class 
        where TSearch : BaseSearchObject where TUpdate : class where TInsert : class
    {
        public BaseCRUDService(eProdajaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public T Insert(TInsert insert)
        {
            var set = _context.Set<TDb>();
            TDb entity = _mapper.Map<TDb>(insert);
            set.Add(entity);

            _context.SaveChanges();

            return _mapper.Map<T>(entity);

        }

        public T Update(int id, TUpdate update)
        {
            var set = _context.Set<TDb>();

            var entity = set.Find(id);
            if (entity != null)
            {
                 _mapper.Map(update, entity);
               
            }
            else
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<T>(entity);

        }
    }
}
