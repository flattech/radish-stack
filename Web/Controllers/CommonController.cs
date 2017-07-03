//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using Mentis.TV.Infrastructure.Core.Common.Infrastructure;
//using Mentis.Web.infrastructure.Cache;
//using Mentis.Web.Models;
//using Mentis.Web.Models.Common;
//using Mentis.Web.Models.UI.Paging;
//using Microsoft.Ajax.Utilities;
//using Microsoft.Owin.Logging;

//namespace Mentis.Web.Controllers
//{
//    public class CommonController : BasePublicController
//    {
//        #region Constructors

//        public CommonController(
//            ITermService categoryService,
//            IPostService productService,
//            ILanguageService languageService, ISitemapGenerator sitemapGenerator,
//            ILocalizationService localizationService,
//            IWorkContext workContext,
//            IWebHelper webHelper,
//            ICacheManager cacheManager,
//            StoreInformationSettings storeInformationSettings,
//            LocalizationSettings localizationSettings,
//            CommonSettings commonSettings,
//            IMenuService menuService, IRepository<Form> formRepository, 
//            )
//        {
            
//            _formRepository = formRepository;
//            _menuService = menuService;
//            _storeInformationSettings = storeInformationSettings;
//            _termService = categoryService;
//            _postService = productService;
//            _languageService = languageService;
//            _localizationService = localizationService;
//            _workContext = workContext;
//            _webHelper = webHelper;
//            _cacheManager = cacheManager;
//            _commonSettings = commonSettings;
//            _localizationSettings = localizationSettings;
//            _sitemapGenerator = sitemapGenerator;
//        }

//        #endregion

//        #region Fields

//        private readonly ITermService _termService;
//        private readonly IRepository<NewsLetterSubscription> _newsletterRepo;
//        private readonly IRepository<Form> _formRepository;
//        private readonly IMenuService _menuService;
//        private readonly IPostService _postService;
//        private readonly ILanguageService _languageService;
//        private readonly ILocalizationService _localizationService;
//        private readonly IWorkContext _workContext;
//        private readonly IWebHelper _webHelper;
//        private readonly ICacheManager _cacheManager;
//        private readonly CommonSettings _commonSettings;
//        private readonly LocalizationSettings _localizationSettings;
//        private readonly StoreInformationSettings _storeInformationSettings;
//        private readonly ISitemapGenerator _sitemapGenerator;

//        #endregion

//        #region Methods

//        //[HttpPost]
//        //public ActionResult SubmitNewsLetter(NewsLetterModel form)
//        //{
//        //    string result;
//        //    var success = false;
//        //    if (!CommonHelper.IsValidEmail(form.Email))
//        //    {
//        //        result = _localizationService.GetResource("Newsletter.Email.Wrong");
//        //    }
//        //    else
//        //    {
//        //        form.Email = form.Email.CleanSql().Trim();
//        //        try
//        //        {
//        //            _newsletterRepo.Insert(new NewsLetterSubscription
//        //            {
//        //                Name = form.Name,
//        //                Email = form.Email
//        //            });
//        //            var service = new EmailProcessor();
//        //            service.Subscribe(form.Email);
//        //            result = _localizationService.GetResource("Newsletter.SubscribeSuccessful");
//        //            success = true;
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            EngineContext.Current.Resolve<ILogger>().Error(ex.Message, ex);
//        //            result = "Error occured";
//        //        }
//        //    }
//        //    return Json(new
//        //    {
//        //        Success = success,
//        //        Result = result
//        //    });
//        //}

