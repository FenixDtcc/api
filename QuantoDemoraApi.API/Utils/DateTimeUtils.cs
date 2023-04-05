namespace QuantoDemoraApi.Utils
{
    public class DateTimeUtils
    {
        public static DateTime HorarioBrasilia() => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
