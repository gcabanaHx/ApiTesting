using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.IO;

namespace APITesting
{
    public class HandleContent
    {

        #region https://www.youtube.com/watch?v=u1Fl1gqYAlk&list=PL_GzuQ9GmG0EYtFSyODKjFG0Ldvl178-u&index=6

        public static T getContent<T>(RestResponse response)
        {
            var _content = response.Content;
            return JsonConvert.DeserializeObject<T>(_content);
        }
        public static string serialize(dynamic payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.Indented);
        }
        public static T parseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }

 

        #endregion
    }
}
