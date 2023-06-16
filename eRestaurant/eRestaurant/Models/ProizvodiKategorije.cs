using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class ProizvodiKategorije
    {
        [Key]
        public int ProizvodiKategorijeID { get; set; }
        public string NazivKategorije { get; set; }
    }
}
