using System.Web.Routing;

namespace Web.Models.Widget
{
    public partial class RenderWidgetModel 
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
    }
}