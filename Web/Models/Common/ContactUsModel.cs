using System.ComponentModel;
using System.Web.Mvc;

namespace Web.Models.Common
{
   // [Validator(typeof(ContactUsValidator))]
    public partial class ContactUsModel 
    {
        [AllowHtml]
        [DisplayName("ContactUs.Email")]
        public string Email { get; set; }

        [AllowHtml]
        [DisplayName("ContactUs.Subject")]
        public string Subject { get; set; }
        public bool SubjectEnabled { get; set; }

        [AllowHtml]
        [DisplayName("ContactUs.Enquiry")]
        public string Enquiry { get; set; }

        [AllowHtml]
        [DisplayName("ContactUs.FullName")]
        public string FullName { get; set; }

        public bool SuccessfullySent { get; set; }
        public string Result { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}