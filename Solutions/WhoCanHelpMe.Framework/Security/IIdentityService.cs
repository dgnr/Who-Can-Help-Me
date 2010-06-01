namespace WhoCanHelpMe.Framework.Security
{
    public interface IIdentityService
    {
        Identity GetCurrentIdentity();

        void SignOut();

        bool IsSignedIn();

        void Authenticate(string userName, string password);
        
        void Register(string userName, string password);
    }
}