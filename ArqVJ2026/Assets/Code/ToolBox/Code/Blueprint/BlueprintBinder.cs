using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Cast;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ianco99.ToolBox.Bluprints
{
    public sealed class BlueprintBinder : IService
    {
        public bool IsPersistance => true;

        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();

        private Dictionary<Type, FieldInfo[]> fieldsInType;
        private Dictionary<FieldInfo, BlueprintParameterAttribute> attributeInFields;

        public BlueprintBinder()
        {
            fieldsInType = new Dictionary<Type, FieldInfo[]>();
            attributeInFields = new Dictionary<FieldInfo, BlueprintParameterAttribute>();
        }

        public void Apply(ref object instance, string blueprintTable, string blueprintID)
        {
            Type instanceType = instance.GetType();

            do
            {
                foreach (FieldInfo fieldInfo in GetFields(instanceType))
                {
                    BlueprintParameterAttribute blueprintParameter =
                        GetBlueprintParameterAttribute(fieldInfo);

                    if (blueprintParameter != null)
                    {
                        try
                        {
                            fieldInfo.SetValue(instance, StringCast.Convert(
                                BlueprintRegistry.BlueprintDatas[blueprintTable]
                                    [blueprintID, blueprintParameter.ParameterHeader],
                                fieldInfo.FieldType));
                        }
                        catch (InvalidCastException)
                        {
                            throw new DataMisalignedException(
                                $"Invalid data entry. Tried inputting data {BlueprintRegistry.BlueprintDatas[blueprintTable][blueprintID, blueprintParameter.ParameterHeader]} from Blueprint into data type {fieldInfo.FieldType}");
                            throw;
                        }
                        catch (Exception)
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new NullReferenceException($"No attribute of type {typeof(BlueprintParameterAttribute)} found for field {fieldInfo.Name} in blueprint table {blueprintTable}" );
                    }
                }

                instanceType = instanceType.BaseType;
            } while (instanceType != typeof(object));
        }

        private FieldInfo[] GetFields(Type type)
        {
            if (!fieldsInType.ContainsKey(type))
                fieldsInType.Add(type, type.GetFields(
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.DeclaredOnly));
            return fieldsInType[type];
        }

        private BlueprintParameterAttribute[] GetAttributes(Type type)
        {
            if (!fieldsInType.ContainsKey(type))
                fieldsInType.Add(type, type.GetFields(
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.DeclaredOnly));

            List<BlueprintParameterAttribute> attributeList = new();

            foreach (FieldInfo fieldInfo in fieldsInType[type])
            {
                foreach (Attribute attribute in fieldInfo.GetCustomAttributes())
                {
                    if (attribute is BlueprintParameterAttribute)
                    {
                        attributeList.Add(attribute as BlueprintParameterAttribute);
                        break;
                    }
                }
            }


            return attributeList.ToArray();
        }

        private BlueprintParameterAttribute
            GetBlueprintParameterAttribute(FieldInfo fieldInfo)
        {
            if (!attributeInFields.ContainsKey(fieldInfo))
            {
                foreach (Attribute attribute in fieldInfo.GetCustomAttributes())
                {
                    if (attribute is BlueprintParameterAttribute)
                    {
                        attributeInFields.Add(fieldInfo, attribute as BlueprintParameterAttribute);
                        return attributeInFields[fieldInfo];
                    }
                }

                return null;
            }

            return attributeInFields[fieldInfo];
        }
    }
}