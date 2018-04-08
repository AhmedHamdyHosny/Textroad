using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Textroad.Models
{
    public class AccountModel
    {
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    //public class VerifyCodeViewModel
    //{
    //    [Required]
    //    public string Provider { get; set; }

    //    [Required]
    //    [Display(Name = "Code")]
    //    public string Code { get; set; }
    //    public string ReturnUrl { get; set; }

    //    [Display(Name = "Remember this browser?")]
    //    public bool RememberBrowser { get; set; }

    //    public bool RememberMe { get; set; }
    //}

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Resource))]
        public bool RememberMe { get; set; }

        public UserViewModel Login()
        {
            UserViewModel user = new UserViewModel() { UserName = this.Email, Password = this.Password };
            return user.Login();
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceName = "PasswordNotMatchValidationError", ErrorMessageResourceType  = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceName = "PasswordNotMatchValidationError", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }
    }
}