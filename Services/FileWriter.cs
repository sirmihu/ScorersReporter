using CsvHelper;
using System.Globalization;

namespace ScorersReporter.Services
{
    public class FileWriter
    {
        private readonly ReportFromDatabase _reportFromDatabase;
        public FileWriter(ReportFromDatabase reportFromDatabase)
        {
            _reportFromDatabase = reportFromDatabase;
        }

        public async void WriteCSV()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
 
            using (var writer = new StreamWriter(filePath + "\\ScorersReport.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var records = await _reportFromDatabase.DbReport();
                csv.WriteRecords(records);
            }
        }
    }
}
