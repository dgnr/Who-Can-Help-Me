namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using Contracts;

    using Domain;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class CreateProfilePageViewModelBuilder :
        ICreateProfilePageViewModelBuilder
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public CreateProfilePageViewModelBuilder(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public CreateProfilePageViewModel Get()
        {
            var viewModel = new CreateProfilePageViewModel();
            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }

    }
}