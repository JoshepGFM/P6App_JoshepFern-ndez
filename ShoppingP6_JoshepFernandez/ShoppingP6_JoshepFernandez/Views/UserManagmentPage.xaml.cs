using ShoppingP6_JoshepFernandez.Models;
using ShoppingP6_JoshepFernandez.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ShoppingP6_JoshepFernandez.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserManagmentPage : ContentPage
	{
        UserViewModel ViewModel;
        public UserManagmentPage ()
		{
			InitializeComponent ();

            BindingContext= ViewModel = new UserViewModel ();

            LoadUserRolesList();
            LoadCountryList();

            //llenar los campos con la data del usuario global
            LoadUserData();
		}

        private void LoadUserData()
        {
            TxtName.Text = GlobalObjects.GlobalUser.Nombre;
            TxtEmail.Text = GlobalObjects.GlobalUser.CorreoElectronico;
            TxtEmailBackUP.Text = GlobalObjects.GlobalUser.CorreoRespaldo;
            TxtPhone.Text = GlobalObjects.GlobalUser.NumeroTelefono;

            
        }

        private async void LoadUserRolesList()
        {
            PckUserRole.ItemsSource = await ViewModel.GetUserRolesList();
        }

        private async void LoadCountryList()
        {
            PckCountry.ItemsSource = await ViewModel.GetCountryList();
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnApply_Clicked(object sender, EventArgs e)
        {

        }
    }
}