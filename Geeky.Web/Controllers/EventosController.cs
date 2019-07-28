namespace Geeky.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Geeky.Web.Models;
    using System.IO;
    using System;

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
        public async Task<IActionResult> Create(EventViewModel view)
        {
            if (ModelState.IsValid)
            {

                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Events",
                        view.ImageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Events/{view.ImageFile.FileName}";
                }


                var evento = this.ToEvent(view, path);
                // TODO: Pending to change to: this.User.Identity.Name
                evento.User = await this.userHelper.GetUserByEmailAsync("juantorom@gmail.com");
                await this.eventRepository.CreateAsync(evento);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private Evento ToEvent(EventViewModel view, string path)
        {
            return new Evento
            {
                Id = view.Id,
                Nombre = view.Nombre,
                Direccion = view.Direccion,
                Tematica = view.Tematica,
                Descripcion = view.Descripcion,
                ImageURL = path
            };
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

            var view = this.toEventViewModel(evento);

            return View(view);
        }

        private EventViewModel toEventViewModel(Evento evento)
        {
            return new EventViewModel
            {
                Id = evento.Id,
                Nombre = evento.Nombre,
                Direccion = evento.Direccion,
                Tematica = evento.Tematica,
                Descripcion = evento.Descripcion,
                ImageURL = evento.ImageURL
            };
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var path = view.ImageURL;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Events",
                            view.ImageFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Events/{view.ImageFile.FileName}";
                    }


                    var evento = this.ToEvent(view, path);
                    // TODO: Pending to change to: this.User.Identity.Name
                    evento.User = await this.userHelper.GetUserByEmailAsync("juantorom@gmail.com");
                    await this.eventRepository.UpdateAsync(evento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventRepository.ExistAsync(view.Id))
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

            return View(view);
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

