using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NasaApiExplorer.Services
{
    /// <summary>
    /// Interface for downloading files from a web url.
    /// </summary>
    public interface IFileDownloadService
    {
        /// <summary>
        /// Writes a file to a folder chosen from the Folder Picker.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        Task DownloadFileAsync(string imageUrl);

        Task DownloadFiles(string[] imageUrls);
    }
}