using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Geeky.Web.Data;
using Geeky.Web.Data.Entities;

namespace Geeky.Web.Controllers
{
    public class EventosController : Controller
    {
        private readonly IRepository repository;

        public EventosController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: Eventos
        public IActionResult Index()
        {
            return View(this.repository.GetEventos());
        }

        // GET: Eventos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = this.repository.GetEvento(id.Value);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
               this.repository.AddEvento(evento);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = this.repository.GetEvento(id.Value);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Evento evento)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateEvento(evento);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.EventoExists(evento.Id))
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

        // GET: Eventos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = this.repository.GetEvento(id.Value);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = this.repository.GetEvento(id);
            this.repository.RemoveEvento(evento);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
