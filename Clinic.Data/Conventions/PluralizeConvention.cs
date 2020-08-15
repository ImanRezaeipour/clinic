using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;

namespace Clinic.Data.Conventions
{
    /// <summary>
    /// </summary>
    public class PluralizeConvention : Convention, IPluralizeConvention
    {
        #region Public Constructors

     

        public PluralizeConvention()
        {
            var pluralization = PluralizationService.CreateService(new CultureInfo("en-US"));

            // FieldName Convention
            Properties().Configure(property =>
            {
                if (property.ClrPropertyInfo.ReflectedType != null)
                    property.HasColumnName(property.ClrPropertyInfo.ReflectedType.Name + "_" + property.ClrPropertyInfo.Name);
            });

            // TableName Convention
            Types().Configure(entity => entity.ToTable("AR_" + pluralization.Pluralize(entity.ClrType.Name), "dbo"));
        }

        #endregion Public Constructors
    }
}