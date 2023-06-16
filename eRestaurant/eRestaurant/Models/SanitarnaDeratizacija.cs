using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Models
{
    public class SanitarnaDeratizacija
    {
        [Key]
        public int SanitarnaDeratizacijaID { get; set; }
        public DateTime DatumInspekcije { get; set; }
        public string BrojUgovora { get; set; }
        public DateTime DatumOvjere { get; set; }
        public string DodatnaNapomena { get; set; }
        public byte[] PDF { get; set; }
        public bool PrilozenPDF { get; set; }

        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }

    }
}
