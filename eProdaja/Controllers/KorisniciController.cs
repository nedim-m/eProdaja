using eProdaja.Model;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{


    public class KorisniciController : BaseController<Model.Korisnici, KorisniciSearchObject>
    {
        public KorisniciController(IKorisniciService service) : base(service)
        {
        }
    }
}
