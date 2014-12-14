using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using SMTH.Models;

namespace SMTH.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username Field Empty")]
        [StringLength(50, ErrorMessage = "Entry too long")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Field Empty")]
        [StringLength(50, ErrorMessage = "Password too long")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    #region Services

    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);

        bool ChangePassword(string username, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        public bool ValidateUser(string username, string password)
        {
            if (String.IsNullOrEmpty(username))
                throw new ArgumentException("Value cannot be null or empty.", "userName");

            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");

            return Data.Users.DataAccess.ValidateUser(username, password);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {
                //Data.Users.DataAccess.UpdatePassword(username, newPassword);
                return true;
            }

            return false;
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            SessionWrapper.AbandonSession();
        }
    }

    #endregion
}