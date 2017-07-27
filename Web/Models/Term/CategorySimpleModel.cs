using System.Collections.Generic;

namespace Web.Models.Term
{
    public class CategorySimpleModel 
    {
        public CategorySimpleModel()
        {
            SubCategories = new List<CategorySimpleModel>();
        }

        public string Name { get; set; }


        public bool IncludeInTopMenu
        {
            get { return true; }
        }

        public List<CategorySimpleModel> SubCategories { get; set; }
        public bool IsMega { get; set; }
        public int EntityId { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }
    }
}