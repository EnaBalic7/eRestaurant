namespace eRestaurant.ViewModels
{
    public class KuponUpdateVM
    {
        public int KuponID { get; set; }
        public DateTime DatumAktivacije { get; set; }
        public DateTime DatumDeaktivacije { get; set; }
        public float Popust { get; set; }
    }
}
