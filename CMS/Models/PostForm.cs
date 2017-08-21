using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Repositories;

namespace CMS.Models
{
    public class PostForm : BaseEntityModel
    {
        public PostForm()
        {
            PostFields = new List<PostFields>();
            AvailableTerms = new List<Term>();
            PostAttachments = new Dictionary<string, PostAttachment>();
            MediaGallery = new List<MediaMini>();
            PostMedia = new List<PostFields>();
        }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Title { get; set; }
        public List<Guid> SelectedTerms { get; set; }
        public bool IsHighlighted { get; set; }
        public PostType PostType { get; set; }
        public Dictionary<string, PostAttachment> PostAttachments { get; set; }
        public IList<Term> AvailableTerms { get; set; }
        public List<string> AvailableTags { get; set; }
        public string SelectedTags { get; set; }
        public Guid PostTypeId { get; set; }
        public Media FeatureImage { get; set; }
        public Media FeatureImageBig { get; set; }
        public int Status { get; set; }
        public string ViewPath { get; set; }
        public Guid FeatureImageBigId { get; set; }
        public DateTime CreationDate { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        //public string PostMetaValues { get; set; }
        public List<PostFields> PostFields { get; set; }
        public List<PostFields> PostMedia { get; set; }
        public string Photo { get; set; }
        public string Gallery { get; set; }
        public string Widgets { get; set; }
        public List<MediaMini> MediaGallery { get; set; }
        public string PublishedDate { get; set; }
        public string UrlKey { get; set; }
    }

}