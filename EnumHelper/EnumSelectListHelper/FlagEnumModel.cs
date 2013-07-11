using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace System
{
    public class FlagEnumModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Check if a parameter from HttpRequest is legal format of Enum type
        /// </summary>
        private static readonly Regex rgxCheckFlagEnum = new Regex(@"(\d+,?)+");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.Count <= 0)
                return;

            // get all parameters which are in legal form of Enum type
            var form = filterContext.HttpContext.Request.Form;
            var flagEnumFields = new Dictionary<string, int>();
            int? tmp = null;
            foreach (string key in form.AllKeys)
            {
                if (rgxCheckFlagEnum.IsMatch(form[key]) && (tmp = CombineToFlagEnum(form[key])) != null)
                {
                    flagEnumFields.Add(key, tmp.Value);
                }
            }
            
            foreach (var flagEnum in flagEnumFields)
            {
                foreach (var model in filterContext.ActionParameters)
                {
                    SetPropertyOrField(model.Value, flagEnum.Key.Split('.').AsEnumerable(), flagEnum.Value);
                }
            }
        }

        /// <summary>
        /// Set property value of an object
        /// </summary>
        /// <param name="obj">Object to set</param>
        /// <param name="path">Path to the property</param>
        /// <param name="value">Value to be set</param>
        private void SetPropertyOrField(object obj, IEnumerable<string> path, object value)
        {
            // try to bind property first
            var propertyInfo = obj.GetType().GetProperty(path.ElementAt(0));
            if (propertyInfo != null)
            {
                if (path.Count() == 1)
                {
                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        propertyInfo.SetValue(obj, value, null);
                    }
                }
                else
                {
                    var property = propertyInfo.GetValue(obj, null);
                    if (property == null)
                    {
                        property = Activator.CreateInstance(propertyInfo.PropertyType);
                        propertyInfo.SetValue(obj, property, null);
                    }
                    SetPropertyOrField(property, path.Skip(1), value);
                }
            }
            else
            {
                // binding field
                var fieldInfo = obj.GetType().GetField(path.ElementAt(0));
                if (fieldInfo != null)
                {
                    if (path.Count() == 1)
                    {
                        if (fieldInfo.FieldType.IsEnum)
                        {
                            fieldInfo.SetValue(obj, value);
                        }
                    }
                    else
                    {
                        var field = fieldInfo.GetValue(obj);
                        if (field == null)
                        {
                            field = Activator.CreateInstance(fieldInfo.FieldType);
                            fieldInfo.SetValue(obj, field);
                        }
                        SetPropertyOrField(field, path.Skip(1), value);
                    }
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
            if (array.Length > 0)
            {
                ret = 0;
                int tmp = 0;
                array.ToList().ForEach(
                    x => {
                        int.TryParse(x, out tmp);
                        ret = ret | tmp;
                    }
                );
            }

            return ret;
        }
    }
}
