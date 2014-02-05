using System;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.DataCache
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
        // Members
        XEP_IResolver<XEP_IQuantity> _resolver = null;
        public XEP_IResolver<XEP_IQuantity> Resolver
        {
            get { return _resolver; }
            set { _resolver = value; }
        }
        protected XEP_QuantityFactory()
        {
        }
        public XEP_IQuantity Create(double value, eEP_QuantityType type, string name, XEP_IDataCacheObjectBase owner = null, string enumName = null, string valueName = null)
        {
            Exceptions.CheckNull(_resolver);
            XEP_IQuantity newObject =_resolver.Resolve();
            newObject.Name = name;
            newObject.QuantityType = type;
            newObject.Value = value;
            newObject.Owner = owner;
            if (newObject.QuantityType == eEP_QuantityType.eEnum)
            {
                newObject.EnumName = enumName;
            }
            else if (newObject.QuantityType == eEP_QuantityType.eString)
            {
                newObject.ValueName = valueName;
            }
            return newObject;
        }
    }
}