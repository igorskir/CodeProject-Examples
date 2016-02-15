using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.Repository
{
    /// <remarks>
    /// For testing purposes, use it to flush off all the contents of the
    /// store
    /// </remarks>
    public interface IResetable
    {
        void Reset();
    }
}
