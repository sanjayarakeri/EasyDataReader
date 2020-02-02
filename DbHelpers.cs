using System;
using System.Data;
using System.Data.SqlClient;

namespace EasyDataReader
{
    public static class DbHelpers
    {
        public static SqlParameter AddParameter(this SqlCommand command, string parameterName, DbType dbType)
        {
            SqlParameter sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = parameterName;
            sqlParameter.DbType = dbType;
            return command.Parameters.Add(sqlParameter);
        }

        public static void AddWithValue(this SqlParameterCollection SqlParameterCollection, string parameterName, object value, SqlDbType sqlDbType)
        {
            var result = new SqlParameter(parameterName, sqlDbType) { Value = value };
            SqlParameterCollection.Add(result);
        }

        public static string GetSafeString(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetString(Column);
            return null;
        }

        public static int? GetSafeInt16(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetInt16(Column);
            return null;
        }

        public static int? GetSafeInt32(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetInt32(Column);
            return null;
        }

        public static long? GetSafeInt64(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetInt64(Column);
            return null;
        }

        public static double? GetSafeDouble(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetDouble(Column);
            return null;
        }

        public static decimal? GetSafeDecimal(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetDecimal(Column);
            return null;
        }

        public static bool? GetSafeBoolean(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetBoolean(Column);
            return null;
        }

        public static byte? GetSafeByte(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetByte(Column);
            return null;
        }

        public static DateTime? GetSafeDateTime(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetDateTime(Column);
            return null;
        }

        public static string GetSafeXml(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetSqlXml(Column).Value;
            return null;
        }

        public static Guid? GetSafeGuid(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetSqlGuid(Column).Value;
            return null;
        }

        public static byte[] GetSafeBinary(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return DReader.GetSqlBinary(Column).Value;
            return null;
        }

        public static decimal? GetSafeMoney(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return (int?)DReader.GetSqlMoney(Column).Value;
            return null;
        }

        public static float? GetSafeReal(this SqlDataReader DReader, int Column)
        {
            if (!DReader.IsDBNull(Column))
                return (int?)DReader.GetSqlSingle(Column).Value;
            return null;
        }
    }
}
