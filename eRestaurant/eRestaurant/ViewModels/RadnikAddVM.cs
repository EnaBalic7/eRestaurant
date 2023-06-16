using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class RadnikAddVM
    {
        public DateTime DatumZaposlenja { get; set; }
        public DateTime DatumZavrsetkaRadnogOdnosa { get; set; }
        public string Zvanje { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public string Pozicija { get; set; }
        public int OsobaID { get; set; }
    }
}
