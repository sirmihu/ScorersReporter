using CsvHelper;
using System.Globalization;

namespace ScorersReporter.Services
{
    public class FileServices : IFileServices
    {
        public void WriteCSV<T>(List<T> records, string filePath)
        {
            //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            using (var writer = new StreamWriter(filePath))        //(filePath + "\\ScorersReport.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

    }
}
