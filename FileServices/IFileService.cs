using CsvHelper.Configuration;

namespace FileServices
{
    public interface IFileService
    {
        void WriteCSV<T>(List<T> records, string filePath);
        IEnumerable<T> ReadCSV<T, TMapping>(Stream file) where TMapping : ClassMap;
    }
}