using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavnicaWeb.Models
{
    [Table("Kupac")]
    public partial class Kupac
    {
        public Kupac()
        {
            Porudzbine = new HashSet<Porudzbina>();
        }

        public string KupacId { get; set; }
        [Required(ErrorMessage = "Unesi ime")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesi prezime")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Unesi drzavu")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        public string Drzava { get; set; }
        [Required(ErrorMessage = "Unesi grad")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        public string Grad { get; set; }
        [Required(ErrorMessage = "Unesi adresu")]
        [StringLength(100, ErrorMessage = "Maksimalno 100 karaktera")]
        public string Adresa { get; set; }

        [InverseProperty("Kupac")]
        public ICollection<Porudzbina> Porudzbine { get; set; }
    }
}
