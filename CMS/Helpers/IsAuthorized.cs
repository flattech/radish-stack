namespace CMS.Helpers
{

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    //public class IsAuthorizedAttribute : FilterAttribute, IAuthorizationFilter
    //{
    //    private readonly UserSession _userSession;
    //    private readonly IFormsService _formsAuthentication;
    //    private UserTypeEnum[] _roles;
    //    public IsAuthorizedAttribute(params UserTypeEnum[] roles)
    //    {
    //        _roles = roles;
    //        _userSession = new UserSession();
    //        _formsAuthentication = new FormsService();
    //    }
        
    //    public virtual void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        //Get the user
    //        var user = _userSession.GetCurrentUser();

    //        if (user==null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
    //        {
    //            _formsAuthentication.SignOut();
    //            //redirect to login w/ message
    //            filterContext.Result = new RedirectToRouteResult(
    //              new RouteValueDictionary {
    //                                        { "message", "Session Timed Out" },
    //                                        { "controller", "Account" },
    //                                        { "action", "Login" },
    //                                        { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
    //                                    });
    //            return;
    //        }

    //        if ( !_roles.Contains(user.TypeEnum))
    //        {
    //            var model = new UnauthorizedModel
    //                            {
    //                                CurrentUser = user,
    //                                Message =
    //                                    "The resource you attempted to access is restricted. This access attempt has been logged."
    //                            };
    //            var viewData = new ViewDataDictionary(model);
    //            filterContext.Result = new ViewResult { ViewName = "~/Views/Shared/Unauthorized.cshtml", ViewData = viewData };
    //        }
    //    }

    //}
}