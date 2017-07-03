using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;
using Core.Repositories;
using Core.Repositories.Enums;

namespace CMS.Helpers
{
    public static class HtmlExtensions
    {
        public static HelperResult RenderTreeCheckBoxes(this HtmlHelper helper, IList<Term> tree, List<Guid> selected)
        {
            return new HelperResult(writer =>
            {
                writer.Write(new MvcHtmlString(RenderTree(tree, selected, null)));
            });
        }

        private static Term GetRoot(IList<Term> tree, Guid categoryid)
        {
            var current = tree.SingleOrDefault(x => x.Id == categoryid);
            if (current == null)
                return null;
            if (current.ParentId == null)
                return current;
            return GetRoot(tree, current.ParentId.Value);
        }
        private static string RenderTree(IList<Term> tree, List<Guid> selected, Guid? parentid = null)
        {
            var tabStrip = new StringBuilder();
            var childs = tree.Where(x => x.ParentId == parentid).ToList();
            if (!childs.Any())
                return "";
            tabStrip.AppendLine("<ul>");
            foreach (var t in childs)
            {
                tabStrip.AppendLine("<li>");
                tabStrip.AppendLine("<label><input type='checkbox' name='SelectedTerms[]' value='" + t.Id + "' " +
                                    ((selected != null && selected.Contains(t.Id)) ? "checked='checked'" : "") +
                                    "><a href='/term/edit/" + t.Id + "'>" + t.Title + "</a></label>");
                tabStrip.AppendLine(RenderTree(tree, selected, t.Id));
                tabStrip.AppendLine("</li>");
            }
            tabStrip.AppendLine("</ul>");
            return tabStrip.ToString();
        }
        #region Metronic Template Helpers

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string icon, string current, params UserRoleEnum[] roles)
        {
            if (roles == null)
                return MvcHtmlString.Empty;

           // var userSession = new UserSession();
//            var user = userSession.GetCurrentUser();
//
//            if (user == null || !roles.Contains(user.TypeEnum))
//            {
//                return MvcHtmlString.Empty;
//            }
            var isMatch = current == controllerName;
            var html = string.Format(
                "<li class='{0}'><a href='{1}'><i class='fa fa-{2}'></i><span class='title'>{3}</span>{4}</a></li>",
                isMatch ? "active" : "", "/" + controllerName + "/" + actionName, icon, linkText, isMatch ? "<span class='selected'></span>" : "");

            return new MvcHtmlString(html);
        }
        public static MvcHtmlString AlertMessage(this HtmlHelper html)
        {
            var result = new StringBuilder();

            if (html.ViewContext.TempData.ContainsKey("SuccessMessage"))
            {
                var successMessage = (string)html.ViewContext.TempData["SuccessMessage"];
                if (!string.IsNullOrEmpty(successMessage))
                {
                    result.AppendFormat(
                        "<div class=\"alert alert-success\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>{0}</div>",
                        successMessage);
                }
            }

            if (html.ViewContext.TempData.ContainsKey("ErrorMessage"))
            {
                var errorMessage = (string)html.ViewContext.TempData["ErrorMessage"];
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    result.AppendFormat("<div class=\"alert alert-danger\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button><strong>{0}</strong></div>", errorMessage);
                }
            }

