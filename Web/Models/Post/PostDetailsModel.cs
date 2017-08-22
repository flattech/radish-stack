using System;
using System.Collections.Generic;
using Core.Repositories;
using Web.Models.Term;

namespace Web.Models.Post
{
    public partial class PostDetailsModel 
    {
        public PostDetailsModel()
        {
            Breadcrumb= new PostBreadcrumbModel();
            Categories= new List<TermModel>();
            Tags= new List<TermModel>();
            MetaFields = new Dictionary<string, string>();
            MediaList = new Dictionary<string, string>();
            Gallery= new List<PostAttachment>();
            Widgets= new List<PageViewModel.WidgetViewModel>();
            RecentPosts = new List<PostDetailsModel>();
        }
        public Guid Id { get; set; }
        public int PublicId { get; set; }
        public PostType PostType { get; set; }

        public string Title { get; set; }
        public string GetMedia(string key)
        {
            string s = string.Empty;
            MediaList.TryGetValue(key, out s);
            return s;
        }
        public string GetMetaField(string key)
        {
            string s=string.Empty;
            MetaFields.TryGetValue(key, out s);
            return s;
        }
        public Dictionary<string,string> MetaFields { get; set; }
        public Dictionary<string,string> MediaList { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public string TemplateViewPath { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
        public PostBreadcrumbModel Breadcrumb { get; set; }
        public string FeaturedImage { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<Core.Repositories.PostTerm> PostTerms { get; set; }
        public IEnumerable<TermModel> Categories { get; set; }
        public IEnumerable<TermModel> Tags { get; set; }
        public IEnumerable<Core.Repositories.Post> Posts { get; set; }
        public List<PostAttachment> Gallery { get; set; }
        public List<PageViewModel.WidgetViewModel> Widgets { get; set; }
        public IEnumerable<PostDetailsModel> RecentPosts { get; set; }
        public string UrlKey { get; set; }

        #region Nested Classes

        public partial class PostBreadcrumbModel 
        {
            public int Id { get; set; }
            public PostBreadcrumbModel()
            {
                CategoryBreadcrumb = new List<CategorySimpleModel>();
            }

            public bool Enabled { get; set; }
            public int PostId { get; set; }
            public string PostName { get; set; }
            public string PostSeName { get; set; }
            public IList<CategorySimpleModel> CategoryBreadcrumb { get; set; }
        }

        #endregion
    }
}