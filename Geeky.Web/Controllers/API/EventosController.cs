namespace Geeky.Web.Controllers.API
{
    using Geeky.Web.Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    public class EventosController : Controller
    {
        private readonly IEventRepository eventRepository;

        public EventosController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return Ok(this.eventRepository.GetAll());
        }
    }
}
