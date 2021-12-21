using System.Linq;

namespace Trips.ML.API.Extensions
{
    public static class ArrayExtensions
    {
        public static void Shuffle<T>(this T[] values)
        {
            int n = values.Count();
            var rng = new Random();
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = values[n];
                values[n] = values[k];
                values[k] = temp;
            }
        }
    }
}
