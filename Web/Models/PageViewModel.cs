using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Core.Repositories;

namespace Web.Models
{
    public class PageViewModel
    {
        public PageViewModel()
        {
            pictureModel = new Media();
            rows = new List<RowViewModel>();
            menus = new List<MenuItem>();
        }

        public string title { get; set; }
        public string shortDescription { get; set; }
        public string fullDescription { get; set; }
        public string templateViewPath { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }
        public string metaTitle { get; set; }
        public string seName { get; set; }
        public Media pictureModel { get; set; }
        public string featuredImage { get; set; }
        public List<RowViewModel> rows { get; set; }
        public List<MenuItem> menus { get; set; }



        #region Nested Classes

        //public class RowsViewModel
        //{
        //    public RowsViewModel()
        //    {
        //        rows = new List<RowViewModel>();
        //    }
        //    public List<RowViewModel> rows { get; set; }
        //}

        public class RowViewModel
        {
            public RowViewModel()
            {
                cols = new List<ColViewModel>();
            }

            public List<ColViewModel> cols { get; set; }
            public string classname { get; set; }
        }


        public class ColViewModel
        {

            public ColViewModel()
            {
                rows = new List<RowViewModel>();
                widgets = new List<WidgetViewModel>();
            }
            public string lg { get; set; }
            public string classname { get; set; }
            public string text { get; set; }
            public List<RowViewModel> rows { get; set; }
            public List<WidgetViewModel> widgets { get; set; }
        }


        public class WidgetViewModel
        {
            public WidgetViewModel()
            {
                posts = new List<PostDetailsModel>();
            }

            public Guid widgetid { get; set; }
            public string title { get; set; }
            public int location { get; set; }
            public string moreUrl { get; set; }
            public string viewPath { get; set; }
            public List<PostDetailsModel> posts { get; set; }
            public List<TermModel> categories { get; set; }
            public List<TermModel> tags { get; set; }
            public bool returnPosts { get; set; }
            public bool returnCategories { get; set; }
        }

        #endregion
    }
}