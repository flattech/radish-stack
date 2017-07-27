using System;
using System.Collections.Generic;
using Core.Repositories;
using Web.Models.Widget;

namespace Web.Models.Post
{
    public class PostModel
    {

        public DateTime LastModified { get; set; }
        public string Title { get; set; }
        public string Widgets { get; set; }
        public string Photo { get; set; }
        public string Gallery { get; set; }
        public string Author { get; set; }
        public string Detail { get; set; }
        public Guid PostTypeId { get; set; }
        public bool IsInitial { get; set; }
        public string ViewPath { get; set; }
        public string Attachments { get; set; }
        public string PostMetaValues { get; set; }

        public PostType PostType { get; set; }
        public List<PostMeta> PostMetas { get; set; }
        public List<PostTerm> PostTerms { get; set; }
        public List<PostWidget> PostWidgets { get; set; }
        public Media FeaturedImage { get; set; }
        public string TemplateView { get; set; }
        public bool IsActive { get; set; }
        public Rows Rows { get; set; }
    }
}