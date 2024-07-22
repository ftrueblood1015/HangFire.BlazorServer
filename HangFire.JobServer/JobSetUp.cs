using Hangfire;
using HangFire.JobServer.Jobs;

namespace HangFire.JobServer
{
    public class JobSetUp
    {

        public static void LoadJobs()
        {
            var recurringJobManage = new RecurringJobManager();

            recurringJobManage.AddOrUpdate<ImportHouseFromCsv>(ImportHouseFromCsv.JobName, ImportHouseFromCsv => ImportHouseFromCsv.RunJob(), ImportHouseFromCsv.CronExpress);
            recurringJobManage.AddOrUpdate<RandomScryfallCard>(RandomScryfallCard.JobName, RandomScryfallCard => RandomScryfallCard.RunJob(), RandomScryfallCard.CronExpress);
        }
    }
}