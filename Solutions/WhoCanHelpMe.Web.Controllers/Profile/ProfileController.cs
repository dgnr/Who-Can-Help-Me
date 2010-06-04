namespace WhoCanHelpMe.Web.Controllers.Profile
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using ActionFilters;

    using Domain.Contracts.Tasks;
    using Framework.Security;
    using Home;

    using MvcContrib;
    using MvcContrib.Filters;

    using Shared.ActionResults;

    using ViewModels;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;

    #endregion

    public class ProfileController : BaseController
    {
        private readonly ICategoryQueryTasks categoryTasks;

        private readonly IBuilder<CreateProfilePageViewModel> createProfilePageViewModelMapper;

        private readonly IIdentityService identityTasks;

        private readonly IMapper<Profile, IList<Category>, UpdateProfilePageViewModel> updateProfilePageViewModelMapper;
        
        private readonly IMapper<Profile, ProfilePageViewModel> profilePageViewModelMapper;

        private readonly IProfileQueryTasks profileQueryTasks;
        
        private readonly IProfileCommandTasks profileCommandTasks;
        
        public ProfileController(
            IIdentityService identityTasks,
            IProfileQueryTasks profileQueryTasks,
            IProfileCommandTasks profileCommandTasks,
            ICategoryQueryTasks categoryTasks,
            IMapper<Profile, IList<Category>, UpdateProfilePageViewModel> updateProfilePageViewModelMapper,
            IBuilder<CreateProfilePageViewModel> createProfilePageViewModelMapper,
            IMapper<Profile, ProfilePageViewModel> profilePageViewModelMapper)
        {
            this.identityTasks = identityTasks;
            this.profilePageViewModelMapper = profilePageViewModelMapper;
            this.profileQueryTasks = profileQueryTasks;
            this.profileCommandTasks = profileCommandTasks;
            this.categoryTasks = categoryTasks;
            this.updateProfilePageViewModelMapper = updateProfilePageViewModelMapper;
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

                this.profileCommandTasks.CreateProfile(identity.UserName, formModel.FirstName, formModel.LastName);

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

            this.profileCommandTasks.DeleteProfile(identity.UserName);

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult DeleteAssertion(int assertionId)
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            var user = this.profileQueryTasks.GetProfileByUserName(identity.UserName);

            this.profileCommandTasks.RemoveAssertion(
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

            var user = this.profileQueryTasks.GetProfileByUserName(identity.UserName);

            var categories = this.categoryTasks.GetAll();

            var viewModel = this.updateProfilePageViewModelMapper.MapFrom(
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

                this.profileCommandTasks.AddAssertion(identity.UserName, formModel.CategoryId, formModel.TagName);
            }

            return this.RedirectToAction(x => x.Update());
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            var user = this.profileQueryTasks.GetProfileById(id);

            if (user != null)
            {
                var profileViewModel = this.profilePageViewModelMapper.MapFrom(user);

                return this.View(profileViewModel);
            }

            return new NotFoundResult();
        }
    }
}