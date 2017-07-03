using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS.Models
{
    public class WidgetForm
    {
        public WidgetForm()
        {
            Locales = new List<WidgetLocalizedModel>();
            AvailableTerms = new List<SelectListItem>();
            AvailableTags = new List<SelectListItem>();
            AvailablePostTypes = new List<SelectListItem>();
            AvailablePosts = new List<PostView>();
            SourceCategories = new List<string>();
            SourceTags = new List<string>();
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public List<string> SourceCategories { get; set; }
        public List<string> SourceTags { get; set; }
        public string SourcePosts { get; set; }
        public List<PostView> AvailablePosts { get; set; }
        public List<SelectListItem> AvailableTerms { get; set; }
        public List<SelectListItem> AvailableTags { get; set; }
        public List<SelectListItem> AvailablePostTypes { get; set; }

        public IList<WidgetLocalizedModel> Locales { get; set; }
        public string ViewPath { get; set; }
        public string PostType { get; set; }
        public bool ReturnTags { get; set; }
        public bool ReturnCategories { get; set; }
        public bool ReturnPosts { get; set; }
        public int PostCount { get; set; }
    }

    public class WidgetLocalizedModel
    {
        public string Title { get; set; }

        public string SeName { get; set; }

        public int LanguageId { get; set; }
    }

    public class PostView
    {
        public string Title { get; set; }
        public string Photo { get; set; }
        public Guid Id { get; set; }
    }
}