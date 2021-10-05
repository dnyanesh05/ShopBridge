using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Net;

namespace ShopBridge.DataAccess
{
    public class DBLogger : IDisposable
    {
        internal string ConnectionName = ConfigurationManager.AppSettings["BrandConnectionName"];
        public int LoggedInUserId;
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposed)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (disposed)
                {
                    ConnectionName = "";
                    LoggedInUserId = 0;
                }
            }
        }

        public void LogOperation(Exception exception, string location, string type = "Server Side Log")
        {
            StackTrace stackTrace;
            StackFrame stackFrame;
            string message = "";
            try
            {
                message += "[Main Exception Message] --> " + exception.Message;
                if (exception.InnerException != null)
                {
                    message += " [Inner Exception Message] -->" + exception.InnerException.Message;
                }

                // Get stack trace for the exception with source file information
                stackTrace = new StackTrace(exception, true);

                // Get the top stack frame
                stackFrame = stackTrace.GetFrame(0);

                // Get the line number from the stack frame
                var lineNumber = stackFrame.GetFileLineNumber();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString))
                {
                    using (var cmd = new SqlCommand("USP_CATCH_BLOCK", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter messageparam = new SqlParameter();
                        messageparam.ParameterName = "@message";
                        messageparam.SqlDbType = SqlDbType.NVarChar;
                        messageparam.Direction = ParameterDirection.Input;
                        messageparam.Value = message;
                        cmd.Parameters.Add(messageparam);

                        SqlParameter sourceparam = new SqlParameter();
                        sourceparam.ParameterName = "@source";
                        sourceparam.SqlDbType = SqlDbType.NVarChar;
                        sourceparam.Direction = ParameterDirection.Input;
                        sourceparam.Value = location;
                        cmd.Parameters.Add(sourceparam);

                        cmd.Parameters.AddWithValue("@errorline", lineNumber);

                        SqlParameter typeparam = new SqlParameter();
                        typeparam.ParameterName = "@type";
                        typeparam.SqlDbType = SqlDbType.NVarChar;
                        typeparam.Direction = ParameterDirection.Input;
                        typeparam.Value = type;
                        cmd.Parameters.Add(typeparam);                        
                        cmd.Parameters.AddWithValue("@createdBy", 1);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public void ResetConnection()
        {
            ConnectionName = ConfigurationManager.AppSettings["BrandConnectionName"];
        }
    }
}
