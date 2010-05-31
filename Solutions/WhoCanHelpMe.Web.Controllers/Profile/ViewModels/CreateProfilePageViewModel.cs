namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using Shared.ViewModels;

    #endregion

    public class CreateProfilePageViewModel : PageViewModel
    {
        public CreateProfilePageViewModel()
        {
            this.FormModel = new CreateProfileFormModel();
        }

        public CreateProfileFormModel FormModel { get; set; }
    }
}