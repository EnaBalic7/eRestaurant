using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Models
{
    public class Meni
    {
        [Key]
        public int MeniID { get; set; }
        public DateTime DatumOdobrenjaMenija { get; set; }
        public DateTime DatumIstekaMenija { get; set; }
        public string DodatnaNapomena { get; set; }
    }
}
