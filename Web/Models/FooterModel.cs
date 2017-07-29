using System.Collections.Generic;
using Core.Repositories;

namespace Web.Models
{
    public class FooterModel
    {
        public FooterModel()
        {
            MenuItems = new List<MenuItem>();
        }

        public IList<MenuItem> MenuItems { get; set; }
    }
}