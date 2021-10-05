using ShopBridge.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;

namespace DataAccess
{
    public class DBOperations : DBLogger
    {
        internal IDictionary<string, PropertyInfo[]> PropertiesCache = new Dictionary<string, PropertyInfo[]>();
        internal ReaderWriterLockSlim PropertiesCacheLock = new ReaderWriterLockSlim();
        public IList<T> GetOperation<T>(string storedProcedure, string entityXml)
        {
            List<T> result = new List<T>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString))
                {
                    using (var cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter xmlparam = new SqlParameter();
                        xmlparam.ParameterName = "@xml";
                        xmlparam.SqlDbType = SqlDbType.NVarChar;
                        xmlparam.Direction = ParameterDirection.Input;
                        xmlparam.Value = entityXml;
                        cmd.Parameters.Add(xmlparam);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(GetAs<T>(reader));
                            }
                        }
                    }
                    con.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex,  location);
                return result;
            }
            finally
            {
                ResetConnection();
            }
        }

        internal List<string> GetColumnList(SqlDataReader reader)
        {
            List<string> columnList = new List<string>();
            try
            {
                DataTable readerSchema = reader.GetSchemaTable();

                if (readerSchema != null)
                    for (int i = 0; i < readerSchema.Rows.Count; i++)
                        columnList.Add(readerSchema.Rows[i]["ColumnName"].ToString().ToUpper());
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                columnList = new List<string>();
            }
            return columnList;
        }

        internal T GetAs<T>(SqlDataReader reader)
        {
            // Create a new Object
            T entity = Activator.CreateInstance<T>();
            try
            {
                // Get all the properties in our Object
                PropertyInfo[] props = GetCachedProperties<T>();

                // For each property get the data from the reader to the object
                List<string> columnList = GetColumnList(reader);

                foreach (var prop in props)
                {
                    var propName = prop.Name;
                    if (columnList.Contains(propName.ToUpper()) && reader[propName] != DBNull.Value)
                        typeof(T).InvokeMember(propName, BindingFlags.SetProperty, null, entity, new object[] { reader[propName] });
                }
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
            }
            return entity;
        }

        public string CoreOperation(string storedProcedure, string entityXml)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString))
                {
                    using (var cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        SqlParameter outParam = new SqlParameter("@result", SqlDbType.NVarChar, -1);
                        outParam.Direction = ParameterDirection.Output;

                        SqlParameter xmlparam = new SqlParameter();
                        xmlparam.ParameterName = "@xml";
                        xmlparam.SqlDbType = SqlDbType.NVarChar;
                        xmlparam.Direction = ParameterDirection.Input;
                        xmlparam.Value = entityXml;

                        cmd.Parameters.Add(xmlparam);
                        cmd.Parameters.Add(outParam);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        var result = outParam.Value.ToString();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return "Fail";
            }
            finally
            {
                ResetConnection();
            }
        }

        internal PropertyInfo[] GetCachedProperties<T>()
        {
            if (PropertiesCache == null) PropertiesCache = new Dictionary<string, PropertyInfo[]>();
            if (PropertiesCacheLock == null) PropertiesCacheLock = new ReaderWriterLockSlim();
            PropertyInfo[] props = new PropertyInfo[0];

            try
            {
                if (PropertiesCacheLock.TryEnterUpgradeableReadLock(100))
                {
                    try
                    {
                        var fullName = typeof(T).FullName;
                        if (fullName != null && !PropertiesCache.TryGetValue(fullName, out props))
                        {
                            props = typeof(T).GetProperties();
                            if (PropertiesCacheLock.TryEnterWriteLock(100))
                            {
                                try
                                {
                                    PropertiesCache.Add(fullName, props);
                                }
                                finally
                                {
                                    PropertiesCacheLock.ExitWriteLock();
                                }
                            }
                        }
                    }
                    finally
                    {
                        PropertiesCacheLock.ExitUpgradeableReadLock();
                    }
                }
                else
                {
                    props = typeof(T).GetProperties();
                }
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                props = new PropertyInfo[0];
            }
            return props;
        }        
    }
}
