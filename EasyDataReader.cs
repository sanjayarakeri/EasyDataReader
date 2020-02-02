using System;
using System.Data;
using System.Data.SqlClient;

namespace EasyDataReader
{
    public delegate T OnDataReaderExecute<T>(SqlDataReader DbReader);
    public delegate int OnDataReaderExecute(SqlDataReader DbReader);
    public delegate T OnDataSetExecute<T>(DataSet DataSet);

    public static class EasyDataReader
    {
        /// <summary>
        /// Reads data from datareader using your callback 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectionString"></param>
        /// <param name="SqlQuery">Sql Query</param>
        /// <param name="SqlParameters">Sql Add In Paramters</param>
        /// <param name="OnResult">CallBack to read data from datareader and bind it to your object/list of object or any value types</param>
        /// <param name="CommandType">CommandType may be CommandText,CommandProcedure,CommandT</param>
        /// <returns></returns>
        public static T Execute<T>(string ConnectionString, string SqlQuery, bool UseSqlTransaction, SqlParameter[] SqlParameters, OnDataReaderExecute<T> OnResult, CommandType CommandType = CommandType.Text)
        {
            T ReturnData = default(T);
            IDbTransaction DbTransaction = null;
            using (SqlConnection DbConnection = new SqlConnection(ConnectionString))
            {
                DbConnection.Open();

                if (UseSqlTransaction)
                    DbTransaction = DbConnection.BeginTransaction();

                try
                {
                    using (SqlCommand DbCommand = DbConnection.CreateCommand())
                    {
                        DbCommand.CommandTimeout = 200;
                        DbCommand.CommandText = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; " + SqlQuery;

                        if (SqlParameters != null)
                            DbCommand.Parameters.AddRange(SqlParameters);

                        using (SqlDataReader DbReader = DbCommand.ExecuteReader())
                        {
                            if (OnResult != null)
                            {
                                ReturnData = OnResult(DbReader);

                                if (DbReader != null && !DbReader.IsClosed)
                                    DbReader.Close();

                                return ReturnData;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (UseSqlTransaction)
                        DbTransaction.Rollback();
                }
                finally
                {
                    DbConnection.Close();
                }
            }
            return ReturnData;
        }

        /// <summary>
        /// Reads data from dataset using your callback 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectionString"></param>
        /// <param name="SqlQuery">Sql Query</param>
        /// <param name="SqlParameters">Sql Add In Paramters</param>
        /// <param name="OnResult">CallBack to read data from dataset and bind it to your object/list of object or any value types</param>
        /// <param name="CommandType">CommandType may be CommandText,CommandProcedure,CommandTable...</param>
        /// <returns></returns>
        public static T Execute<T>(string ConnectionString, string SqlQuery, bool UseSqlTransaction, SqlParameter[] SqlParameters, OnDataSetExecute<T> OnResult, CommandType CommandType = CommandType.Text)
        {
            T ReturnData = default(T);
            IDbTransaction DbTransaction = null;
            using (SqlConnection DbConnection = new SqlConnection(ConnectionString))
            {
                DbConnection.Open();
                if (UseSqlTransaction)
                    DbTransaction = DbConnection.BeginTransaction();

                try
                {
                    using (SqlCommand DbCommand = DbConnection.CreateCommand())
                    {
                        DbCommand.CommandText = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; " + SqlQuery;
                        DbCommand.CommandType = CommandType;

                        if (SqlParameters != null)
                            DbCommand.Parameters.AddRange(SqlParameters);

                        DataSet DataSet = new DataSet();
                        new SqlDataAdapter(DbCommand).Fill(DataSet);

                        ReturnData = OnResult(DataSet);
                    }
                }
                catch (Exception ex)
                {
                    DbTransaction.Rollback();
                }
                finally
                {
                    DbConnection.Close();
                }
            }
            return ReturnData;
        }

        /// <summary>
        /// Returns number of rows changed , deleted and inserted
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectionString"></param>
        /// <param name="SqlQuery">Sql Query</param>
        /// <param name="SqlParameters">Sql Add In Paramters</param>
        /// <param name="CommandType">CommandType may be CommandText,CommandProcedure,CommandTable...</param>
        /// <returns></returns>
        public static int Execute(string ConnectionString, string SqlQuery, bool UseSqlTransaction, SqlParameter[] SqlParameters, CommandType CommandType = CommandType.Text)
        {
            return Execute<int>(ConnectionString, SqlQuery, UseSqlTransaction, SqlParameters, IsRecordsAffected, CommandType);
        }

        private static int IsRecordsAffected(SqlDataReader DBreader)
        {
            return DBreader.RecordsAffected;
        }
    }
}
