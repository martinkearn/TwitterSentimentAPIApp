using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Twitter_Sentiment_API_App.Helpers;
using Twitter_Sentiment_API_App.Models;

namespace Twitter_Sentiment_API_App.Controllers
{
    public class SentimentController : ApiController
    {

        /// <summary>
        /// Receives a tweet and returns it with additional analysis data from the Azure ML text analysis service
        /// </summary>
        /// <param name="Tweet">
        /// JSON representing a tweet from the Azure Logic App Twitter Connector
        /// {
        ///     "TweetText":"fibble",
        ///     "TweetId":"87",
        ///     "CreatedAt":"10/9/2014 9:45:06 PM",
        ///     "TweetedBy":"martinkearn",
        ///     "RetweetCount": 2
        /// }
        /// </param>
        /// <param name="AzureMarketplaceAccountKey">
        /// String containing your personal account key. You can get the account key from https://datamarket.azure.com/account/keys. You will need to add an application and subscribe to the 'Machine Learning Text Analytics Service' from the Azure marketplace
        /// </param>
        /// <returns>A tweet with additional analysis data from the Azure ML text analysis service</returns>
        public async Task<AnalysedTweet> Post(Tweet Tweet, string AzureMarketplaceAccountKey)
        {
            var keyPhrases = await AzureMLService.GetKeyPhrases(Tweet.TweetText, AzureMarketplaceAccountKey);
            var score = await AzureMLService.GetSentiment(Tweet.TweetText, AzureMarketplaceAccountKey);

            return new AnalysedTweet()
            {
                Tweet = Tweet,
                KeyPhrases = keyPhrases,
                Score = score,
                AllKeyPhrases  = string.Join(",", keyPhrases.ToArray())
            };
        }

    }
}
