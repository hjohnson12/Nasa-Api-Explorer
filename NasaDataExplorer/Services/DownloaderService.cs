using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace NasaDataExplorer.Services
{
    public class DownloaderService : IDownloaderService
    {
        private IHttpClientFactory _httpClientFactory;

        public DownloaderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task DownloadFileAsync(string url, string targetPathLocation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            byte[] buffer = await httpClient.GetByteArrayAsync(url);

            // Demo 1 - File picker
            StorageFolder photoFolderByDate;
            StorageFolder photoFolder;
            string todaysDate = $"{DateTime.Today.Month}-{DateTime.Today.Day}-{DateTime.Today.Year}";

            FolderPicker picker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            StorageFolder pickedFolder = await picker.PickSingleFolderAsync();

            if (pickedFolder != null)
            {
                photoFolder = await pickedFolder.CreateFolderAsync("Mars Photos", CreationCollisionOption.OpenIfExists);
                photoFolderByDate = await photoFolder.CreateFolderAsync(todaysDate, CreationCollisionOption.OpenIfExists);
                StorageFile photoFile = await photoFolderByDate.CreateFileAsync(
                    Path.GetFileName(url), CreationCollisionOption.ReplaceExisting);

                using (Stream stream = await photoFile.OpenStreamForWriteAsync())
                    stream.Write(buffer, 0, buffer.Length);
            }
        }

        public async Task DownloadFileWithoutPickerAsync(string url, string targetPath)
        {
            // Demo 2 Test - W/o file picker
            // UWP is sandboxed - might not be able to without picker
            var httpClient = _httpClientFactory.CreateClient();
            byte[] buffer = await httpClient.GetByteArrayAsync(url);
            string todaysDate = $"{DateTime.Today.Month}-{DateTime.Today.Day}-{DateTime.Today.Year}";

            //File.WriteAllBytes(@"C:\users\hlj51\Desktop", buffer);
            //StorageFolder downloadsFolder =
            //    await StorageFolder.GetFolderFromPathAsync(UserDataPaths.GetDefault().Downloads);
            StorageFolder photoFolder =
                 await StorageFolder.GetFolderFromPathAsync(UserDataPaths.GetDefault().Downloads);

            StorageFolder photoFolderByDate = 
                await photoFolder.CreateFolderAsync(todaysDate, CreationCollisionOption.ReplaceExisting);

            StorageFile photoFile = 
                await photoFolderByDate.CreateFileAsync(
                    Path.GetFileName(url), CreationCollisionOption.ReplaceExisting);

            //await FileIO.WriteBytesAsync(photoFile, buffer);

            // Using a stream
            using (Stream stream = await photoFile.OpenStreamForWriteAsync())
                stream.Write(buffer, 0, buffer.Length); // Save

            // Test
            //File.WriteAllBytes(UserDataPaths.GetDefault().Downloads, buffer);
        }

        public void DownloadFiles(string[] urls)
        {
            throw new NotImplementedException();
        }

        public async Task Test(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            byte[] buffer = await httpClient.GetByteArrayAsync(url);

            File.WriteAllBytes(@"C:\", buffer);
        }
    }
}
