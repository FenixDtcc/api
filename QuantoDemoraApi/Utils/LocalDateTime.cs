using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantoDemoraApi.Utils
{
    public class LocalDateTime
    {
        public static DateTime HorarioBrasilia() => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}