using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    /// <summary>
    /// EnumDescriptionSelector
    /// </summary>
    public static class EnumDescriptionSelector
    {
        /// <summary>
        /// extension to retrieve enum description for specific enum value
        /// </summary>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static string EnumDescription(Enum enumVal)
        {
            if (enumVal != null)
            {
                var fi = enumVal.GetType().GetField(enumVal.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : enumVal.ToString();
            }
            // default to empty string
            return string.Empty;
        }
    }
}
