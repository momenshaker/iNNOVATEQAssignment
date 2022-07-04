using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Helpers
{
    public static class ValidateExtension
    {
        /// <summary>
        /// Validate Models if null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ValidateNullOrEmpty<T>(this T source)
        {
            if (source == null) return true;
            var modelProperties = source.GetType().GetProperties();
            foreach (var property in modelProperties)
            {
                var propertyValue = property.GetValue(source, null);
                if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    return true;
                }

            }
            return false;
        }

    }
}
