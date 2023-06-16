using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class Uplata
    {
        [Key]
        public int UplataID { get; set; }
        public double IznosZaUplatu { get; set; }
        public string NacinPlacanja { get; set; }
        public string BrojKartice { get; set; }
        public DateTime Datum { get; set; }
    }
}
