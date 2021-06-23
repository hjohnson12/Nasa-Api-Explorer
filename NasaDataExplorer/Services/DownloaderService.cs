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

        public async Task DownloadF(string uri, string targetPath)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                uri);

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
            }
        }

        public async Task DownloadFileAsync(string url, string targetPathLocation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            byte[] buffer = await httpClient.GetByteArrayAsync(url);

            try
            {
                //File.WriteAllBytes(@"C:\users\hlj51\Desktop", buffer);
                //StorageFolder downloadsFolder =
                //    await StorageFolder.GetFolderFromPathAsync(UserDataPaths.GetDefault().Downloads);

                // Demo 1 - File picker
                StorageFolder photoFolderByDate;
                StorageFolder photoFolder;
                string todaysDate = $"{DateTime.Today.Month}-{DateTime.Today.Day}-{DateTime.Today.Year}";

                FolderPicker picker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
                picker.FileTypeFilter.Add("*");
                StorageFolder pickedFolder = await picker.PickSingleFolderAsync();

                if (pickedFolder != null)
                {
                    photoFolder = await pickedFolder.CreateFolderAsync("Mars Photos", CreationCollisionOption.OpenIfExists);
                    photoFolderByDate = await photoFolder.CreateFolderAsync(todaysDate, CreationCollisionOption.OpenIfExists);
                    StorageFile photoFile = await photoFolderByDate.CreateFileAsync(
                        Path.GetFileName(url), CreationCollisionOption.ReplaceExisting);

                    // Using a stream
                    using (Stream stream = await photoFile.OpenStreamForWriteAsync())
                        stream.Write(buffer, 0, buffer.Length); // Save
                }

                // Demo 2 - W/o file picker
                //photoFolderByDate = await folder.CreateFolderAsync(todaysDate, CreationCollisionOption.ReplaceExisting);
                //StorageFile photoFile = await photoFolderByDate.CreateFileAsync(
                //    Path.GetFileName(url), CreationCollisionOption.ReplaceExisting);
                ////await FileIO.WriteBytesAsync(photoFile, buffer);
                //// Using a stream
                //using (Stream stream = await photoFile.OpenStreamForWriteAsync())
                //    stream.Write(buffer, 0, buffer.Length); // Save

                //File.WriteAllBytes(UserDataPaths.GetDefault().Downloads, buffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
