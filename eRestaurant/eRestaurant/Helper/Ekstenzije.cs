namespace eRestaurant.Helper
{
    public static class Ekstenzije
    {
        public static byte[] ParsirajBase64(this string base64string)
        {
            string[] substrings = base64string.Split(',');
            int count = substrings.Length;

            if (count>1)
            {
                base64string = base64string.Split(',')[1];
                return System.Convert.FromBase64String(base64string);
            }
            else
                return System.Convert.FromBase64String(base64string);

        }
        public static string ToBase64(this byte[] bajtovi)
        {
            return System.Convert.ToBase64String(bajtovi);
        }
    }
}
