using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    using Volo.Abp.Identity;

    namespace Dn.ServiceRequest.Web.Pages;

[Authorize] 
    public class IndexModel : ServiceRequestPageModel
    {
      protected IdentityUserManager UserManager { get; }

        private readonly IIdentityUserRepository _userRepository;

        public string PasswordlessLoginUrl { get; set; }

        public string Email { get; set; }

 public IndexModel(IdentityUserManager userManager, IIdentityUserRepository userRepository)
        {
            UserManager = userManager;
            _userRepository = userRepository;
        }    public void OnGet()
    {

    }
      //added for passwordless authentication
      public async Task OnPostGeneratePasswordlessTokenAsync()
{
    var adminUser = await _userRepository.FindByNormalizedUserNameAsync("admin");

    var token = await UserManager.GenerateUserTokenAsync(
        adminUser, 
        "PasswordlessLoginProvider",
        "passwordless-auth"
    );

    PasswordlessLoginUrl = Url.Action(
        "Login", 
        "Passwordless",
        new { token = token, userId = adminUser.Id.ToString() },
        Request.Scheme
    );

    // Pas besoin de return, la page reste la même
}

}
