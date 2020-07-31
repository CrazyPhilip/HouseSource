using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;
using Rg.Plugins.Popup;
using UltimateXF.Droid;
using Xamarin.Forms;
using Android.Content;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Database;
using System;
using Android.Provider;

namespace HouseSource.Droid
{
    [Activity(Label = "68独立经纪人版", Icon = "@mipmap/erp_logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Popup.Init(this, savedInstanceState);   //弹出框
            CarouselViewRenderer.Init();    //轮播图
            CachedImageRenderer.Init(true);
            UltimateXFSettup.Initialize(this);
            Forms.SetFlags( new string[] { "RadioButton_Experimental", "Expander_Experimental" });

            base.OnCreate(savedInstanceState);
            Instance = this;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CachedImageRenderer.InitImageViewHandler();

            LoadApplication(new App());
        }


        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        //public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        public TaskCompletionSource<List<string>> PickImageTaskCompletionSource { set; get; }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            List<string> mediaPicked = null;

            if ((resultCode == Result.Ok) && (intent != null))
            {
                //Android.Net.Uri uri = intent.Data;
                //Stream stream = ContentResolver.OpenInputStream(uri);
                //
                //// Set the Stream as the completion of the Task
                //PickImageTaskCompletionSource.SetResult(stream);
                //mediaPicked = new IList<Stream>();

                mediaPicked = new List<string>();

                ClipData clipData = intent.ClipData;
                if (clipData != null)
                {
                    for (int i = 0; i < clipData.ItemCount; i++)
                    {
                        ClipData.Item item = clipData.GetItemAt(i);
                        //Android.Net.Uri uri = item.Uri;
                        //Stream stream = ContentResolver.OpenInputStream(uri);
                        //if (stream != null)
                        //{
                        //    mediaPicked.Add(stream);
                        //}
                        mediaPicked.Add(getPath(item.Uri));
                        ///external/images/media/424869
                    }
                }

                PickImageTaskCompletionSource.SetResult(mediaPicked);
                mediaPicked = new List<string>();
            }
            else
            {
                PickImageTaskCompletionSource.SetResult(null);
            }
        }

        private string getPath(Android.Net.Uri uri)
        {
            string path = null;
            ICursor cursor = ContentResolver.Query(uri, null, null, null, null);
            if (cursor == null)
            {
                return null;
            }
            if (cursor.MoveToFirst())
            {
                try
                {
                    path = cursor.GetString(cursor.GetColumnIndex(MediaStore.MediaColumns.Data));
                }
                catch (Exception)
                {
                }
            }
            cursor.Close();
            return path;
        }
    }
}