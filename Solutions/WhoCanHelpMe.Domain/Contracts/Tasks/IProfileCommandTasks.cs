namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    public interface IProfileCommandTasks
    {
        void RemoveAssertion(Profile profile, int assertionId);

        void AddAssertion(string userName, int categoryId, string tagName);

        void CreateProfile(string userName, string firstName, string lastName);

        void DeleteProfile(string userId);
    }
}