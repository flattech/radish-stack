using System.Collections.Generic;
using System.Linq;

namespace CMS.Models.Kendoui
{
    public class DataSourceRequest
    {
        public int Page { get; set; }
        public List<Sort> Sort { get; set; }
        public Filter Filter { get; set; }

        public string Order
        {
            get
            {
              if (Sort != null && Sort.Any())
                return string.Join(",", Sort.Select(s => s.ToExpression()));
                return "CreationDate Desc";
            }
        }
        public string Where
        {
            get
            {
              if (Filter != null && Filter.Logic!=null)
                  return  Filter.ToExpression(Filter.All());
                return "";
            }
        }
     
        public int PageSize { get; set; }

        public DataSourceRequest()
        {
            this.Page = 1;
            this.PageSize = 10;
        }
        //public void Fill(HttpRequestBase request)
        //{
        //    if (Sort != null)
        //        for (var i = 0; i < Sort.Count(); i++)
        //        {
        //            Sort[i].Field = request.QueryString["sort[" + i + "][field]"];
        //            Sort[i].Dir = request.QueryString["sort[" + i + "][dir]"];
        //        }
        //    //if (Filter != null)
        //    //    for (var i = 0; i < Filter.Count(); i++)
        //    //    {
        //    //        Filter[i].Field = request.QueryString["filter[" + i + "][field]"];
        //    //        Filter[i].Operator = request.QueryString["filter[" + i + "][operator]"];
        //    //        Filter[i].Value = request.QueryString["filter[" + i + "][value]"];
        //    //    }
        //}
    }
}
