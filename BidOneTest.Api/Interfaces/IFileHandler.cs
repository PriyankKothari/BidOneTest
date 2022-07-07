namespace BidOneTest.Api.Interfaces
{
    public interface IFileHandler
    {
        Task WriteFileAsync(string fileName, string fileContent);
    }
}