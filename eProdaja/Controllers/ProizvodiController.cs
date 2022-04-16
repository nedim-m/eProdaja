using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{



    public class ProizvodiController : BaseCrudController<Model.Proizvodi,ProizvodiSearchObject,ProizvodiInsertRequest, ProizvodiUpdateRequest>
    {
        public IProizvodiService proizvodiService { get; set; }
        public ProizvodiController(IProizvodiService service) : base(service)
        {
            proizvodiService = service; 
        }

        [HttpPut("{id}/Activate")]
        public Model.Proizvodi Activate(int id)
        {
           var result = proizvodiService.Activate(id);
            return result;
        }

        [HttpPut("{id}/AllowedActions")]
        public List<string> AllowedActions(int id)
        {
            var result = proizvodiService.AllowedActions(id);
            return result;
        }

    }
}
