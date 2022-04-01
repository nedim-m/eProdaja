using eProdaja.Model;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{


    public class KorisniciController : BaseController<Model.Korisnici, object>
    {
        public KorisniciController(IKorisniciService service) : base(service)
        {
        }
    }
}
