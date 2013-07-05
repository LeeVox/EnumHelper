using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace System
{
    public class EnumModelBindingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Check if a parameter from HttpRequest is legal format of Enum type
        /// </summary>
        private static readonly Regex rgxCheckFlagEnum = new Regex(@"(\d+,?)+");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var model = filterContext.ActionParameters["model"];

            // get all parameters which are in legal form of Enum type
            var form = filterContext.HttpContext.Request.Form;
            var flagEnumFields = new Dictionary<string, string>();
            foreach (string key in form.AllKeys)
            {
                if (rgxCheckFlagEnum.IsMatch(form[key]))
                    flagEnumFields.Add(key, form[key]);
            }

            // trying to bind those parameters values to Enum
            foreach (var flagEnumField in flagEnumFields)
            {
                var enumValue = CombineToFlagEnum(flagEnumField.Value);
                if (enumValue.HasValue)
                {
                    SetProperty(model, flagEnumField.Key.Split('.').AsEnumerable(), enumValue.Value);
                }
            }
        }

        /// <summary>
        /// Set property value of an object
        /// </summary>
        /// <param name="obj">Object to set</param>
        /// <param name="propertyPath">Path to the property</param>
        /// <param name="value">Value to be set</param>
        private void SetProperty(object obj, IEnumerable<string> propertyPath, object value)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyPath.ElementAt(0));
            if (propertyInfo != null)
            {
                if (propertyPath.Count() == 1)
                {
                    propertyInfo.SetValue(obj, value, null);
                }
                else
                {
                    SetProperty(propertyInfo.GetValue(obj, null), propertyPath.Skip(1), value);
                }
            }
        }

        /// <summary>
        /// Try to combine integer values to Enum
        /// </summary>
        /// <returns>
        /// Return null if values has only 1 item or nothing
        /// </returns>
        private int? CombineToFlagEnum(string values)
        {
            int? ret = null;
            var array = values.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length > 1)
            {
                ret = 0;
                array.ToList().ForEach(x => ret = ret | int.Parse(x));
            }

            return ret;
        }
    }
}