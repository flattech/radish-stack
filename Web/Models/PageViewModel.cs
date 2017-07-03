using System.Collections.Generic;
using Core.Repositories;

namespace Web.Models
{
    public partial class PageViewModel
    {
        public PageViewModel()
        {
            PictureModel = new Media();
            Widgets = new List<WidgetViewModel>();
        }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string TemplateViewPath { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
        public Media PictureModel { get; set; }
        public string FeaturedImage { get; set; }
        public List<WidgetViewModel> Widgets { get; set; }

        #region Nested Classes

        public partial class WidgetViewModel
        {
            public WidgetViewModel()
            {
                Posts = new List<PostDetailsModel>();
            }
            public string Title { get; set; }
            public int Location { get; set; }
            public string MoreUrl { get; set; }
            public string ViewPath { get; set; }
            public List<PostDetailsModel> Posts { get; set; }
            public List<TermModel> Categories { get; set; }
            public List<TermModel> Tags { get; set; }
            public bool ReturnPosts { get; set; }
            public bool ReturnCategories { get; set; }
        }

        #endregion
    }
}