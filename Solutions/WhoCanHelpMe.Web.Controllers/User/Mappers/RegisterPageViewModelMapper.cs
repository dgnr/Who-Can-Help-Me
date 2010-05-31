namespace WhoCanHelpMe.Web.Controllers.User.Mappers
{
    #region Using Directives

    using Contracts;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class RegisterPageViewModelMapper : IRegisterPageViewModelMapper
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public RegisterPageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public RegisterPageViewModel MapFrom(
            string message,
            string returnUrl)
        {
            var viewModel = new RegisterPageViewModel
                {
                    Message = message ?? string.Empty, 
                    FormModel = { ReturnUrl = returnUrl },
                };

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }
    }
}