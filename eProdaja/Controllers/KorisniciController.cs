using eProdaja.Model;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class KorisniciController: ControllerBase
    {
        private readonly IKorisniciService _service;

        public KorisniciController(IKorisniciService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Korisnici> Get()
        {

            return _service.Get();
        }

    }
}
