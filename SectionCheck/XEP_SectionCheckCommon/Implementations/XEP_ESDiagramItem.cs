using System;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_ESDiagramItemXml : XEP_XmlWorkerImpl
    {
        readonly XEP_ESDiagramItem _data = null;
        public XEP_ESDiagramItemXml(XEP_ESDiagramItem data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_ESDiagramItem";
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_ESDiagramItem.StressPropertyName, _data.Stress.Value));
            xmlElement.Add(new XAttribute(ns + XEP_ESDiagramItem.StrainPropertyName, _data.Strain.Value));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Stress.Value = (double)xmlElement.Attribute(ns + XEP_ESDiagramItem.StressPropertyName);
            _data.Strain.Value = (double)xmlElement.Attribute(ns + XEP_ESDiagramItem.StrainPropertyName);
        }
        #endregion
    }

    [Serializable]
    public class XEP_ESDiagramItem : ObservableObject, XEP_IESDiagramItem
    {
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        string _name = "StressStrainDiagramPoint";

        public XEP_ESDiagramItem(XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_ESDiagramItemXml(this);
            _strain = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, StrainPropertyName);
            _stress = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStress, StressPropertyName);
        }
        #region XEP_IESDiagramItem Members
        public const string StrainPropertyName = "Strain";
        private XEP_IQuantity _strain = null;
        public XEP_IQuantity Strain
        {
            get
            {
                return _strain;
            }
            set
            {
                if (_strain == value)
                {
                    return;
                }
                _strain = value;
                RaisePropertyChanged(StrainPropertyName);
            }
        }
        public const string StressPropertyName = "Stress";
        private XEP_IQuantity _stress = null;
        public XEP_IQuantity Stress
        {
            get
            {
                return _stress;
            }
            set
            {
                if (_stress == value)
                {
                    return;
                }
                _stress = value;
                RaisePropertyChanged(StressPropertyName);
            }
        }
        #endregion
        #region XEP_IDataCacheObjectBase Members
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        #endregion
    }
}
