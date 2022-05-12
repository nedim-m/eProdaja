using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using Microsoft.AspNetCore.Authorization;

namespace eProdaja.Controllers
{
    public class UlogeController : BaseCrudController<Model.Uloge, Model.SearchObjects.UlogeSearchObject, UlogeUpsertRequest, UlogeUpsertRequest>
    {
        
        public UlogeController(IUlogeService service) : base(service)
        {
        }
    }
}
