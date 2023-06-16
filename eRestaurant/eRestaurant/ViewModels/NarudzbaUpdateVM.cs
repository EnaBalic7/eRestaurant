using eRestaurant.Models;

namespace eRestaurant.ViewModels
{
    public class NarudzbaUpdateVM
    {
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
        public int KorisnikID { get; set; }
        public int RadnikID { get; set; }
        public int DostavljacID { get; set; }
        public int UplataID { get; set; }
    }
}
