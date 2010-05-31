namespace WhoCanHelpMe.Web.Controllers.User.Mappers.Contracts
{
    #region Using Directives

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface IRegisterPageViewModelMapper : IMapper<string, string, RegisterPageViewModel>
    {
    }
}