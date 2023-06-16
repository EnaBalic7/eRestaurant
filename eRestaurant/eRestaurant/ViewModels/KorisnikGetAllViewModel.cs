using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class KorisnikGetAllViewModel
    {
        public int id { get; set; }
        public string tipKorisnika { get; set; }
        public int osobaID { get; set; }

    }
}
