using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericApiController.Utilities
{
    internal static class RuntimeTypeBuilder
    {
        private static readonly ModuleBuilder moduleBuilder;
        private static readonly IDictionary<string, Type> builtTypes;

        static RuntimeTypeBuilder()
        {
            var assemblyName = new AssemblyName { Name = "DynamicLinqTypes" };
            moduleBuilder = Thread.GetDomain()
                    .DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run)
                    .DefineDynamicModule(assemblyName.Name);
            builtTypes = new Dictionary<string, Type>();
        }

        internal static Type GetRuntimeType(IDictionary<string, PropertyInfo> fields)
        {
            var typeKey = GetTypeKey(fields);
            if (!builtTypes.ContainsKey(typeKey))
            {
                lock (moduleBuilder)
                {
                    builtTypes[typeKey] = GetRuntimeType(typeKey, fields);
                }
            }

            return builtTypes[typeKey];
        }

        private static Type GetRuntimeType(string typeName, IEnumerable<KeyValuePair<string, PropertyInfo>> properties)
        {
            var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);
            foreach (var property in properties)
            {
                //typeBuilder.DefineField(property.Key, property.Value.PropertyType, FieldAttributes.Public);
                FieldBuilder typeField = typeBuilder.DefineField(property.Key, property.Value.PropertyType, FieldAttributes.Private);
                PropertyBuilder typeProperty = typeBuilder.DefineProperty(property.Key, PropertyAttributes.None, property.Value.PropertyType, null);
                MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
                var methodGet = typeBuilder.DefineMethod("get_"+property.Key ,getSetAttr, property.Value.PropertyType, Type.EmptyTypes);
                ILGenerator ilGet = methodGet.GetILGenerator();
                ilGet.Emit(OpCodes.Ldarg_0);
                ilGet.Emit(OpCodes.Ldfld, typeField);
                ilGet.Emit(OpCodes.Ret);

                var methodSet = typeBuilder.DefineMethod("set_" + property.Key,getSetAttr,null, new Type[] { property.Value.PropertyType });
                ILGenerator ilSet = methodSet.GetILGenerator();
                ilSet.Emit(OpCodes.Ldarg_0);
                ilSet.Emit(OpCodes.Ldarg_1);
                ilSet.Emit(OpCodes.Stfld, typeField);
                ilSet.Emit(OpCodes.Ret);

                typeProperty.SetGetMethod(methodGet);
                typeProperty.SetSetMethod(methodSet);
                
            }

            return typeBuilder.CreateType();
        }

        private static string GetTypeKey(IEnumerable<KeyValuePair<string, PropertyInfo>> fields)
        {
            return fields.Aggregate(string.Empty, (current, field) => current + (field.Key + ";" + field.Value.Name + ";"));
        }
    }
}
