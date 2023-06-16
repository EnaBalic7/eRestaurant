namespace eRestaurant.ViewModels
{
    public class PitanjeGetAllViewModel
    {
        public int id { get; set; }
        public string opis { get; set; }
        public string odgovor { get; set; }
        public string? status { get; set; }
        public string datum { get; set; }
        public int korisnikID { get; set; }
        public int radnikID { get; set; }
    }
}
