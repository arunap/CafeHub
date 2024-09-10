using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeHub.Core.Common
{
    public class BaseEntity : ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}