using HtmlAgilityPack;
using RA.TaskSchedulerServices;

namespace RA.TaskSchedulerServicesImp
{
    public class ScrapingService : IScrapingService
    {
        public async Task<string> GetResponse(string url)
        {
            HttpClient client = new();
            var response = await client.GetStringAsync(url);

            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(response);

            var head = htmlDoc.DocumentNode.Descendants("head").ToList();
            if (head.Count > 0)
            {
                return head[0].InnerHtml.Normalize();
            }
             return response[..1000];
        }
    }
}
