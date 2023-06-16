using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class SastojakGetAllVM
    {
        public int SastojakID { get; set; }
        public string Naziv { get; set; }
        public int KolicinaNaStanju { get; set; }
        public Dobavljac Dobavljac { get; set; }
        public SastojciKategorije SastojciKategorije { get; set; }
    }
}
