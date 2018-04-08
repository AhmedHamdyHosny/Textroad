using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static GenericApiController.Utilities.GenericDataFormat;

namespace GenericApiController.Utilities
{
    public class Utility
    {
        public static object GetPropertyValue(object entity, string propertyName)
        {
            Type type = entity.GetType();
            
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(entity);
            }
            return null;
        }

        public static object GetFieldValue(object entity, string fieldName)
        {
            Type type = entity.GetType();
            FieldInfo fieldInfo = type.GetFields().Where(x => x.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(entity);
            }
            return null;
        }

        //private PropertyInfo GetPrimaryKeyInfo<T>()
        //{
        //    PropertyInfo[] properties = typeof(T).GetProperties();
        //    foreach (PropertyInfo pI in properties)
        //    {
        //        System.Object[] attributes = pI.GetCustomAttributes(true);
        //        foreach (object attribute in attributes)
        //        {
        //            if (attribute is System.Data.Entity.Core.Objects.DataClasses.EdmScalarPropertyAttribute)
        //            {
        //                if ((attribute as System.Data.Entity.Core.Objects.DataClasses.EdmScalarPropertyAttribute).EntityKeyProperty == true)
        //                    return pI;
        //            }
        //            else if (attribute is System.Data.Linq.Mapping.ColumnAttribute)
        //            {
        //                if ((attribute as System.Data.Linq.Mapping.ColumnAttribute).IsPrimaryKey == true)
        //                    return pI;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //private bool IsPropertyPrimaryKey<T>(T entity, string propertyName)
        //{
        //    System.Reflection.PropertyInfo propertyInfo = typeof(T).GetProperties().FirstOrDefault(x => x.Name == propertyName);
        //    Object[] attributes = propertyInfo.GetCustomAttributes(true);
        //    foreach (object attribute in attributes)
        //    {
        //        if (attribute is System.Data.Entity.Core.Objects.DataClasses.EdmScalarPropertyAttribute)
        //        {
        //            if ((attribute as System.Data.Entity.Core.Objects.DataClasses.EdmScalarPropertyAttribute).EntityKeyProperty == true)
        //                return true;
        //        }
        //        else if (attribute is System.Data.Linq.Mapping.ColumnAttribute)
        //        {
        //            if ((attribute as System.Data.Linq.Mapping.ColumnAttribute).IsPrimaryKey == true)
        //                return true;
        //        }
        //    }
        //    return false;
        //}
    }
    internal class CustStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
