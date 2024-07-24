using Hangfire;
using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Services;
using CsvHelper;
using System.Globalization;

namespace HangFire.JobServer.Jobs
{
    public class ImportHouseFromCsv : HangFireJobBase
    {
        private readonly IHouseService _houseService;

        public static new string JobName = "Import House From Csv";

        public static new string CronExpress = Cron.Minutely();

        public ImportHouseFromCsv(IHouseService houseService) : base(JobName, CronExpress)
        {
            _houseService = houseService;
        }

        public override void RunJob()
        {
            Console.WriteLine($"{JobName} - {DateTime.Now}");

            try
            {
                IEnumerable<string> files = GetAllFiles();

                files.ToList().ForEach(file => { BackgroundJob.Enqueue(() => ParseCsvFile(file)); });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{JobName} Failed: {ex}");
            }
        }

        public void ParseCsvFile(string filePath)
        {
            try
            {
                List<House> records;
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<House>().ToList();
                }

                SaveAllRecords(records);
                BackgroundJob.Enqueue(() => ArchiveAndDeleteFile(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{JobName} Failed: {ex}");
            }
        }

        public void SaveAllRecords(List<House> records)
        {
            try
            {
                foreach (var record in records)
                {
                    BackgroundJob.Enqueue(() => _houseService.Add(record));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{JobName} Failed: {ex}");
            }
        }

        public void ArchiveAndDeleteFile(string filePath)
        {
            BackgroundJob.Enqueue(() => ArchiveFile(filePath));
        }

        public void ArchiveFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            File.Copy(file.FullName, $"c:\\temp\\HangFireCsvFiles\\Homes\\Archive\\{file.Name}_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            BackgroundJob.Enqueue(() => DeleteFile(filePath));
        }

        public void DeleteFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            file.Delete();
        }

        public IEnumerable<string> GetAllFiles()
        {
            return Directory.GetFiles("c:\\temp\\HangFireCsvFiles\\Homes", "*.csv");
        }
    }
}
