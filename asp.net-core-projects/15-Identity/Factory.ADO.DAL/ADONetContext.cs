using System;
using System.Data.SqlClient;

namespace Factory.ADO.DAL
{
    public class ADONetContext:IDisposable
    {
        private SqlConnection connection;
        private SqlTransaction transaction;

        public ADONetContext(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
            connection.Open();
            this.transaction = connection.BeginTransaction();
        }
        public SqlCommand CreateCommand()
        {
            var command = connection.CreateCommand();
            command.Transaction = transaction;
            return command;
        }        

        public void SaveChanges()
        {
            if (transaction == null)
            {
                throw new InvalidOperationException();
            }
            transaction.Commit();
            transaction = null;
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }
    }
}
