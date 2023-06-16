namespace eRestaurant.ViewModels
{
    public class RezervacijaAddViewModel
    {
        public int id { get; set; }
        public DateTime datumRezervacije { get; set; }
        public int brojOsoba { get; set; }
        public int brojStola { get; set; }
        public string? napomena { get; set; }
        public string? status { get; set; }
        public int korisnikID { get; set; }
    }
}
