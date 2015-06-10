using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter_Sentiment_API_App.Models
{
    public class Tweet
    {
        public string TweetText { get; set; }
        public string TweetId { get; set; }
        public string CreatedAt { get; set; }
        public string TweetedBy { get; set; }
        public int RetweetCount { get; set; }
    }
}
