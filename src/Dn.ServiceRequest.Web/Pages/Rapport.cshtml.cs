using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    using Volo.Abp.Identity;

    namespace Dn.ServiceRequest.Web.Pages;

[Authorize] 
    public class RapportModel : ServiceRequestPageModel
    {
      protected IdentityUserManager UserManager { get; }

        private readonly IIdentityUserRepository _userRepository;

        public string PasswordlessLoginUrl { get; set; }

        public string Email { get; set; }

 public RapportModel(IdentityUserManager userManager, IIdentityUserRepository userRepository)
        {
            UserManager = userManager;
            _userRepository = userRepository;
        }    public void OnGet()
    {

    }
   
}
