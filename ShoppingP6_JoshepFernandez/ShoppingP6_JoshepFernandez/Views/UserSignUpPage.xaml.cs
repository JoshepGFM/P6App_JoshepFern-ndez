using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingP6_JoshepFernandez.ViewModels;

namespace ShoppingP6_JoshepFernandez.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSignUpPage : ContentPage
    {
        UserViewModel ViewModel;

        public UserSignUpPage()
        {
            InitializeComponent();

            //se agrega en bindingContext de la vista contra el viewModel.
            BindingContext = ViewModel = new UserViewModel();

            LoadUserRolesList();
            LoadCountryList();

        }

        private async void LoadUserRolesList()
        {
            PckUserRole.ItemsSource = await ViewModel.GetUserRolesList();
        }

        private async void LoadCountryList()
        {
            PckCountry.ItemsSource = await ViewModel.GetCountryList();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            //en este caso la llamada a la funcinalidad no sera por command
            //TODO: implementar Command

            bool R = await ViewModel.AddNewUser(TxtName.Text.Trim(),
                                                TxtEmail.Text.Trim(),
                                                TxtPassword.Text.Trim(),
                                                TxtEmailBackUP.Text.Trim(),
                                                TxtPhone.Text.Trim());
            if (R)
            {
                await DisplayAlert(":)", "Everything is OK", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(":(", "Something went wrong", "OK");
            }

        }
    }
}