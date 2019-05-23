using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    class EFCommandInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            ShowCommandInformation(command, interceptionContext);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            ShowCommandInformation(command, interceptionContext);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            ShowCommandInformation(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            ShowCommandInformation(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            ShowCommandInformation(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            ShowCommandInformation(command, interceptionContext);
        }

        private void ShowCommandInformation<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
#if DEBUG
            try
            {
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine($"****************************************************************************");
                Debug.WriteLine($"EFCommandInterceptor");
                Debug.WriteLine($"Time: {DateTime.Now}");
                if (command.Transaction != null)
                {
                    Debug.WriteLine($"Transactional query");
                    Debug.WriteLine($"IsolationLevel: {command.Transaction.IsolationLevel}");
                }
                else
                {
                    Debug.WriteLine($"Non transactional query");
                }
                Debug.WriteLine($"CommandTimeout: {command.CommandTimeout}");
                Debug.WriteLine($"DataSource: {command.Connection.DataSource}");
                Debug.WriteLine($"Database: {command.Connection.Database}");
                Debug.WriteLine("Query:");
                if (command.Parameters != null)
                {
                    foreach (DbParameter parameter in command.Parameters)
                    {
                        Debug.WriteLine($"-> {parameter.ParameterName} = {parameter.Value}");
                    }
                }
                Debug.WriteLine(command.CommandText);
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine($"****************************************************************************");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine($"CommandInterception Error: {ex.Message}");
            }
#endif
        }
    }
}
