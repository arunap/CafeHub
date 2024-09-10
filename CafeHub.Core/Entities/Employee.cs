using CafeHub.Core.Common;
using CafeHub.Core.Enums;

namespace CafeHub.Core.Entities
{
    public class Employee : BaseAuditableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; } = Gender.Other;
        public List<CafeEmployee> CafeEmployees { get; set; }
    }
}