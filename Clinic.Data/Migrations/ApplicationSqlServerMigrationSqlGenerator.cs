using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Clinic.Data.Migrations
{
    /// <summary>
    /// </summary>
    public class ApplicationSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="dropForeignKeyOperation"></param>
        protected override void Generate(DropForeignKeyOperation dropForeignKeyOperation)
        {
            dropForeignKeyOperation.Name = StripDbo(dropForeignKeyOperation.Name);
            base.Generate(dropForeignKeyOperation);
        }

        /// <summary>
        /// </summary>
        /// <param name="dropIndexOperation"></param>
        protected override void Generate(DropIndexOperation dropIndexOperation)
        {
            dropIndexOperation.Name = StripDbo(dropIndexOperation.Name);
            base.Generate(dropIndexOperation);
        }

        /// <summary>
        /// </summary>
        /// <param name="dropPrimaryKeyOperation"></param>
        protected override void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation)
        {
            dropPrimaryKeyOperation.Name = StripDbo(dropPrimaryKeyOperation.Name);
            base.Generate(dropPrimaryKeyOperation);
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string StripDbo(string name)
        {
            return name.Replace("dbo.", "");
        }

        #endregion Private Methods
    }
}