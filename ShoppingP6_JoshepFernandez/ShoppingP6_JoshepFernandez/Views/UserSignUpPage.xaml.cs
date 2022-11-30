using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingP6_JoshepFernandez.ViewModels;
using ShoppingP6_JoshepFernandez;
using ShoppingP6_JoshepFernandez.Models;

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

        private bool UserInputValidation()
        {
            bool R = false;

            if (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
                TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword .Text.Trim()) &&
                TxtEmailBackUP.Text != null && !string.IsNullOrEmpty(TxtEmailBackUP.Text.Trim()) &&
                TxtPhone.Text != null && !string.IsNullOrEmpty(TxtPhone.Text.Trim()) &&
                PckUserRole.SelectedIndex > -1 && PckCountry.SelectedIndex > -1
                )
            {
                R = true;
            }
            else
            {
                if (TxtName.Text == null || string.IsNullOrEmpty(TxtName.Text.Trim()))
                {
                    DisplayAlert("Validation Error", "Name is Required!", "OK");
                    TxtName.Focus();
                    return false;
                }

                if (TxtEmail.Text == null || string.IsNullOrEmpty(TxtEmail.Text.Trim()))
                {
                    DisplayAlert("Validation Error", "Email is Required!", "OK");
                    TxtEmail.Focus();
                    return false;
                }
                if (TxtPassword.Text == null || string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    DisplayAlert("Validation Error", "Password is Required!", "OK");
                    TxtPassword.Focus();
                    return false;
                }

                if (TxtEmailBackUP.Text == null || string.IsNullOrEmpty(TxtEmailBackUP.Text.Trim()))
                {
                    DisplayAlert("Validation Error", "Email Back up is Required!", "OK");
                    TxtEmailBackUP.Focus();
                    return false;
                }

                if (TxtPhone.Text == null || string.IsNullOrEmpty(TxtPhone.Text.Trim()))
                {
                    DisplayAlert("Validation Error", "Phone is Required!", "OK");
                    TxtPhone.Focus();
                    return false;
                }

                if (PckUserRole.SelectedIndex == -1)
                {
                    PckUserRole.Focus();
                    DisplayAlert("Validation Error", "Rol is Required!", "OK");
                    return false;
                }

                if (PckCountry.SelectedIndex == -1)
                {
                    PckCountry.Focus();
                    DisplayAlert("Validation Error", "Country is Required!", "OK");
                    return false;
                }
            }

            return R;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            //en este caso la llamada a la funcinalidad no sera por command

            if (UserInputValidation())
            {



                //seleccionar el ID del Rol de Usuario
                
                    UserRole UserRol = PckUserRole.SelectedItem as UserRole;
                    int IdRol = UserRol.IduserRole;
                
                //Seleccionar el ID de Pais
                
                    Country country = PckCountry.SelectedItem as Country;
                    int IdCountry = country.Idcountry;
                

                //pedir configuracion de la accion a realizar

                var answer = await DisplayAlert("Confirmation required!", "Are you sure?", "Yes", "Nop");

                if (answer)
                {
                    bool R = await ViewModel.AddNewUser(TxtName.Text.Trim(),
                                                    TxtEmail.Text.Trim(),
                                                    TxtPassword.Text.Trim(),
                                                    TxtEmailBackUP.Text.Trim(),
                                                    TxtPhone.Text.Trim(),
                                                    IdRol, IdCountry);
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
    }
}