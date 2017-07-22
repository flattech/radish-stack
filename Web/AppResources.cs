using System.Collections.Generic;
using System.IO;

namespace Web
{
    public class AppsResources
    {
        public static readonly AppsResources Instance = new AppsResources();


        private Dictionary<string, string> _dictionary;

        private AppsResources()
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