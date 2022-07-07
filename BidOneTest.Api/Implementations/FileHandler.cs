using BidOneTest.Api.Interfaces;

namespace BidOneTest.Api.Implementations
{
    /// <summary>
    /// File Handler
    /// </summary>
    public class FileHandler : IFileHandler
    {
        /// <summary>
        /// Directory Path to create file
        /// </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directoryPath"></param>
        public FileHandler(string directoryPath)
        {
            // TODO: refactor this into a private method
            // create directory if it doesn't exist
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            // assign value to the property
            this.DirectoryPath = directoryPath;
        }

        /// <summary>
        /// Write file content to file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public async Task WriteFileAsync(string fileName, string fileContent)
        {
            try
            {
                await File.WriteAllTextAsync(Path.Combine(this.DirectoryPath, fileName), fileContent);
            }
            catch (Exception exception)
            {
                // log exception
                throw;
            }
        }
    }
}