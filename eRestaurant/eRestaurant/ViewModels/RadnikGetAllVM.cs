using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class RadnikGetAllVM
    {
        public int RadnikID { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public DateTime DatumZavrsetkaRadnogOdnosa { get; set; }
        public string Zvanje { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public string Pozicija { get; set; }
        public Osoba Osoba { get; set; }
    }
}
