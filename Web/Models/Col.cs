using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Col
    {
        public string lg { get; set; }
        public string text { get; set; }
        public List<Rows> rows { get; set; }
        public List<WidgetModel> widgets { get; set; }
    }
}