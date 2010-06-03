namespace WhoCanHelpMe.Web.Controllers.Shared.Mappers.Contracts
{
    #region Using Directives

    using ViewModels;

    #endregion

    public interface IPageViewModelBuilder : IBuilder<PageViewModel>
    {
        T UpdateSiteProperties<T>(T pageViewModel) where T : PageViewModel;
    }
}