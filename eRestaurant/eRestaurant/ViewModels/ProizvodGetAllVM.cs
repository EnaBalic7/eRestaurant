using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class ProizvodGetAllVM
    {
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public double PocetnaCijena { get; set; }
        public string Opis { get; set; }
        public int Recenzija { get; set; }
        public int JedinicaMjere { get; set; }
        public int ProizvodiKategorijeID { get; set; }
        public string SlikaNovaBase64 { get; set; } //za POST-anje
        public byte[] SlikaPostojeca { get; set; } //za GET-anje
    }
}
