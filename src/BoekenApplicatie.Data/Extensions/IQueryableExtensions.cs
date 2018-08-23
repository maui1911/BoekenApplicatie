using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace BoekenApplicatie.Data.Extensions
{
  public static class QueryableExtensions
  {
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderByProp, bool desc) where T : class
    {
      if(string.IsNullOrWhiteSpace(orderByProp))
        return query;
      // orderByProp = "Id";
      string command = desc ? "OrderByDescending" : "OrderBy";
      var type = typeof(T);
      var property = type.GetProperty(orderByProp);
      var parameter = Expression.Parameter(type, "p");
      if(property == null)
      {
        throw new ArgumentNullException($"{type} does not contain property: {orderByProp}");
      }
      var propertyAccess = Expression.MakeMemberAccess(parameter, property);
      var orderByExpression = Expression.Lambda(propertyAccess, parameter);
      var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] {type, property.PropertyType},
        query.Expression, Expression.Quote(orderByExpression));
      return query.Provider.CreateQuery<T>(resultExpression);
    }

    public static IQueryable<T> FilterBy<T>(this IQueryable<T> query, string filterByProp,
      string filterValue) where T : class
    {
      if(string.IsNullOrWhiteSpace(filterValue) || string.IsNullOrWhiteSpace(filterByProp))
        return query;

      Type type = typeof(T);
      ParameterExpression parameter = Expression.Parameter(type, "param");
      MemberExpression memberAccess = Expression.MakeMemberAccess(parameter,
        type.GetProperty(filterByProp) ?? throw new InvalidOperationException());
      ConstantExpression constant = Expression.Constant(filterValue);
      MethodInfo contains = memberAccess.Type.GetMethods()
        .FirstOrDefault(x => x.Name == "Contains" && x.GetParameters().Length == 1);

      MethodCallExpression methodExp = Expression.Call(memberAccess, contains, constant);
      Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
      return query.Where(lambda);
    }


    //and filter
    public static IQueryable<T> FilterBy<T>(this IQueryable<T> query, string[] filterByProp, string[] filterValue)
      where T : class
    {
      if(filterByProp.Length == 0 || filterValue.Length == 0)
        return query;

      for(int i = 0; i < filterByProp.Length; i++)
      {
        if(string.IsNullOrWhiteSpace(filterValue[i])) continue;
        if(string.IsNullOrWhiteSpace(filterByProp[i])) continue;
        //Type type = typeof(T);
        Type type = Util.GetType<T>(filterByProp[i]);
        ParameterExpression parameter = Expression.Parameter(type, "param");
        //MemberExpression memberAccess = Expression.MakeMemberAccess(parameter,
        //  type.GetProperty(filterByProp[i]) ?? throw new InvalidOperationException());
        MemberExpression memberAccess = Expression.MakeMemberAccess(parameter, Util.GetProperty<T>(filterByProp[i]));
        ConstantExpression constant = Expression.Constant(filterValue[i]);
        MethodInfo contains = memberAccess.Type.GetMethods()
          .FirstOrDefault(x => x.Name == "Contains" && x.GetParameters().Length == 1);

        MethodCallExpression methodExp = Expression.Call(memberAccess, contains, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
        var methodCallExpression =
          Expression.Call(typeof(Queryable), "Where", new[] {typeof(T)}, query.Expression, lambda);
        query = query.Provider.CreateQuery(methodCallExpression) as IQueryable<T>;
      }

      return query;
    }

    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumZeroStart, int pageSize)
    {
      if(pageSize == 0)
      {
        throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize cannot be zero.");
      }

      if(pageNumZeroStart != 0)
      {
        query = query.Skip(pageNumZeroStart * pageSize);
      }

      return query.Take(pageSize);
    }
  }
}