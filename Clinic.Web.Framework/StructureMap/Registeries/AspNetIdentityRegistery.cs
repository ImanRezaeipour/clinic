using System;
using System.Data.Entity;
using System.Web;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Domains.Users;
using Clinic.Data.DbContexts;
using Clinic.Service.Services;
using Clinic.Service.Services.ApplicationSignIn;
using Clinic.Service.Services.Messages;
using Clinic.Service.Services.Roles;
using Clinic.Service.Services.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap;
using StructureMap.Web;

namespace Clinic.Web.Framework.StructureMap.Registeries
{
    /// <summary>
    /// </summary>
    public class AspNetIdentityRegistery : Registry
    {
        /// <summary>
        /// </summary>
        public AspNetIdentityRegistery()
        {
            For<BaseDbContext>().HybridHttpOrThreadLocalScoped().Use(context => (BaseDbContext)context.GetInstance<IUnitOfWork>());

            For<DbContext>().HybridHttpOrThreadLocalScoped().Use(context => (BaseDbContext) context.GetInstance<IUnitOfWork>());

            For<IUserStore<User, Guid>>().HybridHttpOrThreadLocalScoped().Use<UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>>();

            For<IRoleStore<Role, Guid>>().HybridHttpOrThreadLocalScoped().Use<RoleStore<Role, Guid, UserRole>>();

            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);

            For<IApplicationSignInManager>().HybridHttpOrThreadLocalScoped().Use<ApplicationSignInManager>();

            For<IRoleService>().HybridHttpOrThreadLocalScoped().Use<RoleService>();

            For<IIdentityMessageService>().Use<SmsService>();

            For<IIdentityMessageService>().Use<EmailService>();

            For<IUserService>().HybridHttpOrThreadLocalScoped().Use<UserService>()
                .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                .Setter(userService => userService.SmsService).Is<SmsService>()
                .Setter(userService => userService.EmailService).Is<EmailService>();

            For<UserService>().HybridHttpOrThreadLocalScoped().Use(context => (UserService) context.GetInstance<IUserService>());

            //For<IRoleStore>().HybridHttpOrThreadLocalScoped().Use<RoleStore>();

            //For<IUserService>().HybridHttpOrThreadLocalScoped().Use<UserStore>();
        }
    }
}