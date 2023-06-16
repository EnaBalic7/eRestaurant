namespace eRestaurant.ViewModels
{
    public class ProizvodAddVM
    {
        public string NazivProizvoda { get; set; }
        public double PocetnaCijena { get; set; }
        public string Opis { get; set; }
        public int Recenzija { get; set; }
        public int JedinicaMjere { get; set; }
        public int ProizvodiKategorijeID { get; set; }
        public string SlikaNovaBase64 { get; set; }
    }
}
