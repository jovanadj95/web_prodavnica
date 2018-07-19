using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProdavnicaWeb.Models;

namespace ProdavnicaWeb.Controllers
{
    public class ProizvodController : Controller
    {
        private readonly ProdavnicaWebContext _context;

        public ProizvodController(ProdavnicaWebContext context)
        {
            _context = context;
        }

        // GET: Proizvods
        public async Task<IActionResult> Index()
        {
            var prodavnicaWebContext = _context.Proizvodi.Include(p => p.Kategorija);
            return View(await prodavnicaWebContext.ToListAsync());
        }

        // GET: Proizvods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi
                .Include(p => p.Kategorija)
                .SingleOrDefaultAsync(m => m.ProizvodId == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // GET: Proizvods/Create
        public IActionResult Create()
        {
            ViewBag.KategorijeLista = new SelectList(_context.Kategorije.Select(k => k), "KategorijaId", "Naziv");
            return View();
        }

        // POST: Proizvods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProizvodId,KategorijaId,Naziv,Opis,Cena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije, "KategorijaId", "KategorijaId", proizvod.KategorijaId);
            return View(proizvod);
        }

        // GET: Proizvods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi.SingleOrDefaultAsync(m => m.ProizvodId == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            ViewBag.KategorijeLista = new SelectList(_context.Kategorije.Select(k => k), "KategorijaId", "Naziv");
            return View(proizvod);
        }

        // POST: Proizvods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProizvodId,KategorijaId,Naziv,Opis,Cena")] Proizvod proizvod)
        {
            if (id != proizvod.ProizvodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.ProizvodId))
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
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije, "KategorijaId", "KategorijaId", proizvod.KategorijaId);
            return View(proizvod);
        }

        // GET: Proizvods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi
                .Include(p => p.Kategorija)
                .SingleOrDefaultAsync(m => m.ProizvodId == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // POST: Proizvods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.Proizvodi.SingleOrDefaultAsync(m => m.ProizvodId == id);
            _context.Proizvodi.Remove(proizvod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvodi.Any(e => e.ProizvodId == id);
        }
    }
}
