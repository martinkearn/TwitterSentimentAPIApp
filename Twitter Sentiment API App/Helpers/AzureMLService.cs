﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Twitter_Sentiment_API_App.Helpers
{
    public static class AzureMLService
    {
        private const string BaseUriSentiment = "https://api.datamarket.azure.com/data.ashx/amla/text-analytics/v1/GetSentiment?Text=";
        private const string BaseUriKeyPhrases = "https://api.datamarket.azure.com/data.ashx/amla/text-analytics/v1/GetKeyPhrases?Text=";

        public static async Task<float> GetSentiment(string Text, string AzureMarketplaceAccountKey)
        {
            try
            {
                dynamic data = await CallAzureAPI(BaseUriSentiment + HttpUtility.UrlEncode(Text), AzureMarketplaceAccountKey);
                if (data != null)
                {
                    return (float)data.Score;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static async Task<List<string>> GetKeyPhrases(string Text, string AzureMarketplaceAccountKey)
        {
            try
            {
                dynamic data = await CallAzureAPI(BaseUriKeyPhrases + HttpUtility.UrlEncode(Text), AzureMarketplaceAccountKey);
                var phrases = new List<string>();
                foreach (string keyPhrase in data.KeyPhrases)
                {
                    phrases.Add(keyPhrase);
                }
                return phrases;
            }
            catch
            {
                return null;
            }
        }

        private static async Task<dynamic> CallAzureAPI(string FullUri, string AzureMarketplaceAccountKey)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(FullUri);
                byte[] authArray = Encoding.ASCII.GetBytes("AccountKey:" + AzureMarketplaceAccountKey);
                var Auth = Convert.ToBase64String(authArray);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Auth);
                using (var response = await client.GetAsync(client.BaseAddress))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(result);
                        return data;
                    }
                }
            }
            return null;
        }
    }
}
