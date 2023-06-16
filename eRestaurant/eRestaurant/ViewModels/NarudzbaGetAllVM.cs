using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class NarudzbaGetAllVM
    {
        public int NarudzbaID { get; set; }
        public bool TipNarudzbe { get; set; }
        public double UkupnaCijenaBezPopusta { get; set; }
        public float Popust { get; set; }
        public double UkupnaCijena { get; set; }
        public string StatusNarudzbe { get; set; }
        public string DodatnaNapomena { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public double CijenaBezPDV { get; set; }
        public int PDV { get; set; }
        public double UkupnaCijenaSaPDV { get; set; }
        public Korisnik Korisnik { get; set; }
        public Radnik Radnik { get; set; }
        public Dostavljac Dostavljac { get; set; }
        public Uplata Uplata { get; set; }
    }
}
