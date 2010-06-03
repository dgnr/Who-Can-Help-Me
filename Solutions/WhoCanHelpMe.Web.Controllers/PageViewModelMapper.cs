namespace WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using Framework.Mapper;

    using Shared.Mappers.Contracts;
    using Shared.ViewModels;

    #endregion

    public abstract class PageViewModelMapper<TInput, TOutput> : Mapper<TInput, TOutput>
        where TOutput : PageViewModel
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        protected PageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public override TOutput MapFrom(TInput input)
        {
            return this.pageViewModelBuilder.UpdateSiteProperties(base.MapFrom(input));
        }
    }
}