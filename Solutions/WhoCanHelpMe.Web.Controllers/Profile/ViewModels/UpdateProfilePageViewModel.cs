namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Diagnostics;

    using Shared.ViewModels;

    #endregion

    [DebuggerDisplay("{FirstName} {LastName}")]
    public class UpdateProfilePageViewModel : PageViewModel
    {
        public UpdateProfilePageViewModel()
        {
            this.Assertions = new List<ProfileAssertionViewModel>();
            this.FormModel = new AddAssertionFormModel();
        }

        public IList<ProfileAssertionViewModel> Assertions { get; set; }

        public IList<CategoryViewModel> Categories { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AddAssertionFormModel FormModel { get; set; }
    }
}