using System.Collections.Generic;
using Core.Repositories;

namespace Web.Models
{
    public class HeaderModel
    {
        public HeaderModel()
        {
            MenuItems = new List<MenuItem>();
        }

        public IList<MenuItem> MenuItems { get; set; }
    }
}