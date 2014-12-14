using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SMTH.Models;
using SMTH.ViewModels;
using SMTH.ViewModels.Users;
using SMTH.Data.Users;

namespace SMTH.Controllers
{
    public class UsersController : Controller
    {
        [AuthorizeUser]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [AuthorizeUser]
        public ActionResult UserPermissions()
        {
            return View(new UserPermissionsViewModel());
        }

        [AuthorizeUser]
        public ActionResult RegisterUser()
        {
            return View(new RegisterUserViewModel());
        }

        [AuthorizeUser]
        [HttpPost]
        public ActionResult RegisterUser(RegisterUserViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            DataAccess.AddUser(new UserDto { 
                UserName = model.UserName,
                Password = model.Password,
                UserId = Guid.NewGuid(),
                UserRole = model.UserRole
            });
            return RedirectToAction("Index");
        }

        #region login

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsService.SignOut();
            Session.Abandon();
            Session.Clear();

            return Redirect(Url.Action("Index", "Home"));
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                    ValidateUser(model.UserName, model.Password);

                if (ModelState.IsValid)
                {
                    SignIn(model.UserName);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (!ModelState.IsValid)
                System.Threading.Thread.Sleep(2000);

            return View(model);
        }

        private void SignIn(string username)
        {
            FormsService.SignIn(username, false);
            InitializeSession(username);
        }

        private void ValidateUser(string username, string password)
        {
            if (MembershipService.ValidateUser(username, password) == false)
            {
                ModelState.AddModelError(string.Empty, "Username or password incorrect");
            }
        }

        private void InitializeSession(string username)
        {
            Data.Users.UserDto user = Data.Users.DataAccess.GetUser(username);

            if (user == null)
                throw new Exception("User information not found in database.");

            SessionWrapper.UserUuid = user.UserId;
        }

        #endregion
    }
}