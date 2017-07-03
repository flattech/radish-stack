using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Helpers
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue,string text="", bool byValue = false,bool choose=false)
        {
            var t = from Enum e in Enum.GetValues(enumValue.GetType())
                    where Convert.ToInt32(e) != -1
                    select new SelectListItem
                    {
                        Selected = e.Equals(enumValue),
                        Text = e.ToDescription(),
                        Value =byValue? Convert.ToInt32(e).ToString(CultureInfo.InvariantCulture):e.ToString()
                    };
           var m = t.ToList();
            if(choose)
            m.Insert(0,new SelectListItem(){Text = text,Value = "0"});
            return m;
        }

        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        //public static SelectList ToSelectList<TEnum>(this TEnum enumObj,bool byValue=false)
        //{
        //    var values = from TEnum e in Enum.GetValues(typeof(TEnum))
        //                 select new { Id = byValue ? Convert.ToInt32(e).ToString(CultureInfo.InvariantCulture) : e, Name = e.ToString() };

        //    return new SelectList(values, "Id", "Name", enumObj);
        //}
    }
}