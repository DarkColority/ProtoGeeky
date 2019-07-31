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
    using Microsoft.AspNetCore.Authorization;
    using Geeky.Web.Data.Repositories;

    [Authorize]
    public class TematicasController : Controller
    {

        private readonly ITematicasRepository tematicasRepository;


        public TematicasController(ITematicasRepository tematicasRepository, IUserHelper userHelper)
        {
            this.tematicasRepository = tematicasRepository;

        }

        // GET: Products
        public IActionResult Index()
        {
            return View(this.tematicasRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tematica = await this.tematicasRepository.GetByIdAsync(id.Value);
            if (tematica == null)
            {
                return NotFound();
            }

            return View(tematica);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TematicaViewModel view)
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


                var tematica = this.ToTematica(view, path);
                // TODO: Pending to change to: this.User.Identity.Name
               
                await this.tematicasRepository.CreateAsync(tematica);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private Tematica ToTematica(TematicaViewModel view, string path)
        {
            return new Tematica
            {
                Id = view.Id,
                Nombre = view.Nombre,
                ImageURL = path
            };
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tematica = await this.tematicasRepository.GetByIdAsync(id.Value);
            if (tematica == null)
            {
                return NotFound();
            }

            var view = this.toTematicaViewModel(tematica);

            return View(view);
        }

        private TematicaViewModel toTematicaViewModel(Tematica tematica)
        {
            return new TematicaViewModel
            {
                Id = tematica.Id,
                Nombre = tematica.Nombre,
                
                ImageURL = tematica.ImageURL
            };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TematicaViewModel view)
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


                    var tematica = this.ToTematica(view, path);
                  
                    await this.tematicasRepository.UpdateAsync(tematica);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.tematicasRepository.ExistAsync(view.Id))
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tematica = await this.tematicasRepository.GetByIdAsync(id.Value);
            if (tematica == null)
            {
                return NotFound();
            }

            return View(tematica);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tematica = await this.tematicasRepository.GetByIdAsync(id);
            await this.tematicasRepository.DeleteAsync(tematica);
            return RedirectToAction(nameof(Index));
        }
    }
}
