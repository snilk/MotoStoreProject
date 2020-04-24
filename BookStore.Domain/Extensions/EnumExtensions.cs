

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BookStore.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumObj)
        {
            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            var attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }

            return attribArray[0] is DescriptionAttribute attrib ? attrib.Description : enumObj.ToString();
        }

        /// <summary>
        /// Gets the values of an enum.
        /// </summary>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
