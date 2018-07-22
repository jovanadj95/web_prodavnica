using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdavnicaWeb.Models;
using System.Net.Mail;
using System.Net;

namespace ProdavnicaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdavnicaWebContext db;

        public HomeController(ProdavnicaWebContext _db)
        {
            db = _db;
        }
        public IActionResult Index(decimal? min, decimal? max, string kategorija = "")
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

            if (listaProizvoda.Any())
            {
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


        public IActionResult PosaljiEmail(string Ime,string Prezime, string Email,string Poruka)
        {
            try
            {
                MailAddress posiljaoc = new MailAddress(Email, Ime + " " + Prezime);
                MailAddress primaoc = new MailAddress("filip.panic.1993@gmail.com");
                MailMessage poruka = new MailMessage();

                poruka.From = posiljaoc;
                poruka.To.Add(primaoc);
                poruka.Subject = "Poruka od :" + Email;
                poruka.Body = Poruka;
                poruka.IsBodyHtml = true;

                SmtpClient klijent = new SmtpClient("smtp.gmail.com");
                klijent.Port = 587;
                klijent.EnableSsl = true;
                klijent.Credentials = new NetworkCredential("filip.panic.1993@gmail.com", "jakota1993");
                klijent.Send(poruka);
                return View("Uspesno");

            }
            catch (Exception)
            {
                return View("Greska");
            }
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
