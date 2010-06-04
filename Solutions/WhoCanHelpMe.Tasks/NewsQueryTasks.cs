namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;
    using Domain.Contracts.Services;
    using Domain.Contracts.Tasks;

    #endregion

    public class NewsQueryTasks : INewsQueryTasks
    {
        private readonly INewsService newsService;

        public NewsQueryTasks(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IList<NewsItem> GetProjectBuzz()
        {
            return this.newsService.GetHeadlines();
        }

        public IList<NewsItem> GetDevelopmentTeamBuzz()
        {
            return this.newsService.GetDevTeamHeadlines();
        }
    }
}