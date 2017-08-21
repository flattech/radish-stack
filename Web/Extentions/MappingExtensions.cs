using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Core.Repositories;
using Core.Repositories.Enums;
using Web.Models;
using Web.Models.Post;
using Web.Models.Term;

namespace Web.Extentions
{
    public static class MappingExtensions
    {
        public static PostModel ToModel(this Post post)
        {
            string mediahost = ConfigurationManager.AppSettings["Media"];
            if (post == null)
                throw new ArgumentNullException("post");
            return new PostModel
            {
                FeaturedImage = mediahost + post.Photo,
                Id = post.Id,
                PostType = post.PostType,
                Title = post.Title,
                UrlKey = post.UrlKey,
                Detail =  post.Detail
                //Term = post.PostTerms.Select(x => x.Term).ToList()
            };
        }

        public static PostDetailsModel ToDetailsModel(this Post post)
        {
            string mediahost = ConfigurationManager.AppSettings["Media"];
            if (post == null)
                throw new ArgumentNullException("post");
            var model = new PostDetailsModel
            {
                FeaturedImage = mediahost + post.Photo,
                Id = post.Id,
                Title = post.Title,
                UrlKey = post.UrlKey,
                Detail = post.Detail.Replace("/content/uploaded/", mediahost + "/content/uploaded/"),
                // SeName = post.GetSeName(),
                PostTerms = post.PostTerms,
                Gallery = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PostAttachment>>(post.Gallery),
                CreationDate = post.CreationDate,
                TemplateViewPath = !string.IsNullOrEmpty(post.ViewPath) ? post.ViewPath : (post.PostType != null && !string.IsNullOrEmpty(post.PostType.ViewPath) ? post.PostType.ViewPath : "PostDetail.Simple")
            };

            if (post.PostType != null && !string.IsNullOrEmpty(post.PostType.PostMetaFields))
            {
                var t = post.PostMetaValues;
                if (!string.IsNullOrEmpty(t))
                {
                    var metavalues = Newtonsoft.Json.JsonConvert.DeserializeObject<PostFields[]>(t);
                    foreach (var metavalue in metavalues)
                    {
                        model.MetaFields.Add(metavalue.Key, metavalue.Value);
                    }
                }
            }
            if (post.PostType != null && !string.IsNullOrEmpty(post.PostType.PostMediaList) && !string.IsNullOrEmpty(post.Attachments))
            {
                var metavalues = Newtonsoft.Json.JsonConvert.DeserializeObject<PostAttachment[]>(post.Attachments);
                foreach (var metavalue in metavalues)
                {
                    if (!string.IsNullOrEmpty(metavalue.Value))
                        model.MediaList.Add(metavalue.Key, mediahost + metavalue.Value);
                }
                //model.FeaturedImage = model.GetMedia("Standard");
            }

            return model;
        }
        public static TermModel ToModel(this Term entity)
        {
            if (entity == null)
                return null;

            var model = new TermModel
            {
                Id = entity.Id,
                Name = entity.Title,
                IsPublic = entity.IsPublic//,
                //SeName = entity.GetSeName(),
            };
            return model;
        }
        public static TermModel ToMiniModel(this Term entity)
        {
            if (entity == null)
                return null;

            var model = new TermModel
            {
                Id = entity.Id,
                IsPublic = entity.IsPublic,
                Name = entity.Title,
                //SeName = entity.GetSeName(),
            };
            return model;
        }
    }
}