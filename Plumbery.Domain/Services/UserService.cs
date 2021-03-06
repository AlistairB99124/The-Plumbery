﻿using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace Plumbery.Domain.Services {
    /// <summary>
    /// Service class to handle User interaction functionality
    /// </summary>
    public class UserService : ServiceBase, IUserService {

        #region Private Field

        private IUserRepository _userRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise User Repository
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Confirm email of User
        /// </summary>
        /// <param name="userManager">User Manager to handle confirmation</param>
        /// <param name="userId">String user Id of the User</param>
        /// <param name="code">User code sent in Email</param>
        /// <returns>Identity Result</returns>
        public Task<IdentityResult> ConfirmEmail(UserManager<User, string> userManager, string userId, string code) =>
            _userRepository.ConfirmEmail(userManager, userId, code);

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
        public User GetUser(string Email) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get User by their GUID
        /// </summary>
        /// <param name="ID">GUID</param>
        /// <returns>User</returns>
        public User GetUserById(string ID) {
            try {
                return _userRepository.GetUserById(ID);
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="signinManager">Signin Manager to handle login</param>
        /// <param name="email">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <param name="rememberMe">Boolean if the user wants to be remembered</param>
        /// <returns>SignIn Status</returns>
        public Task<SignInStatus> Login(SignInManager<User, string> signinManager, string email, string password, bool rememberMe) =>
            _userRepository.Login(signinManager, email, password, rememberMe);
        /// <summary>
        /// Log off user from the Application
        /// </summary>
        /// <param name="AuthenticationManager">Application Manager to handle Sign out</param>
        public void Logoff(IAuthenticationManager AuthenticationManager) {
            _userRepository.Logoff(AuthenticationManager);
        }
        /// <summary>
        /// Register User to Identity Database
        /// </summary>
        /// <param name="userManager">User Manager to Handle Registration</param>
        /// <param name="user">User to add to database</param>
        /// <param name="password">User's password</param>
        public Task<IdentityResult> Register(UserManager<User, string> userManager, User user, string password) =>
            _userRepository.Register(userManager, user, password);

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
        public Task<SignInStatus> VerifyCode(SignInManager<User, string> signinManager, string provider, string code, bool rememberMe, bool rememberBrowser) =>
            _userRepository.VerifyCode(signinManager, provider, code, rememberMe, rememberBrowser);
        /// <summary>
        /// Add supervisor to database
        /// </summary>
        /// <param name="supervisor">Supervisor to be added</param>
        public void AddSupervisor(Supervisor supervisor) {
            try {
                StartTransaction();
                _userRepository.AddSupervisor(supervisor);
                PersistTransaction();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// Adds plumber to the database
        /// </summary>
        /// <param name="plumber">Plumber to add</param>
        public void AddPlumber(Plumber plumber) {
            try {
                StartTransaction();
                _userRepository.AddPlumber(plumber);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// Get all warehouses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Warehouse> GetWarehouses() {
            try {
                return _userRepository.GetWarehouses();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// Get all users to display in list
        /// </summary>
        /// <returns>List of Users</returns>
        public IEnumerable<User> GetAllUsers() {
            try {
                return _userRepository.GetAll();
            }catch(Exception x) {
                throw new ApplicationException(x.Message);
            }
        }
        /// <summary>
        /// Return Role of user (ie Supervisor or Plumber)
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>string Role</returns>
        public string GetUserRole(string userId) {
            try {
                return _userRepository.GetUserRole(userId);
            } catch (Exception x) {
                throw new ApplicationException(x.Message);
            }
        }
        /// <summary>
        /// Get Inventory to populate user profile page
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>IEnumerable Inventory</returns>
        public IEnumerable<Inventory> GetInventory(string userId) {
            try {
                return _userRepository.GetInventory(userId);
            } catch (Exception x) {
                throw new ApplicationException(x.Message);
            }
        }

        #endregion

    }
}
