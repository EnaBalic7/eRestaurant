using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Rezervacija
    {
        [Key]
        public int RezervacijaID { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int BrojOsoba { get; set; }
        public int BrojStola { get; set; }
        public string? Napomena { get; set; }
        public string Status { get; set; }

        [ForeignKey(nameof(KorisnikID))]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
    }
}
