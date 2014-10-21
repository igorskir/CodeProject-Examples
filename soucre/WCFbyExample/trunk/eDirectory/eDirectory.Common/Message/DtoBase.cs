using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;

namespace eDirectory.Common.Message
{
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    [DataContract]
    public abstract class DtoBase
        : IDtoResponseEnvelop
    {
        #region IDtoResponseEnvelop Members

        [DataMember]
        private readonly Response ResponseInstance = new Response();
        public Response Response
        {
            get { return ResponseInstance; }
        }

        #endregion
    }
}
