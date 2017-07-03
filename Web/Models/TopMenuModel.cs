using System.Collections.Generic;

namespace Web.Models
{
    public partial class TopMenuModel 
    {
        public TopMenuModel()
        {
            Categories = new List<CategorySimpleModel>();
        }

        public IList<CategorySimpleModel> Categories { get; set; }
      
     

        #region Nested classes

        public class TopMenuTopicModel 
        {
            public string Name { get; set; }
            public string SeName { get; set; }
        }

        #endregion
    }
}