using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Volo.Abp.Identity;

namespace Dn.ServiceRequest.Web.Pages.Account
{
    public class Login : PageModel
    {
        private readonly ILogger<Login> _logger;
        [BindProperty]
          public LoginViewModel LoginModel { get; set; }
        protected IdentityUserManager _UserManager{get;}
        private readonly IIdentityUserRepository _userRepository;
        public string Message { get; set; }

        public string PasswordlessLoginUrl{get;set;}


        public Login(ILogger<Login> logger,IIdentityUserRepository userRepository,IdentityUserManager UserManager)
        {
            _logger = logger;
            _userRepository=userRepository;
            _UserManager=UserManager;
        }
           public void OnGet()
        {
             LoginModel = new LoginViewModel();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {               
                var adminUser=await _userRepository.FindByNormalizedUserNameAsync(LoginModel.Username);
                if(adminUser!=null){
               //  LdapConnection connection = new LdapConnection("cd.ebsafrica.com");
               // NetworkCredential credential = new NetworkCredential(LoginModel.Username, LoginModel.Password);
               // connection.Credential = credential;
               // connection.Bind();
               //
                var token=await _UserManager.GenerateUserTokenAsync(adminUser,"PasswordlessLoginProvider","passwordless-auth");
                PasswordlessLoginUrl=Url.Action("Login","Passwordless",new{token=token,userId=adminUser.Id.ToString()},Request.Scheme);
                  Message="L'utilisateur existe";

                   return Redirect(PasswordlessLoginUrl);
               }else {
                      Message="L'utilisateur n'existe pas";

                      return Page();
                 }

            }
             catch (LdapException lexc)
            {
                String error = lexc.ServerErrorMessage;
              //  Console.WriteLine(lexc);
               Message="Nom d'utilisateur ou Mot de passe Incorrect";
                return Page();
            }
            catch(Exception ex){
               string error =ex.Message;
                 Message="Nom d'utilisateur ou Mot de passe Incorrect";
            }

         return null;
        // Use the result to create an appropriate action result (e.g., ContentResult)
          //  return Content("ok", "text/plain");
        }
   
    }
     public class LoginViewModel
    {
        [Required]
        [StringLength(256)]
        [DisplayName("Nom d'utilisateur")]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
         [DisplayName("Mot de passe")]
        public string Password { get; set; }

    }
}