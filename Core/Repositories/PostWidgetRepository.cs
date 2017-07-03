using System;
using System.Collections.Generic;

namespace Core.Repositories
{
    public class PostWidgetRepository : BaseRepository<PostWidget>
    {
        public PostWidgetRepository()
        {
            base.Init("PostWidget", "WidgetId,PostId,DisplayOrder,Location");
        }
    }
    public class PostWidget : BaseModel
    {
        public Guid WidgetId { get; set; }
        public Guid PostId { get; set; }
        public int DisplayOrder { get; set; }
        public int Location { get; set; }

        public virtual Widget Widget { get; set; }
        public virtual Post Post { get; set; }
    }
}
