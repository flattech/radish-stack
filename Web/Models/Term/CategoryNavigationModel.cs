﻿using System.Collections.Generic;

namespace Web.Models.Term
{
    public partial class CategoryNavigationModel 
    {
        public CategoryNavigationModel()
        {
            Categories = new List<CategorySimpleModel>();
        }

        public int CurrentCategoryId { get; set; }
        public List<CategorySimpleModel> Categories { get; set; }
    }
}