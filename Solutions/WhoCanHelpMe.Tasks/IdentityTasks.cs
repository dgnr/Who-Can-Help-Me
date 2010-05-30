using System;

namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Security.Authentication;
    using System.Web;
    using System.Web.Security;

    using Domain;
    using Domain.Contracts.Tasks;

    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.RelyingParty;

    #endregion

    public class IdentityTasks : IIdentityTasks
    {
        public void Authenticate(string userName, string password)
        {
            if (Membership.ValidateUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            else
            {
                throw new AuthenticationException("Unknown username or password.");
            }
        }

        public void Register(string userName, string password)
        {
            try
            {
                Membership.CreateUser(userName, password);
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            catch (MembershipCreateUserException ex)
            {
                throw new AuthenticationException(ex.Message);
            }
        }

        public Identity GetCurrentIdentity()
        {
            var identity = HttpContext.Current.User.Identity;

            if (!identity.IsAuthenticated)
            {
                return null;
            }

            return new Identity 
                   {
                        UserName = identity.Name
                   };
        }

        public bool IsSignedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}