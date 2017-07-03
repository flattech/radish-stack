using System.Collections.Generic;

namespace Core.Repositories
{
    public class MediaRepository : BaseRepository<Media>
    {
        public MediaRepository()
        {
            base.Init("Media", "Title,RelativePath,Type");
        }
    }


    public class Media : BaseModel
    {
        public string RelativePath { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
    }

    public class MediaMini 
    {
        public string Path { get; set; }
        public string Title { get; set; }
    }
}
