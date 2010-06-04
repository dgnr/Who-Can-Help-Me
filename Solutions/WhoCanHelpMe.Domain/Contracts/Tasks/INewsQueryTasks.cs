namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface INewsQueryTasks
    {
        IList<NewsItem> GetProjectBuzz();
        IList<NewsItem> GetDevelopmentTeamBuzz();
    }
}