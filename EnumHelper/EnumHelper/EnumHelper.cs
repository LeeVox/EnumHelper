using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System
{
    public static class EnumHelper
    {
        /// <summary>
        /// Get the integer value of this Enum
        /// </summary>
        public static int GetIntegerValue(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        /// <summary>
        /// Determines whether one or more bit fields are set in the Enum
        /// </summary>
        public static bool HasFlag(this Enum enumValue, int flag)
        {
            return (enumValue.GetIntegerValue() & flag) == flag;
        }

        /// <summary>
        /// Get the description of this Enum
        /// </summary>
        public static string GetDescription(this Enum enumValue)
        {
            var description = enumValue.GetType().GetMember(enumValue.ToString()).Select(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()).FirstOrDefault() as DescriptionAttribute;
            return description == null ? enumValue.ToString() : description.Description;
        }

        /// <summary>
        /// Convert all values of this Enum type to a dictionary
        /// </summary>
        public static IDictionary<int, string> ToDictionary(this Enum enumValue)
        {
            return Enum.GetValues(enumValue.GetType()).OfType<Enum>()
                .ToDictionary(x => x.GetIntegerValue(), x => x.GetDescription());
        }
    }

    /// <summary>
    /// Extended DescriptionAttribute in System namespace just for purpose no need to care about namespace
    /// </summary>
    public class EnumDescriptionAttribute : DescriptionAttribute
    {
        public EnumDescriptionAttribute(string description)
            : base(description)
        { }
    }
}
