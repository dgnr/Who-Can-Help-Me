using System;
using WhoCanHelpMe.Web.Controllers.User.ViewModels;

namespace WhoCanHelpMe.Web.Controllers.User
{
    #region Using Directives

    using System.Security.Authentication;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using Home;

    using Mappers.Contracts;

    using MvcContrib;
    using MvcContrib.Filters;

    #endregion

    public class UserController : BaseController
    {
        private readonly IIdentityTasks identityTasks;

        private readonly ILoginPageViewModelMapper loginPageViewModelMapper;

        public UserController(
            IIdentityTasks identityTasks,
            ILoginPageViewModelMapper loginPageViewModelMapper)
        {
            this.identityTasks = identityTasks;
            this.loginPageViewModelMapper = loginPageViewModelMapper;
        }

        [ModelStateToTempData]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(LoginFormModel loginFormModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.identityTasks.Authenticate(loginFormModel.EmailAddress, loginFormModel.Password);

                    return this.BuildSuccessfulLoginRedirectResult(loginFormModel.ReturnUrl);
                }
                catch (AuthenticationException ex)
                {
                    this.TempData.Add("Message", ex.Message);
                }
            }

            return this.RedirectToAction(x => x.Login(loginFormModel.ReturnUrl));
        }

        [ModelStateToTempData]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationFormModel registrationFormModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Validate the registrationFormModel, ensure that the two passwords are the same.
                    this.identityTasks.Register(registrationFormModel.EmailAddress, registrationFormModel.Password);

                    return this.BuildSuccessfulLoginRedirectResult(registrationFormModel.ReturnUrl);
                }
                catch (AuthenticationException ex)
                {
                    this.TempData.Add(
                        "Message",
                        ex.Message);
                }
            }

            return this.RedirectToAction(x => x.Login(registrationFormModel.ReturnUrl));
        }

        private ActionResult BuildSuccessfulLoginRedirectResult(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (this.identityTasks.IsSignedIn())
            {
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            return this.RedirectToAction(x => x.Login(string.Empty));
        }

        [ModelStateToTempData]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (this.identityTasks.IsSignedIn())
            {
                // Redirect to home page
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            var message = this.TempData["Message"] as string;

            var viewModel = this.loginPageViewModelMapper.MapFrom(
                message,
                returnUrl);

            return this.View(viewModel);
        }

        public ActionResult SignOut()
        {
            this.identityTasks.SignOut();

            return this.RedirectToAction<HomeController>(x => x.Index());
        }
    }
}