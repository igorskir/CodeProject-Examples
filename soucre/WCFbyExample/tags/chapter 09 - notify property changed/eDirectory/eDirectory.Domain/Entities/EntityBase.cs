using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.Entities
{
    /// <remarks>
    /// version 0.2 Chapter: Repository
    /// </remarks>
    public abstract class EntityBase
        :IEntity
    {
        public virtual long Id { get; protected set; }
    }
}
