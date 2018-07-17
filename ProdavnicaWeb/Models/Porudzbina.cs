using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavnicaWeb.Models
{
    [Table("Porudzbina")]
    public partial class Porudzbina
    {
        public Porudzbina()
        {
            Stavke = new HashSet<Stavka>();
        }

        public int PorudzbinaId { get; set; }
        [Required]
        [StringLength(450)]
        public string KupacId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DatumKupovine { get; set; }

        [ForeignKey("KupacId")]
        [InverseProperty("Porudzbine")]
        public Kupac Kupac { get; set; }
        [InverseProperty("Porudzbina")]
        public ICollection<Stavka> Stavke { get; set; }
    }
}
