namespace WhoCanHelpMe.Web.Controllers.User.Mappers
{
    #region Using Directives

    using Shared.Mappers.Contracts;

    using ViewModels;

    using WhoCanHelpMe.Framework.Mapper;

    #endregion

    public class RegisterPageViewModelMapper : IMapper<string, string, RegisterPageViewModel>
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