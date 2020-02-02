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
                UserID = "",
                Password = "",
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
                    JobId = DbReader.GetSafeInt32(0),
                    name = DbReader.GetSafeInt32(1),
                    xml = DbReader.GetSafeMoney(2),
                    bindata = DbReader.GetSafeInt16(3),
                    size = DbReader.GetSafeReal(4)
                });
            }
            return objlist;
        }
    }
}
