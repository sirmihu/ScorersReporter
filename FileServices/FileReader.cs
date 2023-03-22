using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace ScorersReporter.FileServices
{
    public class FileReader
    {
        public IEnumerable<T> ReadCsv<T>(Stream file)
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
