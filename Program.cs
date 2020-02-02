using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EasyDataReader
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                ConnectTimeout = 200,
                UserID = "test",
                Password = "test",
                InitialCatalog = "Northwind",
                DataSource = @"LOCALHOST\SQLEXPRESS"
            };

            string ConnectionString = SqlConnectionStringBuilder.ConnectionString.ToString();

            List<object> f = EasyDataReader.Execute(ConnectionString, "select * from [Order Details]", false, null, GetDetails);                                                                                                                                                                                                  //d.Wait();

            Console.ReadLine();
        }

        private static List<object> GetDetails(SqlDataReader DbReader)
        {
            List<object> objlist = new List<object>();
            while (DbReader.Read())
            {
                objlist.Add(new
                {
                    Data1 = DbReader.GetSafeInt32(0),
                    Data2 = DbReader.GetSafeInt32(1),
                    Data3 = DbReader.GetSafeMoney(2),
                    Data4 = DbReader.GetSafeInt16(3),
                    Data5 = DbReader.GetSafeReal(4)
                });
            }
            return objlist;
        }
    }
}
