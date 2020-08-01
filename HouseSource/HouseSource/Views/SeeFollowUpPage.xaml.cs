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
    public partial class SeeFollowUpPage : ContentPage
    {
        public SeeFollowUpPage(string id, bool isProperty)
        {
            InitializeComponent();
            
            BindingContext = new SeeFollowViewModel(id, isProperty);
        }
    }
}