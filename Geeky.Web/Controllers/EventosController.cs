namespace Geeky.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EventosController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;

        public EventosController(IEventRepository eventRepository, IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
            this.userHelper = userHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(this.eventRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await this.eventRepository.GetByIdAsync(id.Value);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                // TODO: Pending to change to: this.User.Identity.Name
                evento.User = await this.userHelper.GetUserByEmailAsync("juantorom@gmail.com");
                await this.eventRepository.CreateAsync(evento);
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await this.eventRepository.GetByIdAsync(id.Value);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Evento evento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Pending to change to: this.User.Identity.Name
                    evento.User = await this.userHelper.GetUserByEmailAsync("juantorom@gmail.com");
                    await this.eventRepository.UpdateAsync(evento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventRepository.ExistAsync(evento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await this.eventRepository.GetByIdAsync(id.Value);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await this.eventRepository.GetByIdAsync(id);
            await this.eventRepository.DeleteAsync(evento);
            return RedirectToAction(nameof(Index));
        }
    }
}

