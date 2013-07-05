
namespace System.Web.Mvc
{
    /// <summary>
    /// This is SelectListItem but using integer for value
    /// </summary>
    public class EnumItem : SelectListItem
    {
        /*
         * Override property Value of parent class to make sure we can use only integer for value
         */
        private int _value;

        /// <summary>
        /// Gets or sets the value of the selected item.
        /// </summary>
        public new int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                base.Value = value.ToString();
            }
        }

        public EnumItem(int value, string text)
            : this(value, text, false)
        { }

        public EnumItem(int value, string text, bool selected)
            : base()
        {
            this.Value = value;
            this.Text = text;
            this.Selected = selected;
        }
    }
}
