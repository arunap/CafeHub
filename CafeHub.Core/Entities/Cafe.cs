using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeHub.Core.Common;

namespace CafeHub.Core.Entities
{
    public class Cafe : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
        public ICollection<CafeEmployee> CafeEmployees { get; set; } = new List<CafeEmployee>();
    }
}