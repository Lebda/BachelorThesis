using System;
using System.Linq;
using XEP_SectionCheckCommon.Implementations;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using System.Threading;

namespace XEP_SectionCheckCommon.Interfaces
{
    public class XEP_QuantityFactory
    {
        static readonly object _lock = new object();
        static XEP_QuantityFactory s_singleton = null;
        public static XEP_QuantityFactory Instance()
        {
            if (s_singleton == null) 
            { // Double checked locking pattern for thread safety
                lock (_lock)
                {
                    if (s_singleton == null)
                    {
                        s_singleton = new XEP_QuantityFactory();
                    }
                }
            }
            return s_singleton;
        }
        protected XEP_QuantityFactory()
        {
        }
        public XEP_IQuantity Create(XEP_IQuantityManager manager, double value, eEP_QuantityType type, string name)
        {
            return new XEP_Quantity(manager, value, type, name);
        }
    }
}