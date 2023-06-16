using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class StavkeMenia
    {
        [Key]
        public int StavkeMeniaID { get; set; }
        [ForeignKey(nameof(ProizvodID))]
        public ProizvodiKategorije Proizvod { get; set; }
        public int ProizvodID { get; set; }

        [ForeignKey(nameof(MeniID))]
        public Meni  Meni{ get; set; }
        public int MeniID { get; set; }
    }
}
