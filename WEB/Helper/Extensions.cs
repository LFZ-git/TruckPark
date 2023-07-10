using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace WEB.Helper
{
    public static class Extensions
    {
        #region DataRow.IsEmpty()
        /// <summary>
        /// Returns false if value of any of columns of the row are not empty or NULL. 
        /// </summary>
        /// <param name="ODataRow">DataRow</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this DataRow ODataRow)
        {
            bool Result = true;

            foreach (object OValue in ODataRow.ItemArray)
                if (OValue.ToString() != string.Empty)
                { Result = false; break; }

            return Result;
        }
        #endregion

        #region String.IsEmpty()
        /// <summary>
        /// Returns true if value is NULL or empty. 
        /// </summary>
        /// <param name="ODataRow">DataRow</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this string Ostring)
        {
            return (Ostring == null || Ostring.Trim().Length == 0);
        }
        #endregion

        #region DateTime.Compare(bool IsAscending, DateTime ComparedValue)
        /// <summary>
        /// Returns comparison between two datetime values depending on IsAscending. 
        /// </summary>
        /// <param name="ODataRow">DataRow</param>
        /// <returns>bool</returns>
        public static int Compare(this DateTime ODateTime, bool IsAscending, DateTime ComparedValue)
        {
            if (IsAscending)
                return ODateTime.CompareTo(ComparedValue);
            else
                return ComparedValue.CompareTo(ODateTime);
        }
        #endregion

        #region DataTable.AddColumns(string[] ColumnNames)
        /// <summary>
        /// Adds the list of columns to the Datatable.
        /// </summary>
        /// <param name="ODataTable">DataTable</param>
        /// <param name="ColumnNames">string[]</param>
        public static void AddColumns(this DataTable ODataTable, string[] ColumnNames)
        {
            foreach (string OColumnName in ColumnNames)
                ODataTable.Columns.Add(OColumnName);
        }
        #endregion

        #region Enum.StringValue()
        /// <summary>
        /// Returns the string value for the enum value
        /// </summary>
        /// <param name="EnumValue">Enum</param>
        /// <returns>string</returns>
        //public static string StringValue(this Enum EnumValue)
        //{
        //    Type OType = EnumValue.GetType();

        //    FieldInfo OFieldInfo = OType.GetField(EnumValue.ToString());
        //    StringValue[] OStringValues = OFieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

        //    return (OStringValues.Length > 0) ? OStringValues[0].Value : "";
        //}
        #endregion

        #region DataTable.RemoveRowsWithValues(string[] columnNames)
        /// <summary>
        /// Removes rows with the values from the datatable.
        /// </summary>
        /// <param name="oDataTable">DataTable</param>
        /// <param name="columnName">string</param>
        /// <param name="values">string[]</param>
        public static void RemoveRowsWithValues(this DataTable oDataTable, string columnName, string[] values)
        {
            if (oDataTable.Columns.Contains(columnName))
            {
                int RowCount = oDataTable.Rows.Count - 1;
                for (int Counter = RowCount; Counter >= 0; Counter--)
                    if (values.Contains<string>(oDataTable.Rows[Counter][columnName].ToString()))
                        oDataTable.Rows[Counter].Delete();
                oDataTable.AcceptChanges();
            }
            else
                throw new Exception("Column with name " + columnName + " not found.");
        }
        #endregion

        #region Object.IsNull()
        /// <summary>
        /// Returns true if the object is null.
        /// </summary>
        /// <param name="oObject">object</param>
        /// <returns>bool</returns>
        public static bool IsNull(this object oObject)
        {
            return oObject == null ? true : false;
        }
        #endregion

        #region Object.IsNotNull()
        /// <summary>
        /// Returns true if the object is not null.
        /// </summary>
        /// <param name="oObject">object</param>
        /// <returns>bool</returns>
        public static bool IsNotNull(this object oObject)
        {
            return oObject == null ? false : true;
        }
        #endregion

        #region Object[].IsNullOrEmpty()
        /// <summary>
        /// Returns true if the object array is null or empty.
        /// </summary>
        /// <param name="oObject">object</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this object[] oObject)
        {
            return (oObject == null || oObject.Length == 0);
        }
        #endregion

        #region DataRowView.IsEmpty()
        /// <summary>
        /// Returns false if value of any of columns of the row view are not empty or NULL. 
        /// </summary>
        /// <param name="oDataRowView">DataRowView</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this DataRowView oDataRowView)
        {
            bool Result = true;

            foreach (object OValue in oDataRowView.Row.ItemArray)
                if (OValue.ToString() != string.Empty)
                { Result = false; break; }

            return Result;
        }
        #endregion

        #region DataTable.AddBlankRow()
        /// <summary>
        /// Adds a blank row to the datatable.
        /// </summary>
        /// <param name="oDataTable">DataTable</param>
        public static void AddBlankRow(this DataTable oDataTable)
        {
            oDataTable.Rows.Add(new object[] { });
        }
        #endregion

        #region DataTable.EnsureRow()
        /// <summary>
        /// Adds a blank row to the datatable if no rows exist.
        /// </summary>
        /// <param name="oDataTable">DataTable</param>
        public static void EnsureRow(this DataTable oDataTable)
        {
            if (oDataTable.Rows.Count == 0)
                oDataTable.Rows.Add(new object[] { });
        }
        #endregion

        #region DataTable.IsEmpty()
        /// <summary>
        /// Returns false if at least one row exists in the table.
        /// </summary>
        /// <param name="oDataTable"></param>
        /// <returns></returns>
        public static bool IsNullorEmpty(this DataTable oDataTable)
        {
            if (oDataTable.IsNull() || oDataTable.Rows.Count == 0)
                return true;
            else
                return false;
        }
        #endregion

        #region string.ReplacePrefixWildCard()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oString">string</param>
        public static string ReplacePrefixWildCard(this string oString)
        {
            if (!string.IsNullOrEmpty(oString) && oString.IndexOf('*') == 0)
            {
                char[] modifiedString = oString.ToCharArray();
                modifiedString.SetValue('%', 0);
                oString = new string(modifiedString);
            }
            return oString.Replace("*", "");
        }
        #endregion

        #region AddReplace
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyList"></param>
        /// <param name="customProperty"></param>
        public static void AddReplace(this List<Attribute> propertyList, Attribute customProperty)
        {
            foreach (Attribute listedProperty in propertyList)
            {
                if (listedProperty.Equals(customProperty))
                {
                    propertyList.Remove(listedProperty);
                    break;
                }
            }
            propertyList.Add(customProperty);
        }
        #endregion

        #region Contains
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyList"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool Contains(this Attribute[] propertyList, string propertyName)
        {
            bool contains = false;
            for (int Counter = 0; Counter <= propertyList.Length - 1; Counter++)
            {
                if (((Attribute)propertyList.GetValue(Counter)).ToString() == propertyName)
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        #endregion

        #region ToHexadecimalString
        /// <summary>
        /// Returns hexadecimal string from byte array.
        /// </summary>
        /// <param name="bytes"byte array></param>
        /// <returns>hexadecimal string</returns>
        public static string ToHexadecimalString(this byte[] bytes)
        {
            return bytes.Select(b => b.ToString("x2")).Aggregate((a, b) => a + b);
        }
        #endregion

        #region FromHexadecimalToBytes
        /// <summary>
        /// Returns byte array from a hexadecimal string
        /// </summary>
        /// <param name="hexadecimalString">hexadecimal string</param>
        /// <returns>byte array</returns>
        public static byte[] FromHexadecimalToBytes(this string hexadecimalString)
        {
            var bytes = new byte[hexadecimalString.Length / 2];

            for (var i = 0; i < hexadecimalString.Length; i += 2)
            {
                bytes[i / 2] = byte.Parse(hexadecimalString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }
        #endregion

        #region string.GetDateValue
        /// <summary>
        /// Returns true if the string is null or empty.
        /// </summary>
        /// <param name="source">string</param>
        /// <returns>bool</returns>
        public static DateTime? GetDateValue(this string source)
        {
            DateTime? result = null;
            DateTime parseResult;
            if (source.IsNullOrEmpty())
                result = null;
            else if (DateTime.TryParse(source, out parseResult))
                result = parseResult;
            return result;
        }
        #endregion

        #region DataRow.ContainsColumn(string columnName)
        /// <summary>
        /// Returns True if the column exists in the datarow
        /// </summary>
        /// <param name="oDataRow">DataRow</param>
        /// <param name="columnName">string</param>
        public static bool ContainsColumn(this DataRow oDataRow, string columnName)
        {
            return oDataRow.Table.Columns.Contains(columnName);
        }
        #endregion

        #region CustomConversion
        public static string ToJson<T>(this T source)
        {
            try
            {
                return JsonConvert.SerializeObject(source);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}