using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProdavnicaWeb.Models;
using ProdavnicaWeb.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using ProdavnicaWeb.Services;


namespace ProdavnicaWeb.Controllers

{
    public class KorpaController : Controller
    {
        private readonly ProdavnicaWebContext db;
        private Korpa korpa;
        private KorpaServis kServis;

        public KorpaController(ProdavnicaWebContext _db, KorpaServis _kServis)
        {
            kServis = _kServis;
            db = _db;      
            korpa = kServis.CitajKorpu();
        }

       
        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(korpa);
        }


        public IActionResult DodajStavku(int ProizvodId, string returnUrl)
        {
            Proizvod proizvod = db.Proizvodi
            .SingleOrDefault(p => p.ProizvodId == ProizvodId);

            if (proizvod != null)
            {
                korpa.DodajStavku(proizvod, 1);             
                kServis.CuvajKorpu(korpa);
               
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public IActionResult ObrisiStavku(int ProizvodId, string returnUrl)
        {
            Proizvod proizvod = db.Proizvodi
            .SingleOrDefault(p => p.ProizvodId == ProizvodId);

            if (proizvod != null)
            {
                korpa.ObrisiStavku(proizvod);
                kServis.CuvajKorpu(korpa);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public IActionResult PromeniStavku(int ProizvodId, int kolicina, string returnUrl)
        {
            Proizvod proizvod = db.Proizvodi
            .SingleOrDefault(p => p.ProizvodId == ProizvodId);
            if (proizvod != null)
            {
                korpa.PromeniStavku(proizvod, kolicina);
                kServis.CuvajKorpu(korpa);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

    }
}
