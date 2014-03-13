using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainer.Common.Enums
{
    public enum LifetimeType
    {
        /// <summary>
        /// Default is Transient.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Creates a new instance for each new request.
        /// </summary>
        Transient = 1,
        /// <summary>
        /// Creates one instance for all requests.
        /// </summary>
        Singleton = 2,
    }
}
