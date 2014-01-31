using System;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
    class XEP_QuantityXml : XEP_XmlWorkerImpl
    {
        readonly XEP_Quantity _data = null;
        public XEP_QuantityXml(XEP_Quantity data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_Quantity";
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
            xmlElement.Add(new XAttribute(ns + "QuantityType", (int)_data.QuantityType));
            xmlElement.Add(new XAttribute(ns + "Value", _data.Value));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        protected override void LoadAtributes( XElement xmlElement )
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            _data.QuantityType = (eEP_QuantityType)(int)xmlElement.Attribute(ns + "QuantityType");
            _data.Value = (double)xmlElement.Attribute(ns + "Value");
        }
        #endregion
    }

    [Serializable]
    public class XEP_Quantity : XEP_ObservableObject, XEP_IQuantity
    {
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        public XEP_Quantity(XEP_IQuantityManager manager, double value, eEP_QuantityType type, string name)
        {
            _xmlWorker = new XEP_QuantityXml(this);
            _manager = manager;
            _value = value;
            _quantityType = type;
            _name = name;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            XEP_Quantity p = obj as XEP_Quantity;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (MathUtils.CompareDouble(Value, p.Value) &&
                (QuantityType == p.QuantityType));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region XEP_IQuantityManagerHolder Members

        public XEP_IXmlWorker XmlWorker
        {
            get
            {
                return this._xmlWorker;
            }
            set
            {
                this._xmlWorker = value;
            }
        }

        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        #endregion

        /// <summary>
        /// The <see cref="ManagedValue" /> property's name.
        /// </summary>
        public const string ManagedValuePropertyName = "ManagedValue";

        /// <summary>
        /// Sets and gets the ManagedValue property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double ManagedValue
        {
            get
            {
                return Manager.GetValue(this);
            }
            set
            {
                if (Manager.GetValue(this) == value)
                {
                    return;
                }
                _value = Manager.GetValueManaged(value, _quantityType);
                RaisePropertyChanged(ValuePropertyName);
                RaisePropertyChanged(ManagedValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Value" /> property's name.
        /// </summary>
        public const string ValuePropertyName = "Value";

        private double _value = 0.0;

        /// <summary>
        /// Sets and gets the Value property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (_value == value)
                {
                    return;
                }
                _value = value;
                RaisePropertyChanged(ValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="QuantityType" /> property's name.
        /// </summary>
        public const string QuantityTypePropertyName = "QuantityType";

        private eEP_QuantityType _quantityType = eEP_QuantityType.eNoType;

        /// <summary>
        /// Sets and gets the QuantityType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public eEP_QuantityType QuantityType
        {
            get
            {
                return _quantityType;
            }

            set
            {
                if (_quantityType == value)
                {
                    return;
                }
                _quantityType = value;
                RaisePropertyChanged(QuantityTypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = "";
        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        Guid _guid = Guid.NewGuid();
        public Guid Id
        {
            get { return _guid; }
            set { SetMember<Guid>(ref value, ref _guid, (_guid == value), XEP_Constants.GuidPropertyName); }
        }
    }

    [Serializable]
    public class XEP_QuantityDefinition
    {
        double _scale = 1.0;
        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        string _quantityName = "";
        public string QuantityName
        {
            get { return _quantityName; }
            set { _quantityName = value; }
        }
        string _quantityNameScale = "";
        public string QuantityNameScale
        {
            get { return _quantityNameScale; }
            set { _quantityNameScale = value; }
        }
    }
}
