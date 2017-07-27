using System;
using System.Collections.Generic;
using Core.Repositories;
using Web.Models.Post;
using Web.Models.UI.Paging;

namespace Web.Models.Term
{
    public partial class SearchModel
    {
        public SearchModel()
        {
            //PagingFilteringContext = new BasePageableModel();
        }

        public string Query { get; set; }

        public BasePageableModel PagingFilteringContext { get; set; }
        public List<PostDetailsModel> Posts { get; set; }
    }

    public partial class TermModel
    {
        public TermModel()
        {
            Categories = new List<TermModel>();
            Tags = new List<TermModel>();
            //PagingFilteringContext = new BasePageableModel();
            RecentPosts = new List<PostDetailsModel>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public BasePageableModel PagingFilteringContext { get; set; }
        public bool DisplayCategoryBreadcrumb { get; set; }
        public IList<TermModel> CategoryBreadcrumb { get; set; }
        public IList<TermModel> Categories { get; set; }
        public IList<PostDetailsModel> Posts { get; set; }
        public IList<TermModel> Tags { get; set; }
        public bool IsPublic { get; set; }
        public List<PostDetailsModel> RecentPosts { get; set; }
        public PostType PostType { get; set; }

        #region Nested Classes

        public partial class SubCategoryModel
        {
            public string Name { get; set; }
            public string SeName { get; set; }
        }

        #endregion
    }
}