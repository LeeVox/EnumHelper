using System.Web.Mvc;

namespace System
{
    public static class EnumSelectListExtension
    {
        /// <summary>
        /// Convert all values of this Enum type to EnumSelectList with some added items
        /// </summary>
        public static EnumSelectList AddItems(this Enum enumValue, params string[] items)
        {
            return EnumSelectListHelper.ToEnumSelectList(enumValue).AddItems(items);
        }

        /// <summary>
        /// Convert all values of this Enum type to EnumSelectList with some inserted items at position 0
        /// </summary>
        public static EnumSelectList InsertItems(this Enum enumValue, params string[] items)
        {
            return InsertItems(enumValue, 0, items);
        }

        /// <summary>
        /// Convert all values of this Enum type to EnumSelectList with some inserted items
        /// </summary>
        public static EnumSelectList InsertItems(this Enum enumValue, int position, params string[] items)
        {
            return EnumSelectListHelper.ToEnumSelectList(enumValue).InsertItems(position, items);
        }

        /// <summary>
        /// Convert all values of this Enum type to EnumSelectList with some added items
        /// </summary>
        public static EnumSelectList AddItems(this Enum enumValue, params EnumItem[] items)
        {
            return EnumSelectListHelper.ToEnumSelectList(enumValue).AddItems(items);
        }

        /// <summary>
        /// Convert all values of this Enum type to EnumSelectList with some inserted items at position 0
        /// </summary>
        public static EnumSelectList InsertItems(this Enum enumValue, params EnumItem[] items)
        {
            return InsertItems(enumValue, 0, items);
        }

        /// <summary>
        /// Convert all values of this Enum type to EnumSelectList with some inserted items
        /// </summary>
        public static EnumSelectList InsertItems(this Enum enumValue, int position, params EnumItem[] items)
        {
            return EnumSelectListHelper.ToEnumSelectList(enumValue).InsertItems(position, items);
        }
    }
}