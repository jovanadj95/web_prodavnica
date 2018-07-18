﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdavnicaWeb.Models;

namespace ProdavnicaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdavnicaWebContext db;

        public HomeController(ProdavnicaWebContext _db)
        {
            db = _db;
        }
        public IActionResult Index(decimal? min, decimal? max, String kategorija = "")
        {
            ViewBag.Kategorija = kategorija;

            IEnumerable<Proizvod> listaProizvoda = db.Proizvodi;
            if (kategorija != "")
            {
                IEnumerable<Kategorija> listaKategorija = db.Kategorije;
                listaProizvoda = listaProizvoda
                    .Where(p => p.KategorijaId == listaKategorija
                    .SingleOrDefault(k => k.Naziv.Equals(kategorija)).KategorijaId);

            }

            if (listaProizvoda.Any()) {
            if (min == null)
            {
                min = listaProizvoda.Min(p => p.Cena);
            }

            if (max == null)
            {
                max = listaProizvoda.Max(p => p.Cena);
            }

            listaProizvoda = listaProizvoda
                    .Where(p => p.Cena >= min && p.Cena <= max)
                    .OrderBy(p => p.Cena);
            }
            return View("Index", listaProizvoda.ToList());
        }

        [Authorize(Policy = "SamoAdmin")]
        public IActionResult Administracija()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
