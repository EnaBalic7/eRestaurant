using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Pitanje
    {
        [Key]
        public int PitanjeID { get; set; }
        public string Opis { get; set; }
        public string Odgovor { get; set; }
        public DateTime Datum { get; set; }
        public string Status { get; set; }


        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }


        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
    }
}
