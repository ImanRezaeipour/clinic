using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Clinic.Data.Conventions
{
    public interface IPluralizeConvention : IConvention
    {
        TypeConventionConfiguration Types();
        PropertyConventionConfiguration Properties();
    }
}