
namespace Clinic.Core.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {

        #region Public Properties

        public string Address => System.Configuration.ConfigurationManager.AppSettings["Address"];

        public string AdminDisplayName => System.Configuration.ConfigurationManager.AppSettings["AdminDisplayName"];

        public string AdminEmail => System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];

        public string AdminPassword => System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];

        public string AdminUserName => System.Configuration.ConfigurationManager.AppSettings["AdminUserName"];

        public string AspNetIdentityRequiredEmail => System.Configuration.ConfigurationManager.AppSettings["AspNetIdentityRequiredEmail"];

        public string ColumnNameSeparator => System.Configuration.ConfigurationManager.AppSettings["ColumnNameSeparator"];

        public string ConfirmationEmail => System.Configuration.ConfigurationManager.AppSettings["ConfirmationEmail"];

        public string CookieName => System.Configuration.ConfigurationManager.AppSettings["CookieName"];

        public string CultureEnglish => System.Configuration.ConfigurationManager.AppSettings["CultureEnglish"];

        public string DatabaseSchema => System.Configuration.ConfigurationManager.AppSettings["DatabaseSchema"];

        public string EntitiesNamespace => System.Configuration.ConfigurationManager.AppSettings["EntitiesNamespace"];

        public string GoogleCallbackPath => System.Configuration.ConfigurationManager.AppSettings["GoogleCallbackPath"];

        public string GoogleCientSecret => System.Configuration.ConfigurationManager.AppSettings["GoogleCientSecret"];

        public string GoogleClientId => System.Configuration.ConfigurationManager.AppSettings["GoogleClientId"];

        public string Host => System.Configuration.ConfigurationManager.AppSettings["Host"];

        public string LoginPath => System.Configuration.ConfigurationManager.AppSettings["LoginPath"];

        public string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationConnection"].ConnectionString;


        /// <summary>
        /// كد درگاه پرداخت
        /// </summary>
        public string MerchantCode => System.Configuration.ConfigurationManager.AppSettings["MerchantCode"];

        public string Password => System.Configuration.ConfigurationManager.AppSettings["Password"];

        /// <summary>
        /// آدرس بازگشت
        /// </summary>
        public string PaymentCallbackUrl => System.Configuration.ConfigurationManager.AppSettings["PaymentCallbackUrl"];

        public string Port => System.Configuration.ConfigurationManager.AppSettings["Port"];

        public string PrefixTableName => System.Configuration.ConfigurationManager.AppSettings["PrefixTableName"];

        public string ResetPasswordConfirm => System.Configuration.ConfigurationManager.AppSettings["ResetPasswordConfirm"];

        public string ServiceLayer => System.Configuration.ConfigurationManager.AppSettings["ServiceLayer"];

        public string SqlClientNamespace => System.Configuration.ConfigurationManager.AppSettings["SqlClientNamespace"];

        public string User => System.Configuration.ConfigurationManager.AppSettings["User"];

        public string WebControllers => System.Configuration.ConfigurationManager.AppSettings["WebControllers"];

        public string XsrfKey => System.Configuration.ConfigurationManager.AppSettings["XsrfKey"];

        /// <summary>
        ///  Authority آدرس ارجاع كاربران به وب گيت پس ساخت
        /// </summary>
        public string ZarinpalGateway => System.Configuration.ConfigurationManager.AppSettings["ZarinpalGateway"];

        #endregion Public Properties
    }
}
