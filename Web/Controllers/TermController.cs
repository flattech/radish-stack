//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using Mentis.Web.infrastructure.Cache;
//using Mentis.Web.Models;

//namespace Mentis.Web.Controllers
//{
//    public class TermController : BasePublicController
//    {
//        #region Fields

//        private readonly ITermService _termService;
//        private readonly IPostService _postService;
//        private readonly IWorkContext _workContext;
//        private readonly StoreInformationSettings _settings;
//        private readonly ICacheManager _cacheManager;

//        #endregion

//        #region Constructors

//        public TermController(ITermService termService,
//            IWorkContext workContext,
//           StoreInformationSettings settings,
//            ICacheManager cacheManager, IPostService postService)
//        {
//            this._termService = termService;
//            this._settings = settings;
//            this._workContext = workContext;
//            this._cacheManager = cacheManager;
//            _postService = postService;
//        }
//        #endregion
//        //[NopHttpsRequirement(SslRequirement.No)]
//        public ActionResult Index(int termid, BasePageableModel command)
//        {
//            var cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_HOMEPAGE_KEY,
//                _workContext.WorkingLanguage.Id, termid, command.PageNumber, command.PageSize);

//            var cachedModel = _cacheManager.Get(cacheKey, () => GetModel(termid, command));

//            if (cachedModel == null)
//                return InvokeHttp404();

//            return View(cachedModel.PostType.TermViewPath ?? "Index", cachedModel);
//        }

//        [NonAction]
//        protected virtual TermModel GetModel(int termid, BasePageableModel command)
//        {
//            var category = _termService.Get(termid);
//            if (category == null)
//                return null;
//            TermModel model = category.ToModel();
//            var posttype = category.PostType;
//            model.PostType = posttype;
//            var terms = _termService.GetByPostType(posttype.Id, 0);
//            model.Categories =
//                terms.Where(x => !x.IsTag())
//                    .OrderBy(x => x.DisplayOrder)
//                    .Select(x => x.ToMiniModel())
//                    .Where(x => !string.IsNullOrEmpty(x.Name))
//                    .ToList();
//            model.Tags =
//                terms.Where(x => x.IsTag())
//                    .Select(x => x.ToMiniModel())
//                    .Where(x => !string.IsNullOrEmpty(x.Name))
//                    .ToList();

//            var categoryIds = new List<int>();
//            categoryIds.Add(category.Id);

//            //include subcategories
//            //categoryIds.AddRange(GetChildCategoryIds(category.Id));
//            categoryIds.AddRange(GetChildCategoryIds(category.Id));

//            //ensure pge size is specified
//            if (command.PageSize <= 0)
//                command.PageSize = 6;

//            if (command.PageNumber <= 0)
//                command.PageNumber = 1;


//            var list = _postService.SearchPosts(command.PageNumber - 1, command.PageSize, categoryIds, true);

//            model.PagingFilteringContext.LoadPagedList(list);
//            model.Posts = list.Select(x => x.ToModel(_settings.MediaLink)).ToList();
//            model.RecentPosts =
//                _postService.GetByType(category.PostType.Id, true).Take(4).Select(x => x.ToModel()).ToList();
//            return model;
//        }

//        [NonAction]
//        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
//        {
//            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_CHILD_IDENTIFIERS_MODEL_KEY,
//                parentCategoryId, string.Join(",", 0), 0);
//            return _cacheManager.Get(cacheKey, () =>
//            {
//                var categoriesIds = new List<int>();
//                var categories = _termService.GetAllTermsByParentCategoryId(parentCategoryId);
//                foreach (var category in categories)
//                {
//                    categoriesIds.Add(category.Id);
//                    categoriesIds.AddRange(GetChildCategoryIds(category.Id));
//                }
//                return categoriesIds;
//            });
//        }

//        [NonAction]
//        protected virtual IList<CategorySimpleModel> PrepareCategorySimpleModels(int? rootCategoryId = null,
//             bool loadSubCategories = true, IList<Term> allCategories = null, int? postTypeId = null)
//        {
//            var result = new List<CategorySimpleModel>();

//            if (allCategories == null)
//            {
//                allCategories = _termService.GetByPostType(postTypeId ?? 0, (int)TermTypeEnum.Category);
//            }
//            var categories = allCategories.Where(c => c.ParentId == rootCategoryId).ToList();
//            foreach (var category in categories)
//            {
//                var categoryModel = new CategorySimpleModel
//                {
//                    Id = category.Id,
//                    Name = category.GetLocalized(x => x.Title),
//                    SeName = category.GetSeName(),
//                    // IncludeInTopMenu = category.IncludeInTopMenu
//                };
//                if (loadSubCategories)
//                {
//                    var subCategories = PrepareCategorySimpleModels(category.Id, loadSubCategories, allCategories);
//                    categoryModel.SubCategories.AddRange(subCategories);
//                }
//                result.Add(categoryModel);
//            }

//            return result;
//        }

//        [ChildActionOnly]
//        public ActionResult TermNavigation(int currentCategoryId)
//        {
//            //get active category
//            int activeCategoryId = 0;
//            if (currentCategoryId > 0)
//            {
//                //category details page
//                activeCategoryId = currentCategoryId;
//            }
//            var category = _termService.Get(currentCategoryId);
//            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_NAVIGATION_MODEL_KEY,
//                _workContext.WorkingLanguage.Id, string.Join(",", ""), "0");
//            var cachedModel = _cacheManager.Get(cacheKey, () => PrepareCategorySimpleModels(null, true, null, category == null ? (int?)null : category.PostTypeId).ToList());

//            var model = new CategoryNavigationModel
//            {
//                CurrentCategoryId = activeCategoryId,
//                Categories = cachedModel
//            };

//            return PartialView(model);
//        }


//    }
//}