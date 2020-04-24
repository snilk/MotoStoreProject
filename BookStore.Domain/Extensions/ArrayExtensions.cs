using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Extensions
{
    public static class ArrayExtensions
    {
        private static readonly Random random = new Random();
        public static T GetRandomItem<T>(this IList<T> collection)
        {
            var index = random.Next(collection.Count);
            return collection[index];
        }

        public static IList<T> GetRandomItemList<T>(this IList<T> collection, int count)
        {
            var newList = new List<T>();

            for (var i = 0; i < count; i++)
            {
                var index = random.Next(collection.Count);
                newList.Add(collection[index]);
            }
            
            return newList;
        }
    }
}
