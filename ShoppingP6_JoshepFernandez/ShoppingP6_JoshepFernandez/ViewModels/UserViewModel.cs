using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingP6_JoshepFernandez.Models;

namespace ShoppingP6_JoshepFernandez.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        public UserRole MyUserRole { get; set; }

        public Country MyCountry { get; set; }

        public User MyUser { get; set; }

        public UserViewModel()
        {
            MyUserRole = new UserRole();
            MyCountry = new Country();
            MyUser = new User();
        }

        public async Task<List<UserRole>> GetUserRolesList()
        {
            try
            {
                List<UserRole> list = new List<UserRole>();

                list = await MyUserRole.GetUserRoles();

                if (list == null)
                {
                    return null;
                }
                else
                {
                    return list;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Country>> GetCountryList()
        {
            try
            {
                List<Country> list = new List<Country>();

                list = await MyCountry.GetCountry();

                if (list == null)
                {
                    return null;
                }
                else
                {
                    return list;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //funcion para agregart usuario
        public async Task<bool> AddNewUser(string pName, 
                                           string pEmail, 
                                           string pPassword, 
                                           string pBkpEmail, 
                                           string pPhoneNumer, 
                                           int pUserRole, 
                                           int pContry)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser.Name = pName;
                MyUser.Email = pEmail;
                MyUser.BackUpEmail = pBkpEmail;
                MyUser.PhoneNumber = pPhoneNumer;
                MyUser.UserPassword =  pPassword;
                MyUser.IduserRole = pUserRole;
                MyUser.Idcountry =  pContry;

                bool R = await MyUser.AddUser();

                return R;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                IsBusy = false;
            }

        }


        //Funcion de validacion de ingreso de usuario al app
        public async Task<bool> UserAccessValidation(string pEmail, string pUserPassword)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser.Email = pEmail;
                MyUser.UserPassword = pUserPassword;

                bool R = await MyUser.ValidateLogin();

                return R;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally 
            { IsBusy = false; }


        }

        public async Task<bool> CheckConexion()
        {

            bool R = await MyUser.ValidateConexion();
            return R;

        }

    }
}
