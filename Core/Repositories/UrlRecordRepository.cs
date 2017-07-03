using System.Collections.Generic;

namespace Core.Repositories
{
    public class UrlRecordRepository : BaseRepository<UrlRecord>
    {
        public UrlRecordRepository()
        {
            base.Init("UrlRecord", "Title,Detail,OrganiserId,VenueId,Photo");
        }
    }


    public class UrlRecord : BaseModel
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public int TaxonomyId { get; set; }
        public int Count { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public int PostTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public virtual PostType PostType { get; set; }
    }

}
