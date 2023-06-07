using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace FileServices
{
    public class FileService : IFileService
    {
        public void WriteCSV<T>(List<T> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        public IEnumerable<T> ReadCSV<T, TMapping>(Stream file)
            where TMapping : ClassMap
        {
            var reader = new StreamReader(file);
            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
            var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<TMapping>();

            var options = new TypeConverterOptions { Formats = new[] { "dd.MM.yyyy" } };
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

            var records = csv.GetRecords<T>();

            return records;

        }
    }
}
