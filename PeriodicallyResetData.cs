using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ModerationKitchen.Functions
{
    public class PeriodicallyResetData
    {
        [FunctionName("PeriodicallyResetData")]
        public async Task Run([TimerTrigger("0 0 0 * * *")]TimerInfo timerInfo, ILogger log)
//                                           | | | | | |
//                                           | | | | | --- Years   (Every year)
//                                           | | | | ----- Months  (Every month)
//                                           | | | ------- Days    (Every day)
//                                           | | --------- Hours   (Every 0th hour i.e., midnight)
//                                           | ----------- Minutes (Every 0th minute)
//                                           ------------- Seconds (Every 0th second)
        {
            log.LogInformation("Resetting data via the Moderation Kitchen web API");

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://moderation-kitchen-api.azurewebsites.net");
            await httpClient.PostAsync("/api/admin/recipes/reset-data", null);
        }
    }
}
