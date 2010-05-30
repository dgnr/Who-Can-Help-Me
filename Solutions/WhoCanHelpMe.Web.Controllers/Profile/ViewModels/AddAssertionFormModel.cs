namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class AddAssertionFormModel
    {
        public int CategoryId { get; set; }

        [Required]
        public string TagName { get; set; }
    }
}