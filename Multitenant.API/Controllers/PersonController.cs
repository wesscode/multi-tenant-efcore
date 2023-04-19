using Microsoft.AspNetCore.Mvc;
using Multitenant.API.Data;
using Multitenant.API.Domain;

namespace Multitenant.API.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get([FromServices] ApplicationContext db) //FromServices => recupera o contexto no metodo local.
        {
            var people = db.People.ToArray();
            return people;
        }
    }
}