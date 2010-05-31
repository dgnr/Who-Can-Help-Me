namespace WhoCanHelpMe.Web.Controllers.User
{
    #region Using Directives

    using System.Security.Authentication;
    using System.Web.Mvc;

    using MvcContrib;
    using MvcContrib.Filters;

    using WhoCanHelpMe.Domain.Contracts.Tasks;
    using WhoCanHelpMe.Web.Controllers.Home;
    using WhoCanHelpMe.Web.Controllers.User.Mappers.Contracts;
    using WhoCanHelpMe.Web.Controllers.User.ViewModels;

    #endregion

    public class UserController : BaseController
    {
        #region Constants and Fields

        private readonly IIdentityTasks identityTasks;

        private readonly ILoginPageViewModelMapper loginPageViewModelMapper;

        private readonly IRegisterPageViewModelMapper registerPageViewModelMapper;

        #endregion

        #region Constructors and Destructors

        public UserController(
            IIdentityTasks identityTasks, 
            ILoginPageViewModelMapper loginPageViewModelMapper, 
            IRegisterPageViewModelMapper registerPageViewModelMapper)
        {
            this.identityTasks = identityTasks;
            this.loginPageViewModelMapper = loginPageViewModelMapper;
            this.registerPageViewModelMapper = registerPageViewModelMapper;
        }

        #endregion

        #region Public Methods

        [ModelStateToTempData]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(LoginFormModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.identityTasks.Authenticate(formModel.EmailAddress, formModel.Password);

                    return this.BuildSuccessfulLoginRedirectResult(formModel.ReturnUrl);
                }
                catch (AuthenticationException ex)
                {
                    this.TempData.Add("Message", ex.Message);
                }
            }

            return this.RedirectToAction(x => x.Login(formModel.ReturnUrl));
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

            var viewModel = this.loginPageViewModelMapper.MapFrom(message, returnUrl);

            return this.View(viewModel);
        }

        [ModelStateToTempData]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationFormModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.identityTasks.Register(formModel.EmailAddress, formModel.Password);

                    return this.BuildSuccessfulLoginRedirectResult(formModel.ReturnUrl);
                }
                catch (AuthenticationException ex)
                {
                    this.TempData.Add("Message", ex.Message);
                }
            }

            return this.RedirectToAction(x => x.Register(formModel.ReturnUrl));
        }

        public ActionResult Register(string returnUrl)
        {
            if (this.identityTasks.IsSignedIn())
            {
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            var message = this.TempData["Message"] as string;

            var viewModel = this.registerPageViewModelMapper.MapFrom(message, returnUrl);

            return this.View(viewModel);
        }

        public ActionResult SignOut()
        {
            this.identityTasks.SignOut();

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        #endregion

        #region Methods

        private ActionResult BuildSuccessfulLoginRedirectResult(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        #endregion
    }
}