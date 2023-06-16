using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class Dobavljac
    {
        [Key]
        public int DobavljacID { get; set; }
        public string NazivKompanije { get; set; }
        public string BrojUgovora { get; set; }
        public string KontaktOsoba { get; set; }
        public string BrojTelefona { get; set; }
    }
}
