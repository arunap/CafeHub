namespace CafeHub.Application.Common.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateOnly DateOnly { get; }
    }
}