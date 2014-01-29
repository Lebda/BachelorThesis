using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
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
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
            foreach (var item in _data.Data)
            {
                xmlElement.Add(new XAttribute(ns + item.Value.Name, item.Value.Value));
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            foreach (var item in _data.Data)
            {
                item.Value.Value = (double)xmlElement.Attribute(ns + item.Value.Name);
            }
        }
        #endregion
    }

    [Serializable]
    public class XEP_SetupParameters : ObservableObject, XEP_ISetupParameters
    {
        Dictionary<string, XEP_IQuantity> _data = new Dictionary<string, XEP_IQuantity>();
        public Dictionary<string, XEP_IQuantity> Data
        {
            get { return _data; }
        }

        public XEP_SetupParameters(XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_SetupParametersXml(this);
            _data.Add(GammaCPropertyName, XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, GammaCPropertyName));
            _data.Add(GammaSPropertyName, XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, GammaSPropertyName));
            _data.Add(AlphaCcPropertyName, XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, AlphaCcPropertyName));
            _data.Add(AlphaCtPropertyName, XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, AlphaCtPropertyName));
            _data.Add(FiPropertyName, XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, FiPropertyName));
            _data.Add(FiEffPropertyName, XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, FiEffPropertyName));
        }

        #region XEP_ISetupParameters Members
        public static readonly string GammaCPropertyName = "GammaC";
        public XEP_IQuantity GammaC
        {
            get { return _data[GammaCPropertyName]; }
            set
            {
                if (_data[GammaCPropertyName] == value) { return; }
                SetItem(ref value, ref _data, GammaCPropertyName, GammaCPropertyName);
            }
        }
        public static readonly string GammaSPropertyName = "GammaS";
        public XEP_IQuantity GammaS
        {
            get { return _data[GammaSPropertyName]; }
            set
            {
                if (_data[GammaCPropertyName] == value) { return; }
                SetItem(ref value, ref _data, GammaSPropertyName, GammaSPropertyName);
            }
        }
        public static readonly string AlphaCcPropertyName = "AlphaCc";
        public XEP_IQuantity AlphaCc
        {
            get { return _data[AlphaCcPropertyName]; }
            set
            {
                if (_data[AlphaCcPropertyName] == value) { return; }
                SetItem(ref value, ref _data, AlphaCcPropertyName, AlphaCcPropertyName);
            }
        }
        public static readonly string AlphaCtPropertyName = "AlphaCt";
        public XEP_IQuantity AlphaCt
        {
            get { return _data[AlphaCtPropertyName]; }
            set
            {
                if (_data[AlphaCtPropertyName] == value) { return; }
                SetItem(ref value, ref _data, AlphaCtPropertyName, AlphaCtPropertyName);
            }
        }
        public static readonly string FiPropertyName = "Fi";
        public XEP_IQuantity Fi
        {
            get { return _data[FiPropertyName]; }
            set
            {
                if (_data[FiPropertyName] == value) { return; }
                SetItem(ref value, ref _data, FiPropertyName, FiPropertyName);
            }
        }
        public static readonly string FiEffPropertyName = "FiEff";
        public XEP_IQuantity FiEff
        {
            get { return _data[FiEffPropertyName]; }
            set
            {
                if (_data[FiEffPropertyName] == value) { return; }
                SetItem(ref value, ref _data, FiEffPropertyName, FiEffPropertyName);
            }
        }

        #endregion
        #region XEP_IDataCacheObjectBase Members
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = String.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        #region METHODS
        private void SetItem(ref XEP_IQuantity valueFromBinding, ref Dictionary<string, XEP_IQuantity> data, string index, params string[] names)
        {
            if (data[index] == valueFromBinding || !SetItemFromBinding(ref valueFromBinding, data[index]))
            {
                return;
            }
            data[index] = valueFromBinding;
            foreach (string item in names)
            {
                RaisePropertyChanged(item);
            }
        }
        private bool SetItemFromBinding(ref XEP_IQuantity valueFromBinding, XEP_IQuantity propertyItem)
        {
            if (valueFromBinding == null)
            {
                return false;
            }
            if (valueFromBinding.Manager == null && string.IsNullOrEmpty(valueFromBinding.Name) && valueFromBinding.QuantityType == eEP_QuantityType.eNoType)
            { // setting throw binding
                valueFromBinding.Manager = propertyItem.Manager;
                valueFromBinding.Name = propertyItem.Name;
                valueFromBinding.QuantityType = propertyItem.QuantityType;
                valueFromBinding.Value = Manager.GetValueManaged(valueFromBinding.Value, valueFromBinding.QuantityType);
                if (valueFromBinding.Value == propertyItem.Value)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
