using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class StavkeNarudzbe
    {
        [Key]
        public int StavkeNarudzbeID { get; set; }
        public double Cijena { get; set; }
        public string DodatnaNapomena { get; set; }
        public int Kolicina { get; set; }
        public float Popust { get; set; }

        [ForeignKey(nameof(NarudzbaID))]
        public Narudzba Narudzba { get; set; }
        public int NarudzbaID { get; set; }

        [ForeignKey(nameof(ProizvodID))]
        public Proizvod Proizvod { get; set; }
        public int ProizvodID { get; set; }

        [ForeignKey(nameof(KuponID))]
        public Kupon Kupon { get; set; }
        public int KuponID { get; set; }

    }
}
