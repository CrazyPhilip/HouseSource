using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HouseSource.ViewModels;

namespace HouseSource.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WriteFollowUpPage : ContentPage
    {
        public WriteFollowUpPage(string inquiryID)
        {
            InitializeComponent();

            BindingContext = new WriteFollowViewModel(inquiryID);
        }
    }
}