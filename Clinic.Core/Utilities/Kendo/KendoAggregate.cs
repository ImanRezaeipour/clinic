using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Clinic.Core.Utilities.Kendo
{
    /// <summary>
    /// Represents a aggregate expression of Kendo DataSource.
    /// </summary>
    [DataContract(Name = "aggregate")]
    public class KendoAggregator
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the aggregate.
        /// </summary>
        [DataMember(Name = "aggregate")]
        public string Aggregate { get; set; }

        /// <summary>
        /// Gets or sets the name of the aggregated field (property).
        /// </summary>
        [DataMember(Name = "field")]
        public string Field { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Get MethodInfo.
        /// </summary>
        /// <param name="type">Specifies the type of querable data.</param>
        /// <returns>A MethodInfo for field.</returns>
        public MethodInfo MethodInfo(Type type)
        {
            var proptype = type.GetProperty(Field).PropertyType;
            switch (Aggregate)
            {
                case "max":
                case "min":
                    return GetMethod(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Aggregate), MinMaxFunc().Method, 2).MakeGenericMethod(type, proptype);

                case "average":
                case "sum":
                    return GetMethod(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Aggregate),
                        ((Func<Type, Type[]>)this.GetType().GetMethod("SumAvgFunc", BindingFlags.Static | BindingFlags.NonPublic)
                        .MakeGenericMethod(proptype).Invoke(null, null)).Method, 1).MakeGenericMethod(type);

                case "count":
                    return GetMethod(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Aggregate),
                        Nullable.GetUnderlyingType(proptype) != null ? CountNullableFunc().Method : CountFunc().Method, 1).MakeGenericMethod(type);
            }
            return null;
        }

        #endregion Public Methods

        #region Private Methods

        private static Func<Type, Type[]> CountFunc()
        {
            return (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                };
        }

        private static Func<Type, Type[]> CountNullableFunc()
        {
            return (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                };
        }

        private static MethodInfo GetMethod(string methodName, MethodInfo methodTypes, int genericArgumentsCount)
        {
            var methods = from method in typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static)
                          let parameters = method.GetParameters()
                          let genericArguments = method.GetGenericArguments()
                          where method.Name == methodName &&
                            genericArguments.Length == genericArgumentsCount &&
                            parameters.Select(p => p.ParameterType).SequenceEqual((Type[])methodTypes.Invoke(null, genericArguments))
                          select method;
            return methods.FirstOrDefault();
        }
        private static Func<Type, Type, Type[]> MinMaxFunc()
        {
            return (T, U) => new[]
                {
                    typeof (IQueryable<>).MakeGenericType(T),
                    typeof (Expression<>).MakeGenericType(typeof (Func<,>).MakeGenericType(T, U))
                };
        }

        private static Func<Type, Type[]> SumAvgFunc<U>()
        {
            return (T) => new[]
                {
                    typeof (IQueryable<>).MakeGenericType(T),
                    typeof (Expression<>).MakeGenericType(typeof (Func<,>).MakeGenericType(T, typeof(U)))
                };
        }

        #endregion Private Methods
    }
}