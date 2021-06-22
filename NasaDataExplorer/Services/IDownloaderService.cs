﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataExplorer.Services
{
    public interface IDownloaderService
    {
        Task DownloadFile(string url, string targetPathLocation);
        void DownloadFiles(string[] urls);
    }
}
