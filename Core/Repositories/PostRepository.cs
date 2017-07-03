
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {

        public PostRepository()
        {
            base.Init("Post", "Title,Detail,Photo,PostTypeId,IsInitial,ViewPath,Attachments,PostMetaValues,Author,Gallery,Widgets");
        }

        public IEnumerable<Post> GetByIds(Guid[] postids, bool b)
        {
           return GetAll(1000, 1, string.Format(" Id in ('{0}')",string.Join("','", postids)));
        }
       public Post GetObject(Guid id)
        {
            var p = "Select * from Post where Id=@Id and Status!=-100;";
            var pt = "Select * From PostTerm WHERE PostId=@Id and Status!=-100;";
            var t = "Select t.Id,t.Title,t.TaxonomyId From Term t inner join PostTerm pt on t.id=pt.TermId WHERE  " +
                    " pt.Status!=-100 and pt.PostId=@Id ;";
            using (var results = QueryMultiple(t+pt + p, new { id }))
            {
                var terms = results.Read<Term>().ToList();
                var postterms = results.Read<PostTerm>().Select(x=>Map(x,terms)).ToList();
                var post = results.Read<Post>().SingleOrDefault();
                if (post != null)
                {
                    post.PostTerms = postterms;
                    return post;
                }
                return null;
            }
        }

        private PostTerm Map(PostTerm pt,List<Term> terms )
        {
            pt.Term = terms.SingleOrDefault(x => x.Id == pt.TermId);
            return pt;
        }
        public IEnumerable<Post> GetByType(Guid posttypeid)
        {
            throw new NotImplementedException();
        }
      

        public IQueryable<Post> GetForGrid(int posttypeid, bool b, int termid)
        {
            throw new NotImplementedException();
        }

        public void DeleteWidget(PostWidget postWidget)
        {
            throw new NotImplementedException();
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

        public DateTime LastModified { get; set; }
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
    public class PostTerm:BaseModel
    {
        public Guid PostId { get; set; }
        public Guid TermId { get; set; }
        public int DisplayOrder { get; set; }
        public  Term Term { get; set; }
        public  Post Post { get; set; }
    }
  
}
