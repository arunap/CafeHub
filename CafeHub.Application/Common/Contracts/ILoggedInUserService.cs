namespace CafeHub.Application.Common.Contracts
{
    public interface ILoggedInUserService
    {
        Guid? UserId { get; }
    }
}