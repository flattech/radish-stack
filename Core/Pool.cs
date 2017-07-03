using Core.Repositories;

namespace Core
{
    public class Pool
    {
        public static readonly Pool Instance = new Pool();

        public TermRepository Terms { get; set; }
        public PostTypeRepository PostTypes { get; set; }
        public PostRepository Posts { get; set; }
        public PostTermRepository PostTerms { get; set; }
        public PostWidgetRepository PostWidgets { get; set; }
        public MenuRepository Menues { get; set; }
        public WidgetRepository Widgets { get; set; }
        public MediaRepository Medias { get; set; }
        public UrlRecordRepository UrlRecords { get; set; }
        public UserRepository Users { get; set; }

        private Pool()
        {
            PostTypes = new PostTypeRepository();
            PostWidgets = new PostWidgetRepository();
            Terms = new TermRepository();
            PostTerms = new PostTermRepository();
            Posts = new PostRepository();
            Menues = new MenuRepository();
            Widgets = new WidgetRepository();
            Medias = new MediaRepository();
            UrlRecords = new UrlRecordRepository();
            Users = new UserRepository();
        }

        public void Commit()
        {
        }
    }
}