//        //page not found
//        [HttpPost]
//        public ActionResult SubmitForm(FormModel form)
//        {
//            string result;
//            var success = false;
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _formRepository.Insert(new Form
//                    {
//                        FirstName = form.FirstName,
//                        LastName = form.LastName,
//                        Media = form.Media,
//                        Message = form.Message,
//                        Email = form.Email,
//                        Phone = form.Phone,
//                        DateChoosen = form.DateChoosen,
//                        Type = form.Type,
//                        Gender = form.Gender,
//                        Status = 10
//                    });
//                    result = _localizationService.GetResource("FormSuccessful" + form.Type);
//                    success = true;
//                }
//                catch (Exception ex)
//                {
//                    var logger = EngineContext.Current.Resolve<ILogger>();
//                    logger.Error(ex.Message, ex);
//                    result = "Error occured";
//                }
//            }
//            else
//            {
//                result = "Form Not Valid";
//            }

//            return Json(new
//            {
//                Success = success,
//                Result = result
//            });
//        }

//        //page not found
//        public ActionResult PageNotFound()
//        {
//            Response.StatusCode = 404;
//            Response.TrySkipIisCustomErrors = true;

//            return View();
//        }

//        public ActionResult Search(string q, BasePageableModel command)
//        {
//            var model = new SearchModel { Query = q };

//            //ensure pge size is specified
//            if (command.PageSize <= 0)
//                command.PageSize = 6;

//            if (command.PageNumber <= 0)
//                command.PageNumber = 1;


//            var list = _postService.SearchPostsByKeywords(command.PageNumber - 1, command.PageSize, null, q,
//                _workContext.WorkingLanguage.Id);

//            model.PagingFilteringContext.LoadPagedList(list);
//            model.Posts = list.Select(x => x.ToModel(_storeInformationSettings.MediaLink)).ToList();
//            return View(model);
//        }

//        //language
//        [ChildActionOnly]
//        public ActionResult LanguageSelector()
//        {
//            var availableLanguages =
//                _cacheManager.Get(string.Format(ModelCacheEventConsumer.AVAILABLE_LANGUAGES_MODEL_KEY, "0"), () =>
//                {
//                    var result = _languageService
//                        .GetAllLanguages()
//                        .Select(x => new LanguageModel
//                        {
//                            Id = x.Id,
//                            Name = x.Name,
//                            FlagImageFileName = x.FlagImageFileName
//                        })
//                        .ToList();
//                    return result;
//                });

//            var model = new LanguageSelectorModel
//            {
//                CurrentLanguageId = _workContext.WorkingLanguage.Id,
//                AvailableLanguages = availableLanguages
//            };

//            if (model.AvailableLanguages.Count == 1)
//                Content("");

//            return PartialView(model);
//        }

//        [ChildActionOnly]
//        public ActionResult TopMenu()
//        {
//            //categories
//            var categoryCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_MENU_MODEL_KEY,
//                _workContext.WorkingLanguage.Id, string.Join(",", ""), "0");
//            var cachedCategoriesModel = _cacheManager.Get(categoryCacheKey, () => PrepareCategorySimpleModels());

//            var model = new TopMenuModel
//            {
//                Categories = cachedCategoriesModel
//            };
//            return PartialView(model);
//        }

//        [NonAction]
//        protected virtual IList<CategorySimpleModel> PrepareCategorySimpleModels(int? rootCategoryId = null,
//            bool loadSubCategories = true, IList<MenuItem> allCategories = null)
//        {
//            var result = new List<CategorySimpleModel>();

//            if (allCategories == null)
//            {
//                allCategories = _menuService.GetForHeader();
//            }
//            var categories = allCategories.Where(c => c.ParentId == rootCategoryId).ToList();
//            foreach (var category in categories)
//            {
//                var categoryModel = new CategorySimpleModel
//                {
//                    IsMega = category.IsMega,
//                    Id = category.Id,
//                    Url = category.Url,
//                    EntityId = category.EntityId,
//                    ParentId = category.ParentId,
//                    Name = category.GetLocalized(x => x.Title)
//                };
//                if (category.EntityId > 0)
//                {
//                    categoryModel.SeName = SeoExtensions.GetSeName(category.EntityId, category.EntityName,
//                        _workContext.WorkingLanguage.Id);

//                    if (category.EntityName == "Post" && category.EnableMedia)
//                    {
//                        var post = _postService.Get(category.EntityId).ToModel(_storeInformationSettings.MediaLink);
//                        categoryModel.Icon = post.GetMedia("Standard");
//                    }
//                }
//                else
//                {
//                    categoryModel.SeName = category.Url;
//                }
//                if (loadSubCategories)
//                {
//                    var subCategories = PrepareCategorySimpleModels(category.Id, loadSubCategories, allCategories);
//                    categoryModel.SubCategories.AddRange(subCategories);
//                }
//                result.Add(categoryModel);
//            }

//            return result;
//        }

//        public ActionResult SetLanguage(int langid, string returnUrl = "")
//        {
//            var language = _languageService.GetLanguageById(langid);
//            if (language != null && language.Published)
//            {
//                _workContext.WorkingLanguage = language;
//            }

//            //home page
//            if (String.IsNullOrEmpty(returnUrl))
//                returnUrl = Url.RouteUrl("HomePage");

//            //prevent open redirection attack
//            if (!Url.IsLocalUrl(returnUrl))
//                returnUrl = Url.RouteUrl("HomePage");

//            //language part in URL
//            if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
//            {
//                var applicationPath = HttpContext.Request.ApplicationPath;
//                if (returnUrl.IsLocalizedUrl(applicationPath, true))
//                {
//                    //already localized URL
//                    returnUrl = returnUrl.RemoveLanguageSeoCodeFromRawUrl(applicationPath);
//                }
//                returnUrl = returnUrl.AddLanguageSeoCodeToRawUrl(applicationPath, _workContext.WorkingLanguage);
//            }
//            return Redirect(returnUrl);
//        }


//        //header links
//        [ChildActionOnly]
//        public ActionResult HeaderLinks()
//        {
//            var model = new HeaderLinksModel();
//            return PartialView(model);
//        }

//        //footer
//        [ChildActionOnly]
//        public ActionResult Footer()
//        {
//            var categoryCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_MENU_MODEL_Footer_KEY,
//                _workContext.WorkingLanguage.Id);
//            var cachedCategoriesModel = _cacheManager.Get(categoryCacheKey, () => PrepareFooter());
//            var model = new TopMenuModel
//            {
//                Categories = cachedCategoriesModel
//            };
//            return PartialView(model);
//        }

//        [NonAction]
//        protected virtual IList<CategorySimpleModel> PrepareFooter()
//        {
//            var result = new List<CategorySimpleModel>();


//            var menus = _menuService.GetForFooter();

//            foreach (var category in menus)
//            {
//                var categoryModel = new CategorySimpleModel
//                {
//                    IsMega = category.IsMega,
//                    Id = category.Id,
//                    Url = category.Url,
//                    EntityId = category.EntityId,
//                    Name = category.GetLocalized(x => x.Title)
//                };
//                if (category.EntityId > 0)
//                    categoryModel.SeName = SeoExtensions.GetSeName(category.EntityId, category.EntityName,
//                        _workContext.WorkingLanguage.Id);
//                else
//                {
//                    categoryModel.SeName = category.Url;
//                }
//                result.Add(categoryModel);
//            }

//            return result;
//        }

//        //sitemap page
//        //[NopHttpsRequirement(SslRequirement.No)]
//        public ActionResult Sitemap()
//        {
//            if (!_commonSettings.SitemapEnabled)
//                return RedirectToRoute("HomePage");

//            var cacheKey = string.Format(ModelCacheEventConsumer.SITEMAP_PAGE_MODEL_KEY,
//                _workContext.WorkingLanguage.Id, "", "0");
//            var cachedModel = _cacheManager.Get(cacheKey, () =>
//            {
//                var model = new SitemapModel();
//                //categories
//                if (_commonSettings.SitemapIncludeCategories)
//                {
//                    var categories = _termService.GetAll();
//                    model.Categories = categories.Select(x => x.ToModel()).ToList();
//                }
//                ////products
//                if (_commonSettings.SitemapIncludeProducts)
//                {
//                    //limit product to 200 until paging is supported on this page
//                    var products = _postService.SearchPosts(200, 1, null, true);
//                    model.Products = products.Select(product => new PostDetailsModel
//                    {
//                        Id = product.Id,
//                        Title = product.GetLocalized(x => x.Title),
//                        ShortDescription = product.GetLocalized(x => x.ShortDescription),
//                        FullDescription = product.GetLocalized(x => x.Description),
//                        SeName = product.GetSeName()
//                    }).ToList();
//                }
//                return model;
//            });

//            return System.Web.UI.WebControls.View(cachedModel);
//        }

//        //SEO sitemap page
//        //[NopHttpsRequirement(SslRequirement.No)]
//        public ActionResult SitemapXml()
//        {
//            if (!_commonSettings.SitemapEnabled)
//                return RedirectToRoute("HomePage");

//            var cacheKey = string.Format(ModelCacheEventConsumer.SITEMAP_SEO_MODEL_KEY,
//                _workContext.WorkingLanguage.Id,
//                string.Join(",", ""),
//                "0");
//            var siteMap = _cacheManager.Get(cacheKey, () => _sitemapGenerator.Generate(Url));
//            return Content(siteMap, "text/xml");
//        }

//        //favicon
//        [ChildActionOnly]
//        public ActionResult Favicon()
//        {
//            //try loading a store specific favicon
//            var faviconFileName = string.Format("favicon-{0}.ico", "0");
//            var localFaviconPath = Path.Combine(Request.PhysicalApplicationPath, faviconFileName);
//            if (!System.IO.File.Exists(localFaviconPath))
//            {
//                //try loading a generic favicon
//                faviconFileName = "favicon.ico";
//                localFaviconPath = Path.Combine(Request.PhysicalApplicationPath, faviconFileName);
//                if (!System.IO.File.Exists(localFaviconPath))
//                {
//                    return Content("");
//                }
//            }

//            var model = new FaviconModel
//            {
//                FaviconUrl = _webHelper.GetStoreLocation() + faviconFileName
//            };
//            return PartialView(model);
//        }

//        //EU Cookie law
//        //[ChildActionOnly]
//        //public ActionResult EuCookieLaw()
//        //{
//        //    if (!_storeInformationSettings.DisplayEuCookieLawWarning)
//        //        //disabled
//        //        return Content("");


//        //    //ignore notification?
//        //    //right now it's used during logout so popup window is not displayed twice
//        //    if (TempData["nop.IgnoreEuCookieLawWarning"] != null && Convert.ToBoolean(TempData["nop.IgnoreEuCookieLawWarning"]))
//        //        return Content("");

//        //    return PartialView();
//        //}

//        //robots.txt file
//        public ActionResult RobotsTextFile()
//        {
//            var disallowPaths = new List<string>
//            {
//                "/bin/",
//                "/content/files/",
//                "/content/files/exportimport/",
//                "/country/getstatesbycountryid",
//                "/install",
//                "/setproductreviewhelpfulness"
//            };
//            var localizableDisallowPaths = new List<string>
//            {
//                "/post/edit/",
//                "/post/create/",
//                "/post/index",
//                "/term/index",
//                "/term/create",
//                "/term/edit"
//            };


//            const string newLine = "\r\n"; //Environment.NewLine
//            var sb = new StringBuilder();
//            sb.Append("User-agent: *");
//            sb.Append(newLine);
//            //sitemaps
//            if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
//            {
//                //URLs are localizable. Append SEO code
//                foreach (var language in _languageService.GetAllLanguages())
//                {
//                    sb.AppendFormat("Sitemap: {0}{1}/sitemap.xml", "", language.UniqueSeoCode);
//                    sb.Append(newLine);
//                }
//            }
//            else
//            {
//                //localizable paths (without SEO code)
//                sb.AppendFormat("Sitemap: {0}sitemap.xml", "");
//                sb.Append(newLine);
//            }

//            //usual paths
//            foreach (var path in disallowPaths)
//            {
//                sb.AppendFormat("Disallow: {0}", path);
//                sb.Append(newLine);
//            }
//            //localizable paths (without SEO code)
//            foreach (var path in localizableDisallowPaths)
//            {
//                sb.AppendFormat("Disallow: {0}", path);
//                sb.Append(newLine);
//            }
//            if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
//            {
//                //URLs are localizable. Append SEO code
//                foreach (var language in _languageService.GetAllLanguages())
//                {
//                    foreach (var path in localizableDisallowPaths)
//                    {
//                        sb.AppendFormat("Disallow: {0}{1}", language.UniqueSeoCode, path);
//                        sb.Append(newLine);
//                    }
//                }
//            }

//            Response.ContentType = "text/plain";
//            Response.Write(sb.ToString());
//            return null;
//        }

//        public ActionResult GenericUrl()
//        {
//            //seems that no entity was found
//            return InvokeHttp404();
//        }

//        [ChildActionOnly]
//        public ActionResult SearchBox()
//        {
//            var model = new SearchBoxModel();
//            return PartialView(model);
//        }

//        public ActionResult ClearCache()
//        {
//            var cacheManager = new MemoryCacheManager();
//            cacheManager.Clear();
//            return RedirectToAction("Index", "Home");
//        }

//        #endregion
//    }
//}