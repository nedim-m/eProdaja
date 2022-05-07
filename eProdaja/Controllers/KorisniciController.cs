using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{

    [Authorize]
    public class KorisniciController : BaseCrudController<Model.Korisnici,KorisniciSearchObject,KorisniciInsertRequest,KorisniciUpdateRequest>
    {
        public KorisniciController(IKorisniciService service) : base(service)
        {
        }
    }
}
