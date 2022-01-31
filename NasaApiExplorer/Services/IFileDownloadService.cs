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

        /// <summary>
        /// Downloads all files to a folder specified from a folder picker shown
        /// to the user.
        /// </summary>
        /// <param name="imageUrls"></param>
        /// <returns></returns>
        Task DownloadFilesAsync(string[] imageUrls);
    }
}