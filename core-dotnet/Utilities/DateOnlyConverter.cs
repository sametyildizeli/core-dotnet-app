using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace core_dotnet.Utilities;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), dateTime => DateOnly.FromDateTime(dateTime)) { }
}