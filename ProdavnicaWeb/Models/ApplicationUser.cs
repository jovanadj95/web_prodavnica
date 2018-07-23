using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace ProdavnicaWeb.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
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
    }
}
