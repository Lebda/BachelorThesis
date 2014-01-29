using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
    public class XEP_QuantityManagerHolderImpl : XEP_IQuantityManagerHolder
    {
        public XEP_QuantityManagerHolderImpl(XEP_IQuantityManager manager)
        {
            _manager = manager;
        }
        XEP_IQuantityManager _manager = null;
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
    }
}
