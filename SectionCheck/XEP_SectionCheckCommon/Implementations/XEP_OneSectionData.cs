﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_OneSectionDataXml : XEP_XmlWorkerImpl
    {
        readonly XEP_OneSectionData _data = null;
        public XEP_OneSectionDataXml(XEP_OneSectionData data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_OneSectionData";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents on section on member";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.InternalForces)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
            xmlElement.Add(_data.ConcreteSectionData.XmlWorker.GetXmlElement());
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _data.Id));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlForces = xmlElement.Elements(ns + _data.ResolverForce.Resolve().XmlWorker.GetXmlElementName());
            if (xmlForces != null && xmlForces.Count() > 0)
            {
                for (int counter = 0; counter < xmlForces.Count(); ++counter)
                {
                    XElement xmlForce = Exceptions.CheckNull<XElement>(xmlForces.ElementAt(counter), "Invalid XML file");
                    XEP_IInternalForceItem item = _data.ResolverForce.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlForce);
                    _data.InternalForces.Add(item);
                }
            }
            _data.ConcreteSectionData.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.ConcreteSectionData.XmlWorker.GetXmlElementName()));
        }
        protected override void LoadAtributes( XElement xmlElement )
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
        }
        #endregion
    }

    public class XEP_OneSectionData : XEP_ObservableObject, XEP_IOneSectionData
    {
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = "Section data";
        ObservableCollection<XEP_IInternalForceItem> _internalForces = new ObservableCollection<XEP_IInternalForceItem>();
        XEP_IConcreteSectionData _concreteSectionData = null;
        // ctors
        public XEP_OneSectionData(XEP_IResolver<XEP_IInternalForceItem> resolverForce, XEP_IResolver<XEP_IConcreteSectionData> resolverConcrete, 
            XEP_IQuantityManager manager)
        {
            _resolverForce = resolverForce;
            _manager = manager;
            _xmlWorker = new XEP_OneSectionDataXml(this);
            _concreteSectionData = resolverConcrete.Resolve();
        }
        public XEP_IResolver<XEP_IInternalForceItem> ResolverForce
        {
            get
            {
                return this._resolverForce;
            }
        }
        #region XEP_IOneSectionData
        public static readonly string ConcreteSectionDataPropertyName = "ConcreteSectionData";
        public XEP_IConcreteSectionData ConcreteSectionData
        {
            get { return _concreteSectionData; }
            set { SetMember<XEP_IConcreteSectionData>(ref value, ref _concreteSectionData, (_concreteSectionData == value), ConcreteSectionDataPropertyName); }
        }
        public static readonly string InternalForcesPropertyName = "InternalForces";
        public ObservableCollection<XEP_IInternalForceItem> InternalForces
        {
            get { return _internalForces; }
            set { SetMember<ObservableCollection<XEP_IInternalForceItem>>(ref value, ref _internalForces, (_internalForces == value), InternalForcesPropertyName); }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
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
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        #endregion
    }
}
