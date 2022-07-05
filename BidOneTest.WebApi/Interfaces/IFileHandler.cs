namespace BidOneTest.WebApi.Interfaces
{
    public interface IFileHandler
    {
        Task WriteFileAsync(string fileName, string fileContent);
    }
}