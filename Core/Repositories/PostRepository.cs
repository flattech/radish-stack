
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Repositories.Enums;

namespace Core.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {

        public PostRepository()
        {
            base.Init("Post", "Title,Detail,Photo,PostTypeId,IsInitial," +
                              "ViewPath,Attachments,PostMetaValues,Author,Gallery,Widgets,PublishedDate,UrlKey");
        }

        public IEnumerable<Post> GetByIds(Guid[] postids, bool b)
        {
            return GetAll(1000, 1, string.Format(" Id in ('{0}')", string.Join("','", postids)));
        }

        public Post GetPage(Guid id)
        {
            var post = Get(id);
            if (string.IsNullOrEmpty(post.Widgets))
                post.PostWidgets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PostWidget>>(post.Widgets);
            return post;
        }

        public Post GetObject(Guid id)
        {
            var qp = $"Select * FROM [Post] WHERE Id='{id}';";
            var qpt = $"Select * FROM [PostTerm] WHERE PostId='{id}' AND Status!=-100;";
            var qt = "Select t.* FROM Term t INNER JOIN [PostTerm] pt " +
                     $"ON t.id=pt.TermId WHERE pt.Status!=-100 and pt.PostId='{id}' ;";

            var qptype = "Select pt.* FROM [PostType] pt INNER JOIN [Post] p " +
                           $"ON pt.id=p.PostTypeId WHERE p.Id ='{id}' ;";

            using (var results = QueryMultiple(qptype + qt + qpt + qp))
            {
                var type = results.Read<PostType>().SingleOrDefault();
                var terms = results.Read<Term>().ToList();
                var postterms = results.Read<PostTerm>().Select(x => Map(x, terms)).ToList();
                var post = results.Read<Post>().SingleOrDefault();
                if (post == null) return null;
                post.PostTerms = postterms;
                post.PostType = type;
                return post;
            }
        }
        public IList<Post> GetPosts(PostSearch critiria, int page, int size = 12)
        {
            string where = "WHERE p.Status!=-100 ";
            string order = "p.PublishedDate desc ";

            if (critiria.PostTypeId.HasValue)
                where += $"AND p.PostTypeId='{critiria.PostTypeId.Value}' ";
            if (critiria.Status != PostSatusEnum.NotSet)
                where += $"AND p.Status={(int)critiria.Status} ";
            if (!string.IsNullOrEmpty(critiria.Order))
                order = $" {critiria.Order} ";

            var qpids = "SELECT p.Id FROM [Post] p ";
            if (critiria.PostIds != null && critiria.PostIds.Any())
                qpids += $"{where} AND p.Id IN ('{string.Join("','", critiria.PostIds)}') ";
            else if (critiria.Terms != null && critiria.Terms.Any())
                qpids += "INNER JOIN [PostTerm] pt ON p.id=pt.PostId  " +
                        $"{where} AND pt.TermId IN ('{string.Join("','", critiria.Terms)}') ";
            else
                qpids += $"{where} ";

            var qpposts = $"SELECT * FROM [Post] p WHERE p.Id IN ({qpids}) ORDER BY {order};";
            var qppostterm = $"SELECT * FROM [PostTerm] pt WHERE pt.PostId IN ({qpids}) AND pt.Status!=-100;";
            var qterm = "SELECT t.* FROM [Term] t inner join [PostTerm] " +
                    $"pt ON t.id=pt.TermId WHERE pt.Status!=-100 and pt.PostId IN ({qpids}) ;";

            var qposttype = "SELECT DISTINCT pt.* FROM [PostType] pt INNER JOIN Post p " +
                $" ON pt.Id=p.PostTypeId WHERE p.Id IN ({qpids}) ;";

            using (var results = QueryMultiple(qposttype + qterm + qppostterm + qpposts))
            {
                var posttypes = results.Read<PostType>().ToList();
                var terms = results.Read<Term>().ToList();
                var postterms = results.Read<PostTerm>().Select(x => Map(x, terms)).ToList();
                return results.Read<Post>().Select(x => Map(x, posttypes, postterms)).ToList();
            }
        }

        private Post Map(Post p, List<PostType> pts, List<PostTerm> terms)
        {
            p.PostTerms = terms.Where(x => x.PostId == p.Id).ToList();
            p.PostType = pts.SingleOrDefault(x => x.Id == p.PostTypeId);
            return p;
        }

        private PostTerm Map(PostTerm pt, List<Term> postterms)
        {
            pt.Term = postterms.FirstOrDefault(x => x.Id == pt.TermId);
            return pt;
        }

    }

    public class PostTermRepository : BaseRepository<PostTerm>
    {
        public PostTermRepository()
        {
            base.Init("PostTerm", "DisplayOrder,PostId,TermId");
        }
    }
    public class Post : BaseModel
    {
        public Post()
        {
            this.PostMetas = new List<PostMeta>();
            this.PostTerms = new List<PostTerm>();
            this.PostWidgets = new List<PostWidget>();
        }

        public DateTime PublishedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string UrlKey { get; set; }
        public string Title { get; set; }
        public string Widgets { get; set; }
        public string Photo { get; set; }
        public string Gallery { get; set; }
        public string Author { get; set; }
        public string Detail { get; set; }
        public Guid PostTypeId { get; set; }
        public bool IsInitial { get; set; }
        public string ViewPath { get; set; }
        public string Attachments { get; set; }
        public string PostMetaValues { get; set; }

        public PostType PostType { get; set; }
        public List<PostMeta> PostMetas { get; set; }
        public List<PostTerm> PostTerms { get; set; }
        public List<PostWidget> PostWidgets { get; set; }
        public Media FeaturedImage { get; set; }
        public string TemplateView { get; set; }
        public bool IsActive { get; set; }
    }

    public class PostSearch
    {
        public PostSearch()
        {
            Status = PostSatusEnum.NotSet;
        }
        public Guid? PostTypeId { get; set; }
        public PostSatusEnum Status { get; set; }
        public Guid[] PostIds { get; set; }
        public Guid[] Terms { get; set; }
        public string Order { get; set; }
    }
    public class PostAttachment
    {
        public PostAttachment(string key)
        {
            this.Key = key;
        }

        public string Key { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public List<PostAttachment> PostAttachments { get; set; }
    }
    public class PostFields
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class PostMeta : BaseModel
    {
        public int PostId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
        public Post Post { get; set; }
    }
    public class PostTerm : BaseModel
    {
        public Guid PostId { get; set; }
        public Guid TermId { get; set; }
        public int DisplayOrder { get; set; }
        public Term Term { get; set; }
        public Post Post { get; set; }
    }

}