            if (!html.ViewData.ModelState.IsValid)
            {
                result.AppendFormat("<div class=\"alert alert-danger\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button><strong>{0}</strong></div>", html.ValidationSummary());
            }
            return new MvcHtmlString(result.ToString());
        }

        public static MvcHtmlString MetronicBackButton(this HtmlHelper html, string url)
        {
            return new MvcHtmlString(string.Format("<a href='{0}' class='btn  btn-primary'><i class='fa fa-arrow-left bigger-110'></i> " + AppResources.Instance.Get("Back") +" </a>", url));
        }
        //public static MvcHtmlString MetronicLink(this HtmlHelper html, string url, string text, string icon, string color)
        //{
        //    return new MvcHtmlString(string.Format("<a href='{0}' class='btn  btn-{3}'><i class=' fa fa-{2} bigger-110'></i>{1}</a>", url, text, icon, color));
        //}
        public static MvcHtmlString MetronicLink(this HtmlHelper html, string url, string text, string icon, string color, string size = "btn-sm")
        {
            return new MvcHtmlString(string.Format("<a href='{0}' class='btn {4} {3}'><i class='fa fa-{2} '></i> {1}</a>", url, text, icon, color, size));
        }
        public static MvcHtmlString MetronicEditLink(this HtmlHelper html, string url)
        {
            return new MvcHtmlString(string.Format("<a href='{0}' class='btn btn-sm  yellow'><i class='fa fa-pencil'></i> " + AppResources.Instance.Get("Edit") + " </a>", url));
        }
        public static MvcHtmlString MetronicDetailsLink(this HtmlHelper html, string url)
        {
            return new MvcHtmlString(string.Format("<a href='{0}' class='btn btn-sm  btn-primary'><i class='fa fa-ellipsis-horizontal'></i> " + AppResources.Instance.Get("Details") + " </a>", url));
        }
        public static MvcHtmlString MetronicDeleteLink(this HtmlHelper html, string url)
        {
            return new MvcHtmlString(@"<a onclick='bootbox.confirm(""Are you sure you want to delete?"",function(result){if(result){location.href=""" + url + @"""; }});' href=""#"" class=""btn btn-sm red""><i class=""fa fa-trash-o""></i> " + AppResources.Instance.Get("Delete") + " </a>");
        }
        public static MvcHtmlString MetronicActionButton(this HtmlHelper html, string url, string text, string icon)
        {
            return new MvcHtmlString(string.Format("<a href='{0}' class='btn  btn-primary'><i class='{2} bigger-110'></i>{1}</a>", url, text, icon));
        }
        public static MvcHtmlString MetronicLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.LabelFor(expression, new { @class = "control-label col-md-3" });
        }

        public static MvcHtmlString MetronicSaveButton(this HtmlHelper html)
        {
            return new MvcHtmlString(string.Format("<button type='submit' class='btn green'><i class='fa fa-check'></i> " + AppResources.Instance.Get("Save") + " </button>"));
        }
        public static MvcHtmlString MetronicSearchButton(this HtmlHelper html)
        {
            return new MvcHtmlString(string.Format("<button type='submit' class='btn btn-primary'><i class='fa fa-check'></i> " + AppResources.Instance.Get("Search") + " </button>"));
        }

        public static MvcHtmlString MetronicTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, int maxlength)
        {
            return helper.TextAreaFor(expression, 2, 5, new { @class = "col-xs-12 limited autosize", maxlength = maxlength, data_bind = "myTextArea:{}" });
        }
        public static MvcHtmlString MetronicTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.TextBoxFor(expression, new { @class = "form-control" });
        }
        public static MvcHtmlString MetronicTextBoxReadonlyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.TextBoxFor(expression, new { @class = "col-xs-12", @readonly = "readonly" });
        }
        public static MvcHtmlString Pager(this HtmlHelper helper, int currentPage, int pageSize, int totalItemCount, object routeValues)
        {
            if (totalItemCount == 0)
                return new MvcHtmlString("");
            // how many pages to display in each page group const
            int cGroupSize = 5;
            var pageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

            // cleanup any out bounds page number passed
            currentPage = Math.Max(currentPage, 1);
            currentPage = Math.Min(currentPage, pageCount);

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            var container = new TagBuilder("ul");
            // container.AddCssClass("clearfix");
            container.AddCssClass("pagination");
            var actionName = helper.ViewContext.RouteData.GetRequiredString("Action");

            // calculate the last page group number starting from the current page
            // until we hit the next whole divisible number
            var lastGroupNumber = currentPage;
            while ((lastGroupNumber % cGroupSize != 0)) lastGroupNumber++;

            // correct if we went over the number of pages
            var groupEnd = Math.Min(lastGroupNumber, pageCount);

            // work out the first page group number, we use the lastGroupNumber instead of
            // groupEnd so that we don't include numbers from the previous group if we went
            // over the page count
            var groupStart = lastGroupNumber - (cGroupSize - 1);

            // if we are past the first page
            if (currentPage > 1)
            {
                var previous = new TagBuilder("a");
                previous.SetInnerText("<");
                previous.AddCssClass("prev");
                var routingValues = new RouteValueDictionary(routeValues);
                routingValues.Add("page", currentPage - 1);
                var page = currentPage - 1;
                previous.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                previous.MergeAttribute("data-page", page.ToString(CultureInfo.InvariantCulture));
                container.InnerHtml += "<li>" + previous + "</li>";
            }

            // if we have past the first page group
            if (currentPage > cGroupSize)
            {
                var previousDots = new TagBuilder("a");
                previousDots.SetInnerText("...");
                previousDots.AddCssClass("previous-dots");
                var routingValues = new RouteValueDictionary(routeValues);
                routingValues.Add("page", groupStart - cGroupSize);
                previousDots.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                var page = groupStart - cGroupSize;
                previousDots.MergeAttribute("data-page", page.ToString(CultureInfo.InvariantCulture));
                container.InnerHtml += "<li>" + previousDots.ToString() + "</li>";
            }

            for (var i = groupStart; i <= groupEnd; i++)
            {
                var pageNumber = new TagBuilder("a");
                //pageNumber.AddCssClass(((i == currentPage)) ? "active" : "page");
                pageNumber.SetInnerText((i).ToString());
                var routingValues = new RouteValueDictionary(routeValues);
                routingValues.Add("page", i);
                pageNumber.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                var page = i;
                pageNumber.MergeAttribute("data-page", page.ToString(CultureInfo.InvariantCulture));

                container.InnerHtml += string.Format("<li class='{0}'>", ((i == currentPage)) ? "active" : "page") + pageNumber.ToString() + "</li>";
            }

            // if there are still pages past the end of this page group
            if (pageCount > groupEnd)
            {
                var nextDots = new TagBuilder("a");
                nextDots.SetInnerText("...");
                nextDots.AddCssClass("next-dots");
                var routingValues = new RouteValueDictionary(routeValues);
                routingValues.Add("page", groupEnd + 1);
                nextDots.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                var page = groupEnd + 1;
                nextDots.MergeAttribute("data-page", page.ToString(CultureInfo.InvariantCulture));

                container.InnerHtml += "<li>" + nextDots.ToString() + "</li>";
            }

            // if we still have pages left to show
            if (currentPage < pageCount)
            {
                var next = new TagBuilder("a");
                next.SetInnerText(">");
                next.AddCssClass("next");
                var routingValues = new RouteValueDictionary(routeValues);
                routingValues.Add("page", currentPage + 1);
                next.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
                var page = currentPage + 1;
                next.MergeAttribute("data-page", page.ToString(CultureInfo.InvariantCulture));
                container.InnerHtml += "<li>" + next.ToString() + "</li>";
            }

            return MvcHtmlString.Create(container.ToString());
        }

       
          #endregion
    }

}