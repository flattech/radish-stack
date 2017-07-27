using System.Collections.Generic;
using Web.Models.Post;
using Web.Models.Term;

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