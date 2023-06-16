using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class SanitarnaAddVM
    {
        public DateTime DatumInspekcije { get; set; }
        public string BrojUgovora { get; set; }
        public DateTime DatumOvjere { get; set; }
        public string DodatnaNapomena { get; set; }
        public string PDF { get; set; }
        public bool PrilozenPDF { get; set; }

        public int RadnikID { get; set; }
    }
}
