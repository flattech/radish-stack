
using System;
using System.Collections.Generic;

namespace Core.Repositories
{
    public class WidgetRepository : BaseRepository<Widget>
    {
        public WidgetRepository()
        {
            base.Init("Widget", "Title,SourceCategories,SourceTags,SourcePosts,ViewPath,PostCount,PostType,Tags,Categories,Posts,IsActive,ReturnTags,ReturnCategories,ReturnPosts");
        }
    }

    public partial class Widget : BaseModel
    {
        public Widget()
        {
            this.PostWidgets = new List<PostWidget>();
        }

        public string Title { get; set; }
        public string SourceCategories { get; set; }
        public string SourceTags { get; set; }
        public string SourcePosts { get; set; }
        public string ViewPath { get; set; }
        public int PostCount { get; set; }

        public List<PostWidget> PostWidgets { get; set; }

        public string PostType { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public string Posts { get; set; }
        public bool ReturnTags { get; set; }
        public bool ReturnCategories { get; set; }
        public bool ReturnPosts { get; set; }
        public bool IsActive { get; set; }
    }

    //public class WidgetConfig
    //{
    //    public string PostType { get; set; }
    //    public string Tags { get; set; }
    //    public bool ReturnTags { get; set; }
    //    public string Categorys { get; set; }
    //    public bool ReturnCategorys { get; set; }
    //    public string Posts { get; set; }
    //    public bool ReturnPosts { get; set; }
    //}

}
