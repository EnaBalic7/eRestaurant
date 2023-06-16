using eRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.ViewModels
{
    public class SastojakAddVM
    {
        public string Naziv { get; set; }
        public int KolicinaNaStanju { get; set; }
        public int DobavljacID { get; set; }
        public int SastojciKategorijeID { get; set; }
    }
}
