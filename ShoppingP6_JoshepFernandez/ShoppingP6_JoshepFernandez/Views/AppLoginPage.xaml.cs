using Acr.UserDialogs;
using ShoppingP6_JoshepFernandez.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingP6_JoshepFernandez.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
    {

        UserViewModel va;

        public AppLoginPage()
        {
            InitializeComponent();

            this.BindingContext = va = new UserViewModel();

            TimerConexion();
        }

        private void CmdWatchPassword(object sender, ToggledEventArgs e)
        {
            if (SwWatchPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void BtnUserSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignUpPage());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            bool R = false;

            if (TxtUserName.Text != null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Checking User Data...");
                await Task.Delay(500);

                string u = TxtUserName.Text.Trim();
                string p = TxtPassword.Text.Trim();

                R = await va.UserAccessValidation(u, p);

                }
                catch (Exception)
                {

                    throw;
                }
                finally 
                { 
                    UserDialogs.Instance.HideLoading();
                }

                

                
            }
            else
            {
                
                await DisplayAlert("Validation Error", "Username and password are required", "OK");
                return;
            }

            if (R)
            {
                //await DisplayAlert(":)", "User OK", "OK");

                await Navigation.PushAsync(new ActionMenuPage());
                //TODO: mostrar la page de seleccion de acciones en el sisitema
            }
            else
            {
                if (await va.CheckConexion())
                {
                    await DisplayAlert(":(", "Incorrect Username or Pasword!", "OK");
                }
                else
                {
                    await DisplayAlert("Validation Error", "There is no connection to the database. Check your connection or contact an administrator", "OK");
                }
            }
        }

        private async void Showing_Conexion()
        {
            bool R = false;
            
            if (await va.CheckConexion())
            {
                BtnGaugeC.BackgroundColor = Color.Green;
            }
            else
            {
                BtnGaugeC.BackgroundColor= Color.Red;
            }
            await Task.Delay(500);
        }

        private void TimerConexion()
        {
            var Timer = TimeSpan.FromSeconds(0.5);

            Device.StartTimer(Timer, () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Showing_Conexion();
                });
                return true;
            });
        }
    }
}