using CafeHub.Core.Enums;

namespace CafeHub.Application.Queries.ViewModels
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public int DaysWorked { get; set; } = 0;
        public string? CafeName { get; internal set; }
    }
}