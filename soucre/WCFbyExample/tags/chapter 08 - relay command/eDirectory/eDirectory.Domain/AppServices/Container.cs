using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.AppServices
{
    /// <remarks>
    /// version 0.71 Context Re-Factor
    /// </remarks>  
    public class Container
    {
        private static Container InternalInstace;

        private Container(){}

        private static Container Instance()
        {
            if (InternalInstace != null) return InternalInstace;
            InternalInstace = new Container();
            return InternalInstace;
        }

        public static IGlobalContext GlobalContext
        {
            get
            {
                return AppServices.GlobalContext.Instance();
            }
        }

        public static IRequestContext RequestContext { get; set; }
    }
}
