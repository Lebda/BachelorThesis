﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Implementations;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Infrastructure
{
    class XEP_QuantityXml : XEP_XmlWorkerImpl
    {
        XEP_Quantity _data = null;
        public XEP_QuantityXml(XEP_Quantity data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        protected override string GetXmlElementName()
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
        #endregion
    }

    [Serializable]
    public class XEP_Quantity : ObservableObject, XEP_IQuantity
    {
        XEP_XmlWorkerImpl _xmlWorker = null;
        public XEP_Quantity(XEP_IQuantityManager manager, double value, eEP_QuantityType type, string name)
        {
            _xmlWorker = new XEP_QuantityXml(this);
            _managerHolder = new XEP_QuantityManagerHolderImpl(manager);
            _value = value;
            _quantityType = type;
            _name = name;
        }
        XEP_QuantityManagerHolderImpl _managerHolder = null;
        #region XEP_IQuantityManagerHolder Members

        public XEP_IQuantityManager Manager
        {
            get { return _managerHolder.Manager; }
            set { _managerHolder.Manager = value; }
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

        #region XEP_IXmlWorker Members
        XElement XEP_IXmlWorker.GetXmlElement()
        {
            return _xmlWorker.GetXmlElement();
        }
        void XEP_IXmlWorker.LoadFromXmlElement(XElement xmlElement)
        {
            _xmlWorker.LoadFromXmlElement(xmlElement);
        }
        #endregion
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
