using System;
using System.Collections.Generic;
using Core.Repositories;
using Web.Models.Widget;

namespace Web.Models.Post
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FeaturedImage { get; set; }
        public string Gallery { get; set; }
        public string Author { get; set; }
        public PostType PostType { get; set; }
        public string UrlKey { get; set; }
        public string Detail { get; set; }
        public bool IsInitial { get; set; }
        public bool IsActive { get; set; }
        public string PublishedDate { get; set; }
        public string Photo { get; set; }
        public string Attachments { get; set; }

    }
}