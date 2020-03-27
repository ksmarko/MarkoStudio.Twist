using System.Collections.Generic;

namespace MarkoStudio.Twist.TwitterApi
{
    public static class Configuration
    {
        public static List<string> GetParameterStoreParams()
        {
            return new List<string>
            {
                "Twist_Twitter_ConsumerKey",
                "Twist_Twitter_ConsumerSecret"
            };
        }
    }
}
