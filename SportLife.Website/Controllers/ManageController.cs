using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SportLife.Models.IdentityModels;
using SportLife.Website.Models;

namespace SportLife.Website.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        #region Constructors and properties
        private SignInManager _signInManager;
        private MyUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController( MyUserManager userManager, SignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public MyUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.EditProfileSuccess ? "Success!"
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var userId = User.Identity.GetUserId<int>();
            var user = await UserManager.FindByIdAsync(userId);
            
            var model = new IndexViewModel
            {
                FirstName = user.UserFirstName,
                Surname = user.UserSurname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(model);
        }

       //
        // POST: /Manage/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile ( IndexViewModel viewModel ) {
            var message = ManageMessageId.Error;
            if ( ModelState.IsValid ) {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if ( user.Email != viewModel.Email )
                    UserManager.SetEmail(user.Id, viewModel.Email);

                if ( user.PhoneNumber != viewModel.PhoneNumber )
                    UserManager.SetPhoneNumber(user.Id, viewModel.PhoneNumber);

                user.UserFirstName = viewModel.FirstName;
                user.UserSurname = viewModel.Surname;

                UserManager.Update(user);
                message = ManageMessageId.EditProfileSuccess;
            } else {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Index", message);
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId<int>(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

       //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword ( ChangePasswordViewModel model ) {
            if ( !ModelState.IsValid ) {
                return View(model);
            }
            var result =
                await
                    UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
            if ( result.Succeeded ) {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if ( user != null ) {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
            }
            AddErrors(result);
            return View(model);
        }

       protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

    #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            EditProfileSuccess,
            Error
        }

#endregion
    }
}