using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Plumbery.Domain.Entities;
using Plumbery.Infrastructure.Data.Configuration;
using Plumbery.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Plumbery.Infrastructure.Data.Configuration {
    /// <summary>
    /// EMail service for email confirmation and password recovery
    /// </summary>
    public class EmailService : IIdentityMessageService {
        /// <summary>
        /// Send EMail with configured service
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <returns></returns>
        public async Task SendAsync(IdentityMessage message) {
            MailMessage email = new MailMessage(
                new MailAddress("theplumbery.email.service@gmail.com", "(do not reply)"),
                new MailAddress(message.Destination)
            );
            email.Subject = message.Subject;
            email.Body = message.Body;
            email.IsBodyHtml = true;            
            using (var mailClient = new HostKingService()) {
                //In order to use the original from email address, uncomment this line:
                email.From = new MailAddress(mailClient.UserName, "(do not reply)");

                await mailClient.SendMailAsync(email);
            }
        }
        public async Task SendAsync(IdentityMessage message, string AttachmentPath) {
            MailMessage email = new MailMessage(
                new MailAddress("admin@plumbery.org.za", "(do not reply)"),
                new MailAddress(message.Destination)
            );
            email.Subject = message.Subject;
            email.Body = message.Body;
            email.IsBodyHtml = true;
            var attachement = new Attachment(AttachmentPath);
            email.Attachments.Add(attachement);
                using (var mailClient = new HostKingService()) {
                    //In order to use the original from email address, uncomment this line:
                    email.From = new MailAddress(mailClient.UserName, "(do not reply)");

                    await mailClient.SendMailAsync(email);
                }
        }
    }
    /// <summary>
    /// Gmail service to support EmailService
    /// </summary>
    public class HostKingService : SmtpClient {
        /// <summary>
        /// Gmail username for email service
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Configuration for Gmail
        /// </summary>
        public HostKingService():base(ConfigurationManager.AppSettings["HostKingHost"], Int32.Parse(ConfigurationManager.AppSettings["HostKingPort"])) {
            this.UserName = ConfigurationManager.AppSettings["HostKingUserName"];
            this.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["HostKingSsl"]);
            this.UseDefaultCredentials = false;
            this.Credentials = new System.Net.NetworkCredential(this.UserName, ConfigurationManager.AppSettings["HostKingPassword"]);
        }
    }
    /*---------NOT IMPLEMENTED---------------------*/
    //public class SmsService : IIdentityMessageService {
    //    public Task SendAsync(IdentityMessage message) {
    //        // Plug in your SMS service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}

    /// <summary>
    /// User Manager handling user tasks such as login and register
    /// </summary>
    public class UserManager : UserManager<User> {
        
        /// <summary>
        /// Initialise userstore in constructor
        /// </summary>
        /// <param name="store"></param>
        public UserManager(IUserStore<User> store)
            : base(store) {
        }

        public static async Task<bool> SendEmailAttachment(string email, string body, string subject, string AttachmentPath) {
            EmailService myService = new EmailService();
            IdentityMessage IMessage = new IdentityMessage();
            IMessage.Body = body;
            IMessage.Subject = subject;
            IMessage.Destination = email;
            await myService.SendAsync(IMessage, AttachmentPath);
            return true;
        }

        /// <summary>
        /// Create the UserManager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context) {
            var manager = new UserManager(new UserStore<User>(context.Get<ContextBank>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager) {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User> {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User> {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            /*------NOT IMPLEMENTED----------*/
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null) {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    /// <summary>
    /// SignInManager for user services
    /// </summary>
    public class SignInManager : SignInManager<User, string> {
        /// <summary>
        /// COnstructor initialising UserManager class and Authentication Manager
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) {
        }
        /// <summary>
        /// Create The User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user) {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }
        /// <summary>
        /// Create the sign in manager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static SignInManager Create(IdentityFactoryOptions<SignInManager> options, IOwinContext context) {
            return new SignInManager(context.GetUserManager<UserManager>(), context.Authentication);
        }
    }
}
