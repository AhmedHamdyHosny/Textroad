using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static GenericApiController.Utilities.GenericDataFormat;

namespace GenericApiController.Utilities
{
    public partial class Repository<TEntity> where TEntity : class
    {
        
        public static IEnumerable<string> GetPKColumns(DbContext context)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();
            IEnumerable<string> keyNames = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name);
            return keyNames;
        }
        
        public static Dictionary<string,Type> GetReferenceTypes(DbContext context)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();
            var referenceEntities = set.EntitySet.ElementType.NavigationProperties;
            Dictionary<string, Type> result = new Dictionary<string, Type>();
            
            foreach (var referenceEntity in referenceEntities)
            {
                var DepProps = referenceEntity.GetDependentProperties();
                if(DepProps != null && DepProps.Count() > 0)
                {
                    var foreignKey = DepProps.SingleOrDefault(x => x.DeclaringType.FullName == typeof(TEntity).FullName);
                    var props = typeof(TEntity).GetProperties();
                    if(props != null && props.Count() > 0)
                    {
                        Type referenceType = props.SingleOrDefault(x => x.PropertyType.FullName == referenceEntity.TypeUsage.EdmType.FullName).PropertyType;
                        result.Add(foreignKey.Name, referenceType);
                    }
                }
                
            }

            return result;
        }
        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy(string orderColumns, GenericDataFormat.SortType orderType)
        {
            // get order by Query
            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable);
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumns.Split('.');
            IQueryable<TEntity> query = new List<TEntity>().AsQueryable<TEntity>();
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type);

            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType.Equals(GenericDataFormat.SortType.Desc) ? "OrderByDescending" : "OrderBy";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();
            
            return orderby;
        }
        
        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetThenBy(string orderColumns, GenericDataFormat.SortType orderType)
        {
            // get order by Query
            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable);
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumns.Split('.');
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type);

            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType.Equals(GenericDataFormat.SortType.Desc) ? "ThenByDescending" : "ThenBy";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);

            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> thenBy = (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();

            return thenBy;
        }

        public static Tuple<Expression, Type> GetSelector(IEnumerable<string> propertyNames)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            Expression body = parameter;

            foreach (var property in propertyNames)
            {
                var s = body.Type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                body = Expression.Property(body,s);
            }

            return Tuple.Create(Expression.Lambda(body, parameter) as Expression
                , body.Type);
        }

        public static Tuple<Expression, Type> GetSelectorTuple(string propertyNames)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            Expression body = parameter;

            foreach (var property in propertyNames.Split('.'))
            {
                var s = body.Type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                body = Expression.Property(body, s);
            }

            return Tuple.Create(Expression.Lambda(body, parameter) as Expression
                , body.Type);
        }

        public static Expression<Func<TEntity,object>> GetSelector(string property)
        {
            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable);
            string[] props = property.Split('.');
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type);
            Expression expr = arg;

            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType; 
            }
            
            var sortExpression = Expression.Lambda<Func<TEntity, object>>(expr, arg);
            return sortExpression;
        }

        public static Expression GetExpression(Expression left, Expression right, FilterOperations operation)
        {
            Expression exprBody = null;
            switch (operation)
            {
                case FilterOperations.Equal:
                    exprBody = Expression.Equal(left,
                             right);
                    break;
                case FilterOperations.NotEqual:
                    exprBody = Expression.NotEqual(left, right);
                    break;
                case FilterOperations.GreaterThan:
                    exprBody = Expression.GreaterThan(left, right);
                    break;
                case FilterOperations.LessThan:
                    exprBody = Expression.LessThan(left, right);
                    break;
                case FilterOperations.GreaterThanOrEqual:
                    exprBody = Expression.GreaterThanOrEqual(left, right);
                    break;
                case FilterOperations.LessThanOrEqual:
                    exprBody = Expression.LessThanOrEqual(left, right);
                    break;
                //case FilterOperations.In:
                //    break;
                //case FilterOperations.NotIn:
                //    break;
                //case FilterOperations.Between:
                //    break;
                case FilterOperations.Like:
                    exprBody =  Expression.Equal(
                        Expression.Call(left,
                        typeof(String).GetMethod("Contains"),
                        new Expression[] { (ConstantExpression)right }), Expression.Constant(true));
                    break;
                default:
                    break;
            }
            
            return exprBody;
        }

        public static void SetPropertyValue(ref TEntity entity, string propertyName, object cellValue)
        {
            Type type = entity.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName,StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var value = ChangeType(cellValue, propertyInfo.PropertyType);
                propertyInfo.SetValue(entity, value);
            }

        }

        public static object ChangeType(object value, Type conversionType)
        {
            var targetType = conversionType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                targetType = Nullable.GetUnderlyingType(targetType);
            }
            if (targetType == typeof(Guid))
            {
                return System.ComponentModel.TypeDescriptor.GetConverter(typeof(Guid)).ConvertFromInvariantString(value.ToString());
            }
            return Convert.ChangeType(value, targetType);
        }

        public static object GetPropertyValue(TEntity entity, string propertyName)
        {
            Type type = entity.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(entity);
            }
            return null;
        }
        
        public static Expression<Func<TEntity, bool>> GetFilter(List<FilterItems> filters)
        {
            Expression<Func<TEntity, bool>> filterExpr = null;
            if (filters != null)
            {
                var logicalOperation = LogicalOperations.And;
                foreach (var filter in filters)
                {
                    ParameterExpression paramExpr = Expression.Parameter(typeof(TEntity));
                    var left = Expression.PropertyOrField(paramExpr, filter.Property);
                    var rightValue = ChangeType(filter.Value, left.Type);
                    var right = Expression.Constant(rightValue, left.Type);
                    Expression exprBody = Repository<TEntity>.GetExpression(left, right, filter.Operation);
                    if (filterExpr != null)
                    {
                        Expression<Func<TEntity, bool>> exp2 = Expression.Lambda<Func<TEntity, bool>>(exprBody, paramExpr);
                        if (logicalOperation == LogicalOperations.And)
                        {
                            filterExpr = filterExpr.AndAlso<TEntity>(exp2);
                        }
                        else
                        {
                            filterExpr = filterExpr.OrElse<TEntity>(exp2);
                        }
                    }
                    else
                    {
                        filterExpr = Expression.Lambda<Func<TEntity, bool>>(exprBody, paramExpr);
                    }
                    logicalOperation = filter.LogicalOperation;
                }
            }
            return filterExpr;

        }

        //public static object GetId(object id, DbContext context)
        //{
        //    IEnumerable<string> keyNames = GetPKColumns(context);
        //    PropertyInfo propertyInfo = null;
        //    if (keyNames != null && keyNames.Count() == 1)
        //    {
        //        Type type = typeof(TEntity);
        //        propertyInfo = type.GetProperties().Where(x => x.Name.Equals(keyNames.ElementAt(0), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

        //    }
        //    else
        //    {
        //        throw new Exception("Model not has primary key or has more than one primary key");
        //    }

        //    var TEntityId = ChangeType(id, propertyInfo.PropertyType);
        //    return TEntityId;
        //}

        //public static Expression<Func<TEntity, TKey>> GetSelectorx<TKey>(string property)
        //{
        //    Type typeQueryable = typeof(IQueryable<TEntity>);
        //    ParameterExpression argQueryable = Expression.Parameter(typeQueryable);
        //    string[] props = property.Split('.');
        //    Type type = typeof(TEntity);
        //    ParameterExpression arg = Expression.Parameter(type);
        //    Expression expr = arg;

        //    foreach (string prop in props)
        //    {
        //        PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        expr = Expression.Property(expr, pi);
        //        type = pi.PropertyType;
        //    }

        //    var sortExpression = Expression.Lambda<Func<TEntity, TKey>>(expr, arg);
        //    return sortExpression;
        //}

        //public static IQueryable<TEntity> ApplySorting<TEntity, U>(IQueryable<TEntity> query, Expression<Func<TEntity, U>> predicate, string orderType = "Asc")
        //{
        //    var ordered = query as IOrderedQueryable<TEntity>;
        //    if (true)
        //    {
        //        if (ordered != null)
        //            return ordered.ThenBy(predicate);
        //        return query.OrderBy(predicate);
        //    }
        //    else
        //    {
        //        if (ordered != null)
        //            return ordered.ThenByDescending(predicate);
        //        return query.OrderByDescending(predicate);
        //    }
        //}
        //public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderByFunc<T>(Tuple<IEnumerable<string>, string> sortCriteria)
        //{
        //    var selector = GetSelector<T>(sortCriteria.Item1);
        //    Type[] argumentTypes = new[] { typeof(T), selector.Item2 };

        //    var orderByMethod = typeof(Queryable).GetMethods()
        //        .First(method => method.Name == "OrderBy"
        //            && method.GetParameters().Count() == 2)
        //            .MakeGenericMethod(argumentTypes);
        //    var orderByDescMethod = typeof(Queryable).GetMethods()
        //        .First(method => method.Name == "OrderByDescending"
        //            && method.GetParameters().Count() == 2)
        //            .MakeGenericMethod(argumentTypes);

        //    if (sortCriteria.Item2 == "Desc")
        //        return query => (IOrderedQueryable<T>)
        //            orderByDescMethod.Invoke(null, new object[] { query, selector.Item1 });
        //    else
        //        return query => (IOrderedQueryable<T>)
        //            orderByMethod.Invoke(null, new object[] { query, selector.Item1 });
        //}

        //public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderByFunc(string orderColumns)
        //{

        //    Tuple<Expression, Type> selector = GetSelector(orderColumns.Split(','));
        //    Type type = selector.Item2;
        //    Type[] argumentTypes = new[] { typeof(TEntity), type };

        //    var orderByMethod = typeof(Queryable).GetMethods()
        //        .First(method => method.Name == "OrderBy"
        //            && method.GetParameters().Count() == 2)
        //            .MakeGenericMethod(argumentTypes);
        //    var orderByDescMethod = typeof(Queryable).GetMethods()
        //        .First(method => method.Name == "OrderByDescending"
        //            && method.GetParameters().Count() == 2)
        //            .MakeGenericMethod(argumentTypes);

        //    if (true)
        //        return query => (IOrderedQueryable<TEntity>)
        //            orderByMethod.Invoke(null, new object[] { query, selector.Item1 });
        //    else
        //        return query => (IOrderedQueryable<TEntity>)
        //            orderByDescMethod.Invoke(null, new object[] { query, selector.Item1 });
        //}

        //public virtual IQueryable<TEntity> Tune()
        //{

        //}


    }

    //public class SortingItem
    //{
    //    public string PropertySelectorString { get; set; }
    //    public SortingDirectionsEnum SortingDirections { get; set; }

    //    public string GetSortCriteria(this SortingItem item)
    //    {
    //        return string.Format("{0} {1}", item.PropertySelectorString,
    //               item.SortingDirections == SortingDirectionsEnum.Descending ?
    //                  "DESC" : "ASC");
    //    }
    //}

    //public enum SortingDirectionsEnum
    //{
    //    Descending,
    //    Ascending
    //}

    
}
