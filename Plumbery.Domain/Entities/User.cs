using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// User class to store user properties inherits IdentityUser
    /// </summary>
    public partial class User : IdentityUser {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager) {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FullName {
            get {
                return LastName + ", " + FirstName;
            }
        }
        /// <summary>
        /// The Date the User Registered for general information purposes
        /// </summary>
        public DateTime DateRegistered { get; set; }
    }
}
