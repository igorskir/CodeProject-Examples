using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.Message
{
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    public interface IDtoResponseEnvelop
    {
        Response Response { get; }
    }
}
