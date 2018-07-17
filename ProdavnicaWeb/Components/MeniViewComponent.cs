using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdavnicaWeb.Models;
using ProdavnicaWeb.Components;

namespace ProdavnicaWeb.Components
{
    public class MeniViewComponent :ViewComponent
    {
        private readonly ProdavnicaWebContext db;
        public MeniViewComponent(ProdavnicaWebContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<string> kategorija = db.Kategorije
             .Select(k => k.Naziv).Distinct()
            .OrderBy(k => k);
            return View(kategorija);
        }
    }
}
