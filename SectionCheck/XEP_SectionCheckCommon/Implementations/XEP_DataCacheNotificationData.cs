﻿using System;
using System.Linq;
using XEP_SectionCheckCommon.DataCache;
using XEP_Prism.Infrastructure;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.Implementations
{
    public class XEP_DataCacheNotificationData : XEP_IDataCacheNotificationData
    {
        XEP_IDataCacheObjectBase _owner = null;
        public XEP_IDataCacheObjectBase Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        XEP_IDataCacheObjectBase _notifier = null;
        public XEP_IDataCacheObjectBase Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }
        string _propertyNotifier = String.Empty;
        public string PropertyNotifier
        {
            get { return _propertyNotifier; }
            set { _propertyNotifier = value; }
        }
    }
}
