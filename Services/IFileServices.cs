namespace ScorersReporter.Services
{
    public interface IFileServices
    {
        void WriteCSV<T>(List<T> records, string filePath);
        IEnumerable<T> ReadCSV<T>(Stream filePath);
    }
}