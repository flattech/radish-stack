using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Repositories;
using Core.Repositories.Enums;

namespace CMS.Models
{
    public class TermModel : BaseEntityModel
    {
        public TermModel()
        {
            Children = new List<TermModel>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public Guid ParentId { get; set; }
        public int TemplateId { get; set; }
        public IList<TermModel> Children { get; set; }
        public string Icon { get; set; }
    }
    public class TermForm : BaseEntityModel
    {
        public TermForm()
        {
            AvailableTerms = new List<SelectListItem>();
            AvailableTypes = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public Guid? ParentId { get; set; }
        public Guid PostTypeId { get; set; }
        public PostType PostType { get; set; }
        public int TaxonomyId { get; set; }
        public IList<SelectListItem> AvailableTerms { get; set; }
        public IList<SelectListItem> AvailableTypes { get; set; }
        public string SeName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublic { get; set; }
        public TermTypeEnum Taxonomy { get; set; }
    }
}