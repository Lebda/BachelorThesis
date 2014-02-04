using System;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_SetupParametersXml : XEP_XmlWorkerImpl
    {
        readonly XEP_SetupParameters _data = null;
        public XEP_SetupParametersXml(XEP_SetupParameters data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_SetupParameters";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents setup parameters for calculation.";
        }
        protected override void AddElements(XElement xmlElement)
        {
        }
        protected override void LoadElements(XElement xmlElement)
        {
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _data.Id));
            foreach (var item in _data.Data)
            {
                xmlElement.Add(new XAttribute(ns + item.Name, item.Value));
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
            foreach (var item in _data.Data)
            {
                item.Value = (double)xmlElement.Attribute(ns + item.Name);
            }
        }
        #endregion
    }

    public class XEP_SetupParameters : XEP_ObservableObject, XEP_ISetupParameters
    {
        public XEP_SetupParameters()
        {
            _xmlWorker = new XEP_SetupParametersXml(this);
            AddOneQuantity(0.0, eEP_QuantityType.eNoUnit, GammaCPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eNoUnit, GammaSPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eNoUnit, AlphaCcPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eNoUnit, AlphaCtPropertyName);
            AddOneQuantity( 0.0, eEP_QuantityType.eNoUnit, FiPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eNoUnit, FiEffPropertyName);
        }

        #region XEP_ISetupParameters Members
        public static readonly string GammaCPropertyName = "GammaC";
        public XEP_IQuantity GammaC
        {
            get { return GetOneQuantity(GammaCPropertyName); }
            set { SetItem(ref value, GammaCPropertyName); }
        }
        public static readonly string GammaSPropertyName = "GammaS";
        public XEP_IQuantity GammaS
        {
            get { return GetOneQuantity(GammaSPropertyName); }
            set { SetItem(ref value, GammaSPropertyName); }
        }
        public static readonly string AlphaCcPropertyName = "AlphaCc";
        public XEP_IQuantity AlphaCc
        {
            get { return GetOneQuantity(AlphaCcPropertyName); }
            set { SetItem(ref value, AlphaCcPropertyName); }
        }
        public static readonly string AlphaCtPropertyName = "AlphaCt";
        public XEP_IQuantity AlphaCt
        {
            get { return GetOneQuantity(AlphaCtPropertyName); }
            set { SetItem(ref value, AlphaCtPropertyName); }
        }
        public static readonly string FiPropertyName = "Fi";
        public XEP_IQuantity Fi
        {
            get { return GetOneQuantity(FiPropertyName); }
            set { SetItem(ref value, FiPropertyName); }
        }
        public static readonly string FiEffPropertyName = "FiEff";
        public XEP_IQuantity FiEff
        {
            get { return GetOneQuantity(FiEffPropertyName); }
            set { SetItem(ref value, FiEffPropertyName); }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        string _name = String.Empty;
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

        #region METHODS
        #endregion
    }
}
