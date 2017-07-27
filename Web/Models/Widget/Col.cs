using System.Collections.Generic;

namespace Web.Models.Widget
{
    public class Col
    {
        public string lg { get; set; }
        public string text { get; set; }
        public List<Rows> rows { get; set; }
        public List<WidgetModel> widgets { get; set; }
    }
}