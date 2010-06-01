namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    public interface IProfileQueryTasks
    {
        Profile GetProfileByUserName(string userName);

        Profile GetProfileById(int profileId);
    }
}