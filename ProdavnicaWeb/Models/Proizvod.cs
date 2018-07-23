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
        [Required(ErrorMessage = "Unesi naziv proizvoda")]
        [StringLength(100, ErrorMessage = "Maksimalno 100 karaktera")]
        public string Naziv { get; set; }
        [Display(Name ="Opis proizvoda")]
        [Required(ErrorMessage = "Unesi opis proizvoda")]
        [StringLength(100, ErrorMessage = "Maksimalno 100 karaktera")]
        public string Opis { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [Required(ErrorMessage = "Unesi cenu")]
        public decimal Cena { get; set; }

        [ForeignKey("KategorijaId")]
        [InverseProperty("Proizvod")]
        public Kategorija Kategorija { get; set; }
        [InverseProperty("Proizvod")]
        public ICollection<Stavka> Stavke { get; set; }
    }
}
