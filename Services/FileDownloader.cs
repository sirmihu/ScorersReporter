using Microsoft.AspNetCore.Mvc;

namespace ScorersReporter.Services
{
    public class FileDownloader
    {
        private readonly string filePath;
        public FileDownloader(string filePath)
        {
            this.filePath = filePath;
        }
        public FileContentResult DownloadFile()
        {
            var data = System.IO.File.ReadAllBytes(filePath);
            var result = new FileContentResult(data, "application/csv")
            {
                FileDownloadName = "Scorers.csv"
            };
            return result;

        }
    }
}
