namespace WhoCanHelpMe.Web.Controllers.Shared.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Base view model for page views
    /// </summary>
    public class PageViewModel
    {
        public PageViewModel()
        {
            this.Scripts = new List<string>();
            this.Styles = new List<string>();
            this.MetaData = new MetaDataViewModel();
        }

        public MetaDataViewModel MetaData { get; set; }

        public string AnalyticsIdentifier { get; set; } 

        public IList<string> Scripts { get; set; }

        public string SiteVerification { get; set; }

        public IList<string> Styles { get; set; }

        public string WebTitle { get; set; }
    }
}