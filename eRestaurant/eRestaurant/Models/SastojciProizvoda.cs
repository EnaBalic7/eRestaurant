using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class SastojciProizvoda
    {
        [Key]
        public int SastojciProizvodaID { get; set; }
        
        [ForeignKey(nameof(ProizvodID))]
        public int ProizvodID { get; set; }
        public Proizvod Proizvod { get; set; }

        [ForeignKey(nameof(SastojakID))]
        public int SastojakID { get; set; }
        public Sastojak Sastojak { get; set; }
    }
}
