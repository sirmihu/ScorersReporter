using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace ScorersReporter.Services
{
    public class FileServices : IFileServices
    {
        public void WriteCSV<T>(List<T> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
            var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<ScorersReporterClassMap>();

            var options = new TypeConverterOptions { Formats = new[] { "dd.MM.yyyy" } };
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

            var records = csv.GetRecords<T>();

            return records;

        }
    }
}
