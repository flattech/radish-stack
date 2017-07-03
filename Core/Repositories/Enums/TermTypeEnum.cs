namespace Core.Repositories.Enums
{
    public enum WidgetLocationEnum : int
    {
        NotSet = -1,
        OneColumnTop = 10,
        TwoColumnMiddle = 20,
        TwoColumnMiddleSidebar = 30,
        OneColumnMiddleBottom = 40
    }
    public enum FormTypeEnum : int
    {

        None = 0,
        Appointment = 10,
        Request = 20,
        Career = 30,
    }
    public enum MenuTypeEnum : int
    {
       
        None = 0,
        CustomLink = 10,
        Post = 30,
        Term = 40
    }
    public enum TermTypeEnum : int
    {
        NotSet = -1,
        Category = 10,
        Tag = 20
    }
 
}