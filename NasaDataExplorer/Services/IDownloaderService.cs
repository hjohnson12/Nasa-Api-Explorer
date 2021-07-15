using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services
{
    public interface IDownloaderService
    {
        Task DownloadFileAsync(string url, string targetPathLocation);
        void DownloadFiles(string[] urls);
    }
}