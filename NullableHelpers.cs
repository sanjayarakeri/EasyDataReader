using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDataReader
{
    public static class NullableHelpers42
    {
        public static string NullableStringToString(this string Value, string DefaultValue)
        {
            if (!string.IsNullOrWhiteSpace(Value)) return DefaultValue;
            return Value;
        }

        public static int NullableIntToInt(this int? Value, int DefaultValue)
        {
            if (!Value.HasValue) return DefaultValue;
            return (int)Value;
        }

        public static long NullableLongToLong(this long? Value, long DefaultValue)
        {
            if (!Value.HasValue) return DefaultValue;
            return (long)Value;
        }

        public static bool NullableBooleanToBoolean(this bool? Value, bool DefaultValue)
        {
            if (!Value.HasValue) return DefaultValue;
            return (bool)Value;
        }

        public static float NullableFloatToFloat(this float? Value, float DefaultValue)
        {
            if (!Value.HasValue) return DefaultValue;
            return (float)Value;
        }

        public static decimal NullableDecimalToDecimal(this decimal? Value, decimal DefaultValue)
        {
            if (!Value.HasValue) return DefaultValue;
            return (decimal)Value;
        }

        public static decimal NullableDoubleToDouble(this decimal? Value, decimal DefaultValue)
        {
            if (!Value.HasValue) return DefaultValue;
            return (decimal)Value;
        }
    }
}
