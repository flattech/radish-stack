using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMS.Models
{
    public class BaseEntityModel
    {
        public class BaseNopEntityModel
        {
            public virtual int Id { get; set; }
        }
    }
    public class MenuItemModel : BaseEntityModel
    {
        public MenuItemModel()
        {
            Children = new List<MenuItemModel>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int ParentId { get; set; }
        public int TemplateId { get; set; }
        public IList<MenuItemModel> Children { get; set; }
        public string Icon { get; set; }
    }

    public class MenuItemForm : BaseEntityModel
    {
        public MenuItemForm()
        {
            AvailableMenuItems = new List<SelectListItem>();
            AvailableTags = new List<SelectListItem>();
            Locales = new List<MenuItemLocalizedModel>();
            AvailableTerms = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? EntityId { get; set; }
        public string EntityName { get; set; }
        public IList<SelectListItem> AvailableMenuItems { get; set; }
        public IList<MenuItemLocalizedModel> Locales { get; set; }
        public IList<SelectListItem> AvailableTerms { get; set; }
        public IList<SelectListItem> AvailableTags { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public bool IncludeInFooter { get; set; }
        public bool IncludeInHeader { get; set; }
        public bool EnableMedia { get; set; }
        public bool IsMega { get; set; }
        public Guid? WidgetId { get; set; }
    }
    public partial class MenuItemLocalizedModel
    {
        public int LanguageId { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string MetaKeywords { get; set; }

        [AllowHtml]
        public string MetaDescription { get; set; }

        [AllowHtml]
        public string MetaTitle { get; set; }

        [AllowHtml]
        //[WebResourceDisplayName("Admin.Catalog.Categories.Fields.SeName")]
        public string SeName { get; set; }
    }
}