using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace NasaApiExplorer.Services
{
    /// <summary>
    /// Class for downloading image files from web urls
    /// </summary>
    public class FileDownloadService : IFileDownloadService
    {
        private HttpClient _httpClient;
        private IFolderService _folderService;

        public FileDownloadService(HttpClient client, IFolderService folderService)
        {
            _folderService = folderService;
            _httpClient = client;
        }

        /// <summary>
        /// Writes an image to a folder chosen from the Folder Picker.
        /// </summary>
        /// <param name="url">Url of image file</param>
        /// <returns></returns>
        public async Task DownloadFileAsync(string imageUrl)
        {
            var pickedFolder = await _folderService.OpenFolderPickerAsync();
            var photoFolder = await _folderService.CreateFolderByNameAsync(pickedFolder, "Mars Rover Photos");

            if (photoFolder != null)
            {
                await WriteToFile(photoFolder, imageUrl);
            }
        }

        /// <summary>
        /// Downloads all files to a folder specified from a folder picker shown
        /// to the user.
        /// </summary>
        /// <param name="imageUrls"></param>
        /// <returns></returns>
        public async Task DownloadFilesAsync(string[] imageUrls)
        {
            var pickedFolder = await _folderService.OpenFolderPickerAsync();
            var photoFolder = await _folderService.CreateFolderByNameAsync(pickedFolder, "Mars Rover Photos");

            if (photoFolder != null)
            {
                foreach (var url in imageUrls)
                {
                    await WriteToFile(photoFolder, url);
                }
            }
        }

        /// <summary>
        /// Writes a file in the given url to a specified folder
        /// </summary>
        /// <param name="photoFolder">Folder to save file in</param>
        /// <param name="url">Url of file</param>
        /// <returns></returns>
        private async Task WriteToFile(StorageFolder photoFolder, string url)
        {
            // Get byte array buffer of file to save
            byte[] buffer = await _httpClient.GetByteArrayAsync(url);

            StorageFile photoFile = await photoFolder.CreateFileAsync(
                Path.GetFileName(url),
                CreationCollisionOption.ReplaceExisting);

            using (Stream stream = await photoFile.OpenStreamForWriteAsync())
            {
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}