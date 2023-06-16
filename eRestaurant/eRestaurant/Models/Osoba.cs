using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class Osoba
    {
        [Key]
        public int OsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodenja { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Slika { get; set; }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
