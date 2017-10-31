using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace OKLogger
{
    public static class ReflectionFactory
    {
        private static ConcurrentDictionary<Type, IList<PropertyInfo>> propertyInfos = new ConcurrentDictionary<Type, IList<PropertyInfo>>();

        internal static IList<PropertyInfo> GetProperties<T>(T obj)
        {
            var objType = obj.GetType();
            if(propertyInfos.TryGetValue(objType, out IList<PropertyInfo> propertyInfo))
            {
                return propertyInfo;
            }
            else
            {
                var runtimeProps = objType.GetRuntimeProperties().ToList();
                propertyInfos.TryAdd(objType, runtimeProps);
                return runtimeProps;
            }
        }

    }
}
