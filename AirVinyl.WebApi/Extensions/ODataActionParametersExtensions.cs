using Microsoft.AspNetCore.OData.Formatter;
using System;

namespace AirVinyl.WebApi.Extensions
{
    public static class ODataActionParametersExtensions
    {
        public static bool TryGetValue<T>(this ODataActionParameters source, string key, out T value)
        {
            value = default(T);
            if (source.TryGetValue(key, out object oValue) == false)
                return false;
            value = (T)Convert.ChangeType(oValue.ToString(), typeof(T));
            return true;
        }
    }
}
