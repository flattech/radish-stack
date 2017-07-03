using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories
{
    public class PostTypeRepository : BaseRepository<PostType>
    {
      private  List<PostType> postTypes; 
        public PostTypeRepository()
        {
            base.Init("PostType", "Title,ViewPath,DisplayOrder,Icon,EnableViewPath," +
                                  "EnableWidgets,PostMediaList,PostMetaFields,TermViewPath,IsSystem,IsActive,EnableCategories" +
                                  ",EnableGallery,EnableSummary,EnableDescription,EnableTags,EnableFeatureImage");
            postTypes = GetAll().ToList();
        }
        //public new PostType GetAll(Guid id)
        //{
        //    return postTypes.SingleOrDefault(x => x.Id == id);
        //}
        //public new PostType Get(Guid id)
        //{
        //    return postTypes.SingleOrDefault(x => x.Id == id);
        //}
    }


    public class PostType : BaseModel
    {
        public string Title { get; set; }
        public string ViewPath { get; set; }
        public int DisplayOrder { get; set; }
        public bool EnableFeatureImage { get; set; }
        public bool EnableTags { get; set; }
        public bool EnableDescription { get; set; }
        public bool EnableSummary { get; set; }
        public bool EnableGallery { get; set; }
        public bool EnableCategories { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public bool IsSystem { get; set; }
        public bool EnableViewPath { get; set; }
        public string TermViewPath { get; set; }
        public string PostMetaFields { get; set; }
        public string PostMediaList { get; set; }
        public bool EnableWidgets { get; set; }
    }

}
