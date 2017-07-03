using System.Collections.Generic;
using System.IO;

namespace CMS
{
    public class AppResources
    {
        public static readonly AppResources Instance = new AppResources();

        private Dictionary<string, string> _dictionary;

        private AppResources()
        {
            Refresh();
        }
        public class ResourceRecord
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        public string Get(string key)
        {
            string value;
            if (_dictionary.TryGetValue(key, out value))
                return value;
            return key;
        }

        public void Refresh()
        {
            _dictionary = new Dictionary<string, string>();
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResourceRecord>>(File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/resource_en.json")));
            foreach (var l in list)
            {
                if (!_dictionary.ContainsKey(l.Key))
                    _dictionary.Add(l.Key, l.Value);
            }
        }
    }
}