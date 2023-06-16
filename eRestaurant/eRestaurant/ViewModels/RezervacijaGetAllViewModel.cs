namespace eRestaurant.ViewModels
{
    public class RezervacijaGetAllViewModel
    {
        public int id { get; set; }
        public int korisnikId { get; set; }
        public int brojStola { get; set; }
        public int brojOsoba { get; set; }
        public string datumRezervacije { get; set; }
        public string status { get; set; }
    }
}
