using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class PitanjeAddViewModel
    {
        public int id { get; set; }
        public string opis { get; set; }
        public string odgovor { get; set; }
        public DateTime datum { get; set; }
        public string? status { get; set; }
        public int korisnikID { get; set; }
        public int radnikID { get; set; }
    }
}
