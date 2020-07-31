using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HouseSource.Services
{
    public interface IImagePickerService
    {
        Task<List<string>> PickImageAsync();
    }
}
