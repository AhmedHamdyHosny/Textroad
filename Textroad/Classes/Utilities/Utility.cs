using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using static Classes.Common.Enums;

namespace Classes.Utilities
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

        public static void SetPropertyValue(ref object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var objValue = value;
                if (value.GetType() != propertyInfo.PropertyType)
                {
                    objValue = ChangeType(value, propertyInfo.PropertyType);
                }
                propertyInfo.SetValue(obj, objValue);
            }

        }

        public static void SetPropertyValue<T>(ref T obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var objValue = value;
                if (value.GetType() != propertyInfo.PropertyType)
                {
                    objValue = ChangeType(value, propertyInfo.PropertyType);
                }
                propertyInfo.SetValue(obj, objValue);
            }

        }

        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //get match properties from source and target
            var mathProperties = sourceType.GetProperties().Where(x => targetType.GetProperties().Any(y => y.Name == x.Name));
            //  Loop through the source properties
            foreach (PropertyInfo p in mathProperties)
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;

                //  Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }

        public static void CopyObject<T>(object sourceObject, ref T destObject, List<PropertyInfo> Ex)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            List<PropertyInfo> lst = sourceType.GetProperties().ToList();
            lst = sourceType.GetProperties().Where(x => !Ex.Any(y => y.Name == x.Name)).ToList();
            //  Loop through the source properties
            foreach (PropertyInfo p in lst)
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;

                //  Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }

        public static TEntity CopyEntity<TEntity>(TEntity source) where TEntity : class, new()
        {

            // Get properties from EF that are read/write and not marked witht he NotMappedAttribute
            var sourceProperties = typeof(TEntity)
                                    .GetProperties()
                                    .Where(p => p.CanRead && p.CanWrite &&
                                                p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), true).Length == 0);
            var notVirtualProperties = sourceProperties.Where(p => !p.GetGetMethod().IsVirtual);
            var newObj = new TEntity();

            foreach (var property in notVirtualProperties)
            {

                // Copy value
                property.SetValue(newObj, property.GetValue(source, null), null);

            }

            return newObj;

        }

        internal static string GetDDLValue(params object[] pks)
        {
            return string.Join("_", pks);
        }

        internal static string GetDDLText(string text_en, string text_ar)
        {
            return text_en;
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
            return Convert.ChangeType(value, targetType);
        }

        public static string DataImportHelper(HttpPostedFileBase file, string directoryPath, string folderPath)
        {
            string fileName_datetimePart = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_");
            if (!System.IO.File.Exists(directoryPath + "\\" + folderPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath + "\\" + folderPath);
            }

            string fileName = fileName_datetimePart + file.FileName;
            string tempFilePath = System.IO.Path.Combine(directoryPath + "\\" + folderPath, fileName);
            (new System.IO.FileInfo(tempFilePath)).Directory.Create();
            file.SaveAs(tempFilePath);
            return tempFilePath;
        }

        public static string SaveFile(HttpPostedFileBase file, string directoryPath, string folderPath, ref string fileName)
        {
            string fileName_datetimePart = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_");
            if (!System.IO.Directory.Exists(directoryPath + "\\" + folderPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath + "\\" + folderPath);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = fileName_datetimePart + file.FileName;
            }
            else
            {
                fileName = fileName_datetimePart + fileName + System.IO.Path.GetExtension(file.FileName);
            }
            string tempFilePath = System.IO.Path.Combine(directoryPath + "\\" + folderPath, fileName);
            (new System.IO.FileInfo(tempFilePath)).Directory.Create();
            file.SaveAs(tempFilePath);
            return tempFilePath;
        }

        internal static DateTime? ParseDateTime(string datetimeStr)
        {
            DateTime datetime = new DateTime();
            Double seconds;
            //check if string is long
            if (double.TryParse(datetimeStr, out seconds))
            {
                DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                datetime = unixEpoch.AddSeconds(seconds).ToLocalTime();
                return datetime;
            }
            else if (DateTime.TryParse(datetimeStr, out datetime))
            {
                return datetime;
            }
            return null;
        }

        internal static bool? ParseBool(string boolStr)
        {
            bool flag = false;
            if (bool.TryParse(boolStr, out flag))
            {
                return flag;
            }
            else
            {
                switch (boolStr)
                {
                    case "0":
                        return false;
                    case "1":
                        return true;
                    default:
                        break;
                }
            }
            return null;
        }

        internal static string[] GetForeignKeyValue(string fk)
        {
            return fk.Split('_');
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static bool Compare<T>(T e1, T e2)
        {
            var type = typeof(T);
            if (e1 == null || e2 == null)
                return false;

            foreach (var property in type.GetProperties())
            {
                if (property.Name == "ExtensionData") continue;
                var object1Value = string.Empty;
                var object2Value = string.Empty;
                if (type.GetProperty(property.Name)?.GetValue(e1, null) != null)
                    object1Value = type.GetProperty(property.Name)?.GetValue(e1, null).ToString();
                if (type.GetProperty(property.Name)?.GetValue(e2, null) != null)
                    object2Value = type.GetProperty(property.Name)?.GetValue(e2, null).ToString();
                if (object2Value != null && (object1Value != null && object1Value.Trim() != object2Value.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class AlertMessage
    {

        public AlertMessageType MessageType { get; set; }
        private string _MessageContent;
        public string MessageContent
        {
            get
            {
                if (_MessageContent == null)
                {
                    _MessageContent = GetAlertMessage();
                }

                return _MessageContent;
            }
            set
            {
                _MessageContent = value;
            }
        }
        public int? TransactionCount { get; set; }
        public Transactions Transaction { get; set; }


        internal string GetAlertMessage()
        {
            var message = "";
            switch (this.Transaction)
            {
                case Transactions.Create:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Edit:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Delete:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Import:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Export:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Deactive:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Active:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertActiveSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertActiveErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertActiveWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertActiveInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return message;
        }
    }
}