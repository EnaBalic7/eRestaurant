using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get;set;}
        public string TipKorisnika { get; set; }

        [ForeignKey(nameof(Osoba))]
        public int OsobaID { get; set; }
        public Osoba Osoba { get; set; }


        public override string ToString()
        {
            if(Osoba == null){
                return OsobaID.ToString();
            }
            else
            {
                return Osoba.ToString();
            }
        }
    }
}
