using CafeHub.Application.Common.Contracts;

namespace CafeHub.Infrastructure.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;

        public DateOnly DateOnly => System.DateOnly.FromDateTime(Now);
    }
}