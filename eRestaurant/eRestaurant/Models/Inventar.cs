using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Inventar
    {
        [Key]
        public int InventarID { get; set; }
        public int StanjeNaSkladistu { get; set; }
        public DateTime Datum { get; set; }

        [ForeignKey(nameof(SastojakID))]
        public int SastojakID { get; set; }
        public Sastojak Sastojak { get; set; }


        [ForeignKey(nameof(RadnikID))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
    }
}
