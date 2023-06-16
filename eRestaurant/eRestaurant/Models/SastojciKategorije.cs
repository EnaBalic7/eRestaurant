using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class SastojciKategorije
    {
        [Key]
        public int SastojciKategorijeID { get; set; }
        public string Naziv { get; set; }
    }
}
