using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class KorisnickeAdrese
    {
        [Key]
        public int KorickaAdresaID { get; set; }
        public string NazivAdreseStanovanja { get; set; }
        public string BrojUlice { get; set; }
        public string PostanskiBroj { get; set; }

        [ForeignKey(nameof(KorisnikID))]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }

    }
}
