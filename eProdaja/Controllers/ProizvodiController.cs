using eProdaja.Model;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{



    public class ProizvodiController : BaseController<Proizvodi,ProizvodiSearchObject>
    {
        public ProizvodiController(IProizvodiService service) : base(service)
        {
        }
    }
}
