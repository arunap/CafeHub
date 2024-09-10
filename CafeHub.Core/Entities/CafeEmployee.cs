using CafeHub.Core.Common;

namespace CafeHub.Core.Entities
{
    public class CafeEmployee : BaseEntity
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid CafeId { get; set; }
        public Cafe Cafe { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}