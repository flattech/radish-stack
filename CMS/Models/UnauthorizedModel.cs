using Core.Repositories;

namespace CMS.Models
{
    public class UnauthorizedModel
    {
        public User CurrentUser { get; set; }
        public string Message { get; set; }

    }
}