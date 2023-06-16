using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Dostavljac
    {
        [Key]
        public int DostavljacID { get; set; }
        public string PoslovniBrojTelefona { get; set; }
        public string KategorijaVozacke { get; set; }
        public string PrevoznoSredstvo { get; set; }

        [ForeignKey(nameof(RadnikID))]
        public Radnik Radnik { get; set; }
        public int RadnikID { get; set; }

    }
}
