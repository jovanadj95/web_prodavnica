using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavnicaWeb.Models
{
    [Table("Kategorija")]
    public partial class Kategorija
    {
        public Kategorija()
        {
            Proizvod = new HashSet<Proizvod>();
        }

        public int KategorijaId { get; set; }
        [StringLength(100)]
        [Display(Name = "Naziv kategorije")]
        public string Naziv { get; set; }

        [InverseProperty("Kategorija")]
        public ICollection<Proizvod> Proizvod { get; set; }
    }
}
