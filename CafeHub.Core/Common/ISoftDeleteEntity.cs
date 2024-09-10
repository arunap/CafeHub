using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeHub.Core.Common
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}