using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class PostWidgetForm
    {
        public PostWidgetForm()
        {
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Location { get; set; }
        public Guid WidgetId { get; set; }
        public int DisplayOrder { get; set; }
    }

   
    public class PostWidgetsModel
    {
        //one coloumn top
        public List<PostWidgetForm> List1 { get; set; }
        //two coloumn middle
        public List<PostWidgetForm> List2 { get; set; }
        //two coloumn middle
        public List<PostWidgetForm> List3 { get; set; }
        //one coloumn bottom
        public List<PostWidgetForm> List4{ get; set; }

        public List<PostWidgetForm> AvailableWidgets { get; set; }
    }
}