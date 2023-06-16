using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class KorisnickeAdreseAddViewModel
    {
        public int id { get; set; }
        public string nazivAdreseStanovanja { get; set; }
        public string brojUlice { get; set; }
        public string postanskiBroj { get; set; }
        public int korisnikID { get; set; }
    }
}
