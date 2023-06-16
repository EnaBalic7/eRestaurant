using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class Kupon
    {
        [Key]
        public int KuponID { get; set; }
        public DateTime DatumAktivacije { get; set; }
        public DateTime DatumDeaktivacije { get; set; }
        public float Popust { get; set; }
        public bool Deaktiviran { get; set; }
    }
}
