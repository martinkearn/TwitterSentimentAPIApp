using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter_Sentiment_API_App.Models
{
    public class AnalysedTweet
    {
        public Tweet Tweet { get; set; }
        public double Score { get; set; }
        public List<string> KeyPhrases { get; set; }
    }
}
