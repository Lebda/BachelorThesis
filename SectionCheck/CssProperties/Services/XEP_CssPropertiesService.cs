using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Interfaces;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Implementations;

namespace XEP_CssProperties.Services
{
    public class XEP_CssPropertiesService : XEP_ICssPropertiesService, XEP_IQuantityManagerHolder
    {
        XEP_QuantityManagerHolderImpl _managerHolder = null;
        public XEP_IQuantityManager Manager
        {
            get { return _managerHolder.Manager; }
            set { _managerHolder.Manager = value; }
        }
        public XEP_CssPropertiesService(XEP_IQuantityManager manager)
        {
            _managerHolder = new XEP_QuantityManagerHolderImpl(manager);
        }
        public ObservableCollection<XEP_InternalForceItem> GetInternalForces()
        {
            ObservableCollection<XEP_InternalForceItem> collection = new ObservableCollection<XEP_InternalForceItem>();
            XEP_InternalForceItem item = new XEP_InternalForceItem(Manager);
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = true;
            item.Name = "RS2-C01.1-1";
            item.N.Value = 150000.0;
            item.Vy.Value = 65000.0;
            item.Vz.Value = 0.0;
            item.Mx.Value = 113000;
            item.My.Value = 32000;
            item.Mz.Value = 68000;
            collection.Add(item);
            item = new XEP_InternalForceItem(Manager);
            item.Name = "RS2-C02.2-4";
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = false;
            item.N.Value = 178000.0;
            item.Vy.Value = 52000.0;
            item.Vz.Value = 0.0;
            item.Mx.Value = 98000;
            item.My.Value = 42000;
            item.Mz.Value = 75000;
            collection.Add(item);
            return collection;
        }
    }
}
