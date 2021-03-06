﻿using System;
using System.Linq;
using System.Xml.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_ESDiagramItemXml : XEP_XmlWorkerImpl
    {
        public XEP_ESDiagramItemXml(XEP_ESDiagramItem data) : base (data)
        {
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_ESDiagramItem";
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        #endregion
    }

    public class XEP_ESDiagramItem : XEP_ObservableObject, XEP_IESDiagramItem
    {
        readonly XEP_IResolver<XEP_IESDiagramItem> _resolverDiagramItem = null;
        public XEP_ESDiagramItem(XEP_IResolver<XEP_IESDiagramItem> resolverDiagramItem)
        {
            _resolverDiagramItem = resolverDiagramItem;
            _xmlWorker = new XEP_ESDiagramItemXml(this);
            AddOneQuantity(0.0, eEP_QuantityType.eStrain, StrainPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eStress, StressPropertyName);
            Intergrity(null);
        }
        #region XEP_IESDiagramItem Members
        public XEP_IESDiagramItem CopyInstance()
        {
            XEP_IESDiagramItem newObject = _resolverDiagramItem.Resolve();
            XEP_ESDiagramItem newThis = newObject as XEP_ESDiagramItem;
            newThis.CopyAllQuanties(this, newObject);
            newObject.Name = _name;
            return newObject;
        }
        public static readonly string StrainPropertyName = "Strain";
        public XEP_IQuantity Strain
        {
            get { return GetOneQuantity(StrainPropertyName); }
            set { SetItem(ref value, StrainPropertyName); }
        }
        public static readonly string StressPropertyName = "Stress";
        public XEP_IQuantity Stress
        {
            get { return GetOneQuantity(StressPropertyName); }
            set { SetItem(ref value, StressPropertyName); }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public void Intergrity(string propertyCallerName)
        {

        }
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        string _name = "StressStrainDiagramPoint";
        public string Name
        {
            get { return _name; }
            set { SetMember<string>(ref value, ref _name, (_name == value), XEP_Constants.NamePropertyName); }
        }
        Guid _guid = Guid.NewGuid();
        public Guid Id
        {
            get { return _guid; }
            set { SetMember<Guid>(ref value, ref _guid, (_guid == value), XEP_Constants.GuidPropertyName); }
        }
        XEP_IXmlWorker _xmlWorker = null;
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        #endregion
    }
}
