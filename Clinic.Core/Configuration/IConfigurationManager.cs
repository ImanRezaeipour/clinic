namespace Clinic.Core.Configuration
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        string MerchantCode { get; }

        /// <summary>
        /// 
        /// </summary>
        string PaymentCallbackUrl { get; }

        /// <summary>
        ///  Authority آدرس ارجاع كاربران به وب گيت پس ساخت
        /// </summary>
        string ZarinpalGateway { get; }

        string LoginPath { get; }
        string CookieName { get; }
        string GoogleCallbackPath { get; }
        string WebControllers { get; }
        string ServiceLayer { get; }
        string GoogleCientSecret { get; }
        string GoogleClientId { get; }
        string AdminDisplayName { get; }
        string AdminEmail { get; }
        string AdminPassword { get; }
        string AdminUserName { get; }
        string AspNetIdentityRequiredEmail { get; }
        string ConfirmationEmail { get; }
        string ResetPasswordConfirm { get; }
        string XsrfKey { get; }
        string Address { get; }
        string Host { get; }
        string Password { get; }
        string Port { get; }
        string User { get; }
        string ConnectionString { get; }
        string EntitiesNamespace { get; }
        string SqlClientNamespace { get; }
        string CultureEnglish { get; }
        string PrefixTableName { get; }
        string DatabaseSchema { get; }
        string ColumnNameSeparator { get; }
    }
}