using System;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.DataCache;

namespace XEP_SectionCheckCommon.Interfaces
{
    public class XEP_QuantityFactory
    {
        static XEP_QuantityFactory _factory = null;
        public static XEP_QuantityFactory Instance()
        {
            if (_factory == null)
            {
                _factory = new XEP_QuantityFactory();
            }
            return _factory;
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
