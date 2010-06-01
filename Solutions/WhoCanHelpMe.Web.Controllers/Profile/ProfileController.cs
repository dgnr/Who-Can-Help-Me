namespace WhoCanHelpMe.Web.Controllers.Profile
{
    #region Using Directives

    using System.Web.Mvc;

    using ActionFilters;

    using Domain.Contracts.Tasks;
    using Framework.Security;
    using Home;

    using Mappers.Contracts;

    using MvcContrib;
    using MvcContrib.Filters;

    using Shared.ActionResults;

    using ViewModels;

    #endregion

    public class ProfileController : BaseController
    {
        private readonly ICategoryTasks categoryTasks;

        private readonly ICreateProfilePageViewModelBuilder createProfilePageViewModelMapper;

        private readonly IIdentityService identityTasks;

        private readonly IProfilePageViewModelMapper profilePageViewModelMapper;

        private readonly IProfileTasks userTasks;

        public ProfileController(
            IIdentityService identityTasks,
            IProfileTasks userTasks,
            ICategoryTasks categoryTasks,
            IProfilePageViewModelMapper profilePageViewModelMapper,
            ICreateProfilePageViewModelBuilder createProfilePageViewModelMapper)
        {
            this.identityTasks = identityTasks;
            this.userTasks = userTasks;
            this.categoryTasks = categoryTasks;
            this.profilePageViewModelMapper = profilePageViewModelMapper;
            this.createProfilePageViewModelMapper = createProfilePageViewModelMapper;
        }

        [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [RequireNoExistingProfile("Profile", "Update")]
        public ActionResult Create()
        {
            var viewModel = this.createProfilePageViewModelMapper.Get();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        [RequireNoExistingProfile("Profile", "Update")]
        public ActionResult Create(CreateProfileFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var identity = this.identityTasks.GetCurrentIdentity();

                this.userTasks.CreateProfile(identity.UserName, formModel.FirstName, formModel.LastName);

                return this.RedirectToAction(x => x.Update());
            }

            return this.RedirectToAction(x => x.Create());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Delete()
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            this.userTasks.DeleteProfile(identity.UserName);

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult DeleteAssertion(int assertionId)
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            var user = this.userTasks.GetProfileByUserName(identity.UserName);

            this.userTasks.RemoveAssertion(
                user,
                assertionId);

            return this.RedirectToAction(x => x.Update());
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(x => x.Update());
        }

        [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Update()
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            var user = this.userTasks.GetProfileByUserName(identity.UserName);

            var categories = this.categoryTasks.GetAll();

            var viewModel = this.profilePageViewModelMapper.MapFrom(
                user,
                categories);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Update(AddAssertionFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var identity = this.identityTasks.GetCurrentIdentity();

                this.userTasks.AddAssertion(identity.UserName, formModel.CategoryId, formModel.TagName);
            }

            return this.RedirectToAction(x => x.Update());
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            var user = this.userTasks.GetProfileById(id);

            if (user != null)
            {
                var profileViewModel = this.profilePageViewModelMapper.MapFrom(user);

                return this.View(profileViewModel);
            }

            return new NotFoundResult();
        }
    }
}