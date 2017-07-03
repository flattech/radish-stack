using System.Collections.Generic;

namespace Web.Models.Common
{
    public partial class SitemapModel 
    {
        public SitemapModel()
        {
            Products = new List<PostDetailsModel>();
            Categories = new List<TermModel>();
          }
        public IList<PostDetailsModel> Products { get; set; }
        public IList<TermModel> Categories { get; set; }
    }
}