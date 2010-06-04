namespace WhoCanHelpMe.Web.Controllers.Shared.Mappers
{
    #region Using directives

    using System.Collections.Generic;

    using WhoCanHelpMe.Domain.Contracts.Configuration;
    using WhoCanHelpMe.Web.Controllers.Shared.Mappers.Contracts;
    using WhoCanHelpMe.Web.Controllers.Shared.ViewModels;

    #endregion

    public class PageViewModelBuilder : IPageViewModelBuilder
    {
        #region Constants and Fields

        private readonly IConfigurationService configurationService;

        #endregion

        #region Constructors and Destructors

        public PageViewModelBuilder(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        #endregion

        #region Implemented Interfaces

        #region IPageViewModelBuilder

        public PageViewModel Get()
        {
            var viewModel = new PageViewModel();

            return this.UpdateSiteProperties(viewModel);
        }

        public T UpdateSiteProperties<T>(T pageViewModel) where T : PageViewModel
        {
            pageViewModel.AnalyticsIdentifier = this.configurationService.Analytics.Idenfitier;
            pageViewModel.Scripts = GetScripts();
            pageViewModel.SiteVerification = this.configurationService.Analytics.Verification;
            pageViewModel.Styles = GetStyles();
            pageViewModel.WebTitle = "Who Can Help Me?";

            return pageViewModel;
        }

        #endregion

        #endregion

        #region Methods

        private static IList<string> GetScripts()
        {
            var scripts = new List<string>
                {
                    "jquery-1.4.1.min.js", 
                    "jquery.autocomplete.custom.js", 
                    "bespoke.js", 
                    "MicrosoftAjax.js", 
                    "MicrosoftMvcValidation.js"
                };

            return scripts;
        }

        private static IList<string> GetStyles()
        {
            var styles = new List<string> { "reset.css", "jquery.autocomplete.css", "openid.css", "site.less" };

            return styles;
        }

        #endregion
    }
}