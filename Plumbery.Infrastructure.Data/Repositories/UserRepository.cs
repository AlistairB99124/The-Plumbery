﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace Plumbery.Infrastructure.Data.Repositories {
    /// <summary>
    /// Class for User functionailty
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository {
        /// <summary>
        /// Confirm email of User
        /// </summary>
        /// <param name="userManager">User Manager to handle confirmation</param>
        /// <param name="userId">String user Id of the User</param>
        /// <param name="code">User code sent in Email</param>
        /// <returns>Identity Result</returns>
        public async Task<IdentityResult> ConfirmEmail(UserManager<User, string> userManager, string userId, string code) =>
            await userManager.ConfirmEmailAsync(userId, code);

        public void ExternalLogin() {
            throw new NotImplementedException();
        }

        public void ExternalLoginCallback() {
            throw new NotImplementedException();
        }

        public void ExternalLoginConfirmation() {
            throw new NotImplementedException();
        }

        public void ForgotPassword() {
            throw new NotImplementedException();
        }

        public void ForgotPasswordConfirmation() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get user based on their Email
        /// </summary>
        /// <param name="Email">Email of user</param>
        /// <returns>User</returns>
        public User GetUser(string Email) =>
            _context.Users.Where(p => p.Email == Email).FirstOrDefault();
        /// <summary>
        /// Get User by their GUID
        /// </summary>
        /// <param name="ID">GUID</param>
        /// <returns>User</returns>
        public User GetUserById(string ID) => 
            _context.Users.Find(ID);
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="signinManager">Signin Manager to handle login</param>
        /// <param name="email">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <param name="rememberMe">Boolean if the user wants to be remembered</param>
        /// <returns>SignIn Status</returns>
        public async Task<SignInStatus> Login(SignInManager<User, string> signinManager, string email, string password, bool rememberMe) =>
            await signinManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        /// <summary>
        /// Log off user from the Application
        /// </summary>
        /// <param name="AuthenticationManager">Application Manager to handle Sign out</param>
        public void Logoff(IAuthenticationManager AuthenticationManager) {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
        /// <summary>
        /// Register User to Identity Database
        /// </summary>
        /// <param name="userManager">User Manager to Handle Registration</param>
        /// <param name="user">User to add to database</param>
        /// <param name="password">User's password</param>
        /// <returns>Identity Result</returns>
        public async Task<IdentityResult> Register(UserManager<User, string> userManager, User user, string password) =>
            await userManager.CreateAsync(user, password);

        public void ResetPassword() {
            throw new NotImplementedException();
        }

        public void ResetPasswordConfirmation() {
            throw new NotImplementedException();
        }

        public void SendCode() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Verify that code sent to User is valid
        /// </summary>
        /// <param name="signinManager">Signin Manager to handle verification</param>
        /// <param name="provider">Provider of the code</param>
        /// <param name="code">The code</param>
        /// <param name="rememberMe">Boolean if user wants to be remembered</param>
        /// <param name="rememberBrowser">Boolean if browser should be remembered</param>
        /// <returns>SignIn Status</returns>
        public async Task<SignInStatus> VerifyCode(SignInManager<User, string> signinManager, string provider, string code, bool rememberMe, bool rememberBrowser) =>
            await signinManager.TwoFactorSignInAsync(provider, code, isPersistent: rememberMe, rememberBrowser: rememberBrowser);

        /// <summary>
        /// Add supervisor
        /// </summary>
        /// <param name="supervisor">Supervisor to be added</param>
        /// <remarks>This turned out not to be necessary as I disabled
        /// the ability of any user to decide if they are a supervisor or not
        /// So now</remarks>
        public void AddSupervisor(Supervisor supervisor) => _context.Supervisors.Add(supervisor);
        /// <summary>
        /// Add plumber
        /// </summary>
        /// <param name="plumber">plumber to add</param>
        /// /// <remarks>This turned out not to be necessary as I disabled
        /// the ability of any user to decide if they are a supervisor or not
        /// So now</remarks>
        public void AddPlumber(Plumber plumber) => _context.Plumbers.Add(plumber);
        /// <summary>
        /// Get all warehouses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Warehouse> GetWarehouses() => _context.Warehouses;

        public IEnumerable<Inventory> GetInventory(string userId) {
            if (GetUserRole(userId) == "Plumber") {
                var plumber = _context.Plumbers.Where(x => x.UserId == userId).FirstOrDefault();
                return _context.Inventories.Where(x => x.WarehouseId == plumber.WarehouseId);
            } else if(GetUserRole(userId)=="Supervisor") {
                return _context.Inventories;
            }else {
                return new List<Inventory>();
            }
        }

        public string GetUserRole(string userId) {
            var plumber = _context.Plumbers.Where(x => x.UserId == userId).FirstOrDefault();
            var super = _context.Supervisors.Where(x => x.UserId == userId).FirstOrDefault();
            if (plumber != null) {
                return "Plumber";
            }else if(super!=null) {
                return "Supervisor";
            }else {
                return "Unassigned";
            }
        }
            
    }
}
