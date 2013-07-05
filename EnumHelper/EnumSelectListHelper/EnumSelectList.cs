using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    public class EnumSelectList : List<EnumItem>
    {
        public EnumSelectList(IEnumerable<EnumItem> items)
            : base(items)
        { }

        public EnumSelectList(IEnumerable<EnumItem> items, params int[] selectedValues)
            : base(items)
        {
            SetSelectedItem(selectedValues);
        }

        public EnumSelectList(IEnumerable<EnumItem> items, params string[] selectedTexts)
            : base(items)
        {
            SetSelectedItem(selectedTexts);
        }

        #region add/insert

        #region add/insert by string

        /// <summary>
        /// Get the smallest flag which is greater than this Enum integer value
        /// </summary>
        /// <param name="value">Integer value of the Enum</param>
        private int getNextFlag(int enumValue)
        {
            int ret = 1;
            while (ret <= enumValue)
                ret = ret << 1;
            return ret;
        }

        private EnumItem[] convertToEnumItems(string[] items)
        {
            var nextKey = getNextFlag(this.Max(x => x.Value));
            var enumItems = items.Select(
                x =>
                {
                    var ret = new EnumItem(nextKey, x);
                    nextKey = nextKey << 1;
                    return ret;
                }
            ).ToArray();
            return enumItems;
        }

        /// <summary>
        /// Add some items to the list
        /// </summary>
        /// <param name="items">
        /// Item values will be set automatically (greater than maximum value in existing list)
        /// </param>
        public EnumSelectList AddItems(params string[] items)
        {
            return AddItems(convertToEnumItems(items));
        }

        /// <summary>
        /// Insert some items to the list at position 0
        /// </summary>
        /// <param name="items">
        /// Item values will be set automatically (greater than maximum value in existing list)
        /// </param>
        public EnumSelectList InsertItems(params string[] items)
        {
            return InsertItems(0, items);
        }

        /// <summary>
        /// Insert some items to the list
        /// </summary>
        /// <param name="position">
        /// Position to insert
        /// </param>
        /// <param name="items">
        /// Item values will be set automatically (greater than maximum value in existing list)
        /// </param>
        public EnumSelectList InsertItems(int position, params string[] items)
        {
            return InsertItems(0, convertToEnumItems(items));
        }

        #endregion

        #region add/insert by EnumItem

        /// <summary>
        /// Add some items to the list
        /// </summary>
        public EnumSelectList AddItems(params EnumItem[] items)
        {
            this.AddRange(items);
            return this;
        }

        /// <summary>
        /// Insert some items to the list at position 0
        /// </summary>
        public EnumSelectList InsertItems(params EnumItem[] items)
        {
            return InsertItems(0, items);
        }

        /// <summary>
        /// Insert some items to the list
        /// </summary>
        /// <param name="position">
        /// Position to insert
        /// </param>
        public EnumSelectList InsertItems(int position, params EnumItem[] items)
        {
            this.InsertRange(position, items);
            return this;
        }

        #endregion

        #endregion

        #region set selected items

        /// <summary>
        /// Set Selected items base on value
        /// </summary>
        public EnumSelectList SetSelectedItem(params int[] values)
        {
            return SetSelectedItem(false, values);
        }

        /// <summary>
        /// Set Selected items base on value
        /// </summary>
        /// <param name="clearAllCurrentSelected">
        /// Clear all current selected items before execute
        /// </param>
        public EnumSelectList SetSelectedItem(bool clearAllCurrentSelected, params int[] values)
        {
            if (clearAllCurrentSelected)
                this.ForEach(x => x.Selected = false);
            this.Where(x => values.Contains(x.Value)).ToList().ForEach(x => x.Selected = true);
            return this;
        }

        /// <summary>
        /// Set Selected items base on text
        /// </summary>
        public EnumSelectList SetSelectedItem(params string[] texts)
        {
            return SetSelectedItem(false, texts);
        }

        /// <summary>
        /// Set Selected items base on text
        /// </summary>
        /// <param name="clearAllCurrentSelected">
        /// Clear all current selected items before execute
        /// </param>
        public EnumSelectList SetSelectedItem(bool clearAllCurrentSelected, params string[] texts)
        {
            if (clearAllCurrentSelected)
                this.ForEach(x => x.Selected = false);
            this.Where(x => texts.Any(text => string.Compare(x.Text, text, true) == 0)).ToList().ForEach(x => x.Selected = true);
            return this;
        }

        #endregion
    }
}
