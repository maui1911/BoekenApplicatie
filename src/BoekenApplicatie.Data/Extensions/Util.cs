using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoekenApplicatie.Data
{
  public static class Util
  {
    public static object GetPropertyValue(object src, string propName)
    {
      while (true)
      {
        if (src == null) throw new ArgumentException("Value cannot be null.", nameof(src));
        if (propName == null) throw new ArgumentException("Value cannot be null.", nameof(propName));

        if (propName.Contains(".")) //complex type nested
        {
          var temp = propName.Split(new[] {'.'}, 2);
          src = GetPropertyValue(src, temp[0]);
          propName = temp[1];
          continue;
        }

        var prop = src.GetType().GetProperty(propName);
        return prop?.GetValue(src, null);
      }
    }

    public static Type GetType<T>(string path)
    {
      Type currentType = typeof(T);
      var array = path.Split('.');
      for (var i = 0; i < array.Length-1; i++)
      {
        var propertyName = array[i];
        var property = currentType.GetProperty(propertyName);
        currentType = property.PropertyType;
      }

      return currentType;
    }

    public static PropertyInfo GetProperty<T>(string path)
    {
      Type currentType = typeof(T);
      PropertyInfo property = null;
      foreach (var propertyName in path.Split('.'))
      {
        property = currentType.GetProperty(propertyName);
        currentType = property.PropertyType;
      }

      return property;
    }
  }
}