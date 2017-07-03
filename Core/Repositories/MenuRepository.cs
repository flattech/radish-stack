
using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Repositories
{
    public class MenuRepository : BaseRepository<MenuItem>
    {
        public MenuRepository()
        {
            base.Init("Menu", "Title,ParentId,EntityId,EntityName,Url,DisplayOrder,IsMega");
        }

        public IEnumerable<MenuItem> GetTree()
        {
            throw new NotImplementedException();
        }
    }

    public  class MenuItem:BaseModel
    {
        public string Title { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? EntityId { get; set; }
        public string EntityName { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMega { get; set; }
        public bool IncludeInHeader { get; set; }
        public bool IncludeInFooter { get; set; }
        public bool EnableMedia { get; set; }
    }

}
