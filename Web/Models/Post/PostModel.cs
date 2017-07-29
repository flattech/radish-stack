using System;
using System.Collections.Generic;
using Core.Repositories;
using Web.Models.Widget;

namespace Web.Models.Post
{
    public class PostModel
    {
        public PostModel()
        {
            Term =new List<Core.Repositories.Term>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Core.Repositories.Term> Term { get; set; }
        public string FeaturedImage { get; set; }
        public string Gallery { get; set; }
        public string Author { get; set; }
        public PostType PostType { get; set; }
        public string UrlKey { get; set; }
    }
}