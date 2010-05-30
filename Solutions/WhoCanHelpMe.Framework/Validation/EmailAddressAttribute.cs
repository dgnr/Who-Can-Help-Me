namespace WhoCanHelpMe.Framework.Validation
{
    using System.ComponentModel.DataAnnotations;

    public class EmailAddressAttribute : RegularExpressionAttribute
    {
        public EmailAddressAttribute()
            : base(@"^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$")
        {
        }
    }
}