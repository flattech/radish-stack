using System.Collections.Generic;
using Core.Repositories;

namespace Web.Models
{
    public class TopMenuModel
    {
        public TopMenuModel()
        {
            MenuItems = new List<MenuItem>();
        }

        public IList<MenuItem> MenuItems { get; set; }
    }
}