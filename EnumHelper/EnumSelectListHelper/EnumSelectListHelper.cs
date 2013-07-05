using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class EnumSelectListHelper
    {
        // Just provide some randome string for TEMP_NAME. Refer to EnumDropDownListFor(...), EnumListBoxFor(...) for detail
        private const string TEMP_NAME = "#TEMP_NAME_EnumSelectListHelper#";

        /// <summary>
        /// Get property name when provide expression for EnumDropDownListFor(), EnumListBoxFor()
        /// </summary>
        private static readonly Regex rgxGetPropertyName = new Regex(@"Convert\((?<name>.+?)\)");

        /// <summary>
        /// Convert all values of this Enum type to SelectList
        /// </summary>
        internal static EnumSelectList ToEnumSelectList(Enum selectedValue)
        {
            var items = Enum.GetValues(selectedValue.GetType()).OfType<Enum>().Select(
                x => new EnumItem(
                    x.GetIntegerValue(),
                    x.GetDescription(),
                    selectedValue.HasFlag(x)
                )
            );
            return new EnumSelectList(items);
        }

        private static MvcHtmlString _GenerateEnumDropDownListOrListBox<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool isDropDownListType)
        {
            var propertyName = rgxGetPropertyName.Match(expression.Body.ToString()).Groups["name"].Value;
            propertyName = propertyName.Substring(propertyName.IndexOf('.') + 1);

            var propertyValue = expression.Compile().Invoke(htmlHelper.ViewData.Model);
            var selectList = propertyValue is EnumSelectList ? propertyValue as EnumSelectList : ToEnumSelectList(propertyValue as Enum);

            /*
             * HTMLHelper will not work correctly if we provide name same as property name.
             * So we just provide any dummy name and replace the result after rendered.
             * 
             * Will update this function if found any better solution.
             */
            var tmpResult =
                isDropDownListType ?
                EnumDropDownList(htmlHelper, TEMP_NAME, selectList, htmlAttributes) :
                EnumListBox(htmlHelper, TEMP_NAME, selectList, htmlAttributes);

            return new MvcHtmlString(tmpResult.ToHtmlString().Replace(TEMP_NAME, propertyName));
        }

        #region EnumDropDownList

        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, Enum selectedValue)
        {
            return EnumDropDownList(htmlHelper, name, selectedValue, null);
        }

        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, Enum selectedValue, object htmlAttributes)
        {
            return EnumDropDownList(htmlHelper, name, ToEnumSelectList(selectedValue), htmlAttributes);
        }

        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, EnumSelectList list)
        {
            return EnumDropDownList(htmlHelper, name, list, null);
        }

        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, EnumSelectList list, object htmlAttributes)
        {
            return SelectExtensions.DropDownList(htmlHelper, name, list, htmlAttributes);
        }

        #endregion

        #region EnumDropDownListFor

        public static MvcHtmlString EnumDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Enum>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Enum>> expression, object htmlAttributes)
        {
            return _GenerateEnumDropDownListOrListBox(htmlHelper, expression, htmlAttributes, true);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, EnumSelectList>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, new object());
        }

        public static MvcHtmlString EnumDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, EnumSelectList>> expression, object htmlAttributes)
        {
            return _GenerateEnumDropDownListOrListBox(htmlHelper, expression, htmlAttributes, true);
        }

        #endregion

        #region EnumListBox

        public static MvcHtmlString EnumListBox(this HtmlHelper htmlHelper, string name, Enum selectedValue)
        {
            return EnumListBox(htmlHelper, name, selectedValue, null);
        }

        public static MvcHtmlString EnumListBox(this HtmlHelper htmlHelper, string name, Enum selectedValue, object htmlAttributes)
        {
            return SelectExtensions.ListBox(htmlHelper, name, ToEnumSelectList(selectedValue), htmlAttributes);
        }

        public static MvcHtmlString EnumListBox(this HtmlHelper htmlHelper, string name, EnumSelectList list)
        {
            return EnumListBox(htmlHelper, name, list, new object());
        }

        public static MvcHtmlString EnumListBox(this HtmlHelper htmlHelper, string name, EnumSelectList list, object htmlAttributes)
        {
            return SelectExtensions.ListBox(htmlHelper, name, list, htmlAttributes);
        }

        #endregion

        #region EnumListBoxFor

        public static MvcHtmlString EnumListBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Enum>> expression)
        {
            return EnumListBoxFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString EnumListBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Enum>> expression, object htmlAttributes)
        {
            return _GenerateEnumDropDownListOrListBox(htmlHelper, expression, htmlAttributes, false);
        }

        public static MvcHtmlString EnumListBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, EnumSelectList>> expression)
        {
            return EnumListBoxFor(htmlHelper, expression, new object());
        }

        public static MvcHtmlString EnumListBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, EnumSelectList>> expression, object htmlAttributes)
        {
            return _GenerateEnumDropDownListOrListBox(htmlHelper, expression, htmlAttributes, false);
        }

        #endregion
    }
}
