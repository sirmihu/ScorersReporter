namespace NbpApiServices
{
    public interface INbpApiService
    {
        Task<decimal> GetRate();
    }
}