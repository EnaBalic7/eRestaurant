namespace eRestaurant.ViewModels
{
    public class OsobaUpdateVM
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public DateTime datumRodenja { get; set; }
        public string brojTelefona { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? slika { get; set; }
    }
}
