using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavnicaWeb.Models
{
    [Table("Proizvod")]
    public partial class Proizvod
    {
        public Proizvod()
        {
            Stavke = new HashSet<Stavka>();
        }

        public int ProizvodId { get; set; }
        public int KategorijaId { get; set; }
        [Display(Name ="Naziv proizvoda")]
        [StringLength(100)]
        public string Naziv { get; set; }
        [Display(Name ="Opis proizvoda")]
        [StringLength(100)]
        public string Opis { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Cena { get; set; }

        [ForeignKey("KategorijaId")]
        [InverseProperty("Proizvod")]
        public Kategorija Kategorija { get; set; }
        [InverseProperty("Proizvod")]
        public ICollection<Stavka> Stavke { get; set; }
    }
}
