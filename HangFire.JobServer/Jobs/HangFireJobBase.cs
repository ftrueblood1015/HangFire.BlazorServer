using HangFire.Domain.Interfaces;

namespace HangFire.JobServer.Jobs
{
    public class HangFireJobBase : IHangFireJob
    {
        public string JobName { get; set; }

        public string CronExpress { get; set; }

        public HangFireJobBase(string name, string cronExpress)
        {
            JobName = name;
            CronExpress = cronExpress;
        }

        public virtual void RunJob()
        {
            throw new NotImplementedException();
        }
    }
}
