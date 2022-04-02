using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{
    public class BaseCrudController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch>
        where T : class where TSearch:class where TInsert : class where TUpdate:class
    {
        public BaseCrudController(ICRUDService<T, TSearch, TInsert,TUpdate> service) : base(service)
        {
        }

        [HttpPost]
        public T Insert([FromBody] TInsert insert)
        {
            var result = ((ICRUDService<T, TSearch, TInsert, TUpdate>)this._service).Insert(insert);
            return result;
        }
        [HttpPut("{id}")]
        public T Update(int id,[FromBody]TUpdate update)
        {
            var result = ((ICRUDService<T, TSearch, TInsert, TUpdate>)this._service).Update(id,update);
            return result;
        }
    }
}
