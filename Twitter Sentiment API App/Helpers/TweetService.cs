using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Twitter_Sentiment_API_App.Helpers
{
    public static class TweetService
    {
        private const string TwitterDateFormat = "ddd MMM dd HH:mm:ss zzzz yyyy";
        public static DateTime GetDate(string TwitterDate)
        {
            return DateTime.ParseExact(TwitterDate, TwitterDateFormat, CultureInfo.InvariantCulture);
        }
    }
}