
namespace Core.Repositories
{
    public class SettingRepository : BaseRepository<Setting>
    {
        public SettingRepository()
        {
            base.Init("Setting", "Name,Value,Type");
        }
    }


    public class Setting : BaseModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Type { get; set; }
    }

}
