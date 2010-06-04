namespace WhoCanHelpMe.Web.Controllers.User.Mappers
{
    #region Using Directives

    using Shared.Mappers.Contracts;

    using ViewModels;

    using WhoCanHelpMe.Framework.Mapper;

    #endregion

    public class LoginPageViewModelMapper : IMapper<string, string, LoginPageViewModel>
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public LoginPageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public LoginPageViewModel MapFrom(
            string message,
            string returnUrl)
        {
            var viewModel = new LoginPageViewModel
                {
                    Message = message ?? string.Empty,
                    FormModel = { ReturnUrl = returnUrl },
                };

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }
    }
}