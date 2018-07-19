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
    public class KategorijaController : Controller
    {
        private readonly ProdavnicaWebContext _context;
        private readonly ProdavnicaWebContext _db;

        public KategorijaController(ProdavnicaWebContext context, ProdavnicaWebContext db)
        {
            _context = context;
            _db = db;
        }

        // GET: Kategorija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategorije.ToListAsync());
        }

        // GET: Kategorija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorija = await _context.Kategorije
                .SingleOrDefaultAsync(m => m.KategorijaId == id);
            if (kategorija == null)
            {
                return NotFound();
            }

            return View(kategorija);
        }

        // GET: Kategorija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategorija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategorijaId,Naziv")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorija);
        }

        // GET: Kategorija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorija = await _context.Kategorije.SingleOrDefaultAsync(m => m.KategorijaId == id);
            if (kategorija == null)
            {
                return NotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategorijaId,Naziv")] Kategorija kategorija)
        {
            if (id != kategorija.KategorijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorijaExists(kategorija.KategorijaId))
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
            return View(kategorija);
        }

        // GET: Kategorija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorija = await _context.Kategorije
                .SingleOrDefaultAsync(m => m.KategorijaId == id);
            if (kategorija == null)
            {
                return NotFound();
            }

            return View(kategorija);
        }

        private void ProveraProizvoda(int KategorijaId)
        {
            IEnumerable<Proizvod> listaProizvoda = _db.Proizvodi.ToList();
            listaProizvoda = listaProizvoda
                    .Where(p => p.KategorijaId == KategorijaId);
            if (listaProizvoda.Any())
            {
                foreach (Proizvod proiz in listaProizvoda)
                {
                    _db.Stavke.RemoveRange(_db.Stavke.Where(s => s.ProizvodId == proiz.ProizvodId));
                    _db.SaveChanges();
                }
                _db.Proizvodi.RemoveRange(_db.Proizvodi.Where(p => p.KategorijaId == KategorijaId));
                _db.SaveChanges();
            }
        }

        // POST: Kategorija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorija = await _context.Kategorije.SingleOrDefaultAsync(m => m.KategorijaId == id);
            ProveraProizvoda(kategorija.KategorijaId);
            _context.Kategorije.Remove(kategorija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategorijaExists(int id)
        {
            return _context.Kategorije.Any(e => e.KategorijaId == id);
        }
    }
}
