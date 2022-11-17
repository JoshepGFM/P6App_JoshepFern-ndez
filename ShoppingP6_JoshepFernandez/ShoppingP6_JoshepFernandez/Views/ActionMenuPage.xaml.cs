using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingP6_JoshepFernandez.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionMenuPage : ContentPage
    {
        public ActionMenuPage()
        {
            InitializeComponent();

        }

        private async void BtnUserConfig_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserManagmentPage());
        }
    }
}