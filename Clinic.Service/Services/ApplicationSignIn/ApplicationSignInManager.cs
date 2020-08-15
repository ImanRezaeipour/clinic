using System;
using Clinic.Core.Domains.Users;
using Clinic.Service.Services.Users;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Clinic.Service.Services.ApplicationSignIn
{
    /// <summary>
    ///
    /// </summary>
    public class ApplicationSignInManager : SignInManager<User, Guid>, IApplicationSignInManager
    {
        #region Private Fields

        private readonly IAuthenticationManager _authenticationManager;
        private readonly UserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public ApplicationSignInManager(UserService userService, IAuthenticationManager authenticationManager)
            : base(userService, authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        #endregion Public Constructors
    }
}