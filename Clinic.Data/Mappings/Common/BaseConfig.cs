using System.Data.Entity.ModelConfiguration;

namespace Clinic.Data.Mappings.Common
{
    /// <summary>
    /// </summary>
    public abstract class BaseConfig <TEntity> :EntityTypeConfiguration <TEntity> where TEntity :class 
    {
    }
}