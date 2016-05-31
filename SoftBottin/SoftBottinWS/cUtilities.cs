using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace SoftBottinWS
{
    public static class cUtilities
    {
        #region Global

        #endregion

        #region Properties
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                if (ColType.IsSerializable)
                {
                    t.Columns.Add(propInfo.Name, ColType);
                }
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    if (propInfo.PropertyType.IsSerializable)
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }
                }

                t.Rows.Add(row);
            }

            return ds;
        }
        /// <summary>
        /// Daniel Romero 4 de Marzo de 2016
        /// Metodo que se crea para controlar los logs de errores en la aplicacion
        /// </summary>
        /// <param name="sMessage"></param>
        /// <param name="sError"></param>
        /// <returns></returns>
        public static bool WriteLog(string sMessage, out string sError)
        {
            try
            {
                string sPath = @"\ZAW\logs",
                       fullPath,
                       sFileName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_"
                                        + DateTime.Now.Day + ".txt",
                       sPathString;
                fullPath = Path.GetFullPath(sPath);

                sPathString = System.IO.Path.Combine(fullPath, sFileName);

                sMessage = "---" + DateTime.Now.ToString() + "----" + sMessage + "---\r\n";

                if (!Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                else if (!File.Exists(sPathString))
                {
                    File.Create(sPathString).Close();
                    File.WriteAllText(sPathString, sMessage);
                }
                else
                {
                    File.AppendAllText(sPathString, sMessage);
                }

                sError = "";
                return true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return false;
            }
        }
        #endregion

        #region Methods

        #endregion


    }
}