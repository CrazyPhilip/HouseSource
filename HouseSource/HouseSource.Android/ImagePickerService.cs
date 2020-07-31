using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using HouseSource.Droid;
using HouseSource.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImagePickerService))]
namespace HouseSource.Droid
{
    public class ImagePickerService : IImagePickerService
    {
        public Task<List<string>> PickImageAsync()
        {
            // Define the Intent for getting images
            //Intent intent = new Intent();
            //intent.SetType("image/*");
            //intent.SetAction(Intent.ActionGetContent);

            Intent intent = new Intent(Intent.ActionPick);
            intent.SetType("image/*");
            intent.PutExtra(Intent.ExtraAllowMultiple, true);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Photo"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<List<string>>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }

    }
}