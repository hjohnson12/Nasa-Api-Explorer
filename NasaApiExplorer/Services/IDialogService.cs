using NasaApiExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApiExplorer.Services
{
    public interface IDialogService
    {
        Task ShowPhotoDialog(MarsRoverPhoto marsRoverPhoto, IList<MarsRoverPhoto> photos);
    }
}
