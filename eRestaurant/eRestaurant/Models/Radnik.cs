using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Radnik
    {
        [Key]
        public int RadnikID { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public DateTime DatumZavrsetkaRadnogOdnosa { get; set; }
        public string Zvanje { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public string Pozicija { get; set; }


        [ForeignKey(nameof(Osoba))]
        public int OsobaID { get; set; }
        public Osoba Osoba { get; set; }

    }
}
