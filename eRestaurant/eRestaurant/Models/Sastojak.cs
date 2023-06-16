using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class Sastojak
    {
        [Key]
        public int SastojakID { get; set; }
        public string Naziv { get; set; }
        public int KolicinaNaStanju { get; set; }

        [ForeignKey(nameof(DobavljacID))]
        public int DobavljacID { get; set; }
        public Dobavljac Dobavljac { get; set; }

        [ForeignKey(nameof(SastojciKategorije))]
        public int SastojciKategorijeID { get; set; }
        public SastojciKategorije SastojciKategorije { get; set; }
    }
}
