namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    public interface IIdentityTasks
    {
        Identity GetCurrentIdentity();

        void SignOut();

        bool IsSignedIn();

        void Authenticate(string userName, string password);
        
        void Register(string userName, string password);
    }
}