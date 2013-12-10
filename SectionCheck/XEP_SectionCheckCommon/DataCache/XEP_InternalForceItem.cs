using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Infrastructure;
using System.ComponentModel.DataAnnotations;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Utility;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Infrastructure;
using Microsoft.Practices.Unity;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_InternalForceItemXml : XEP_XmlWorkerImpl
    {
        public static readonly string XmlName = "XEP_InternalForceItem";
        readonly XEP_InternalForceItem _data = null;
        public XEP_InternalForceItemXml(XEP_InternalForceItem data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return XmlName;
        }
        protected override string GetXmlElementComment()
        {
            return "One force on section";
        }
        protected override void AddElements(XElement xmlElement)
        {
            for (int counter = (int)eEP_ForceType.eN; counter < (int)eEP_ForceType.eForceTypeCount; ++counter)
            {
                xmlElement.Add(_data.GetItem((eEP_ForceType)counter).XmlWorker.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
            xmlElement.Add(new XAttribute(ns + "Type", (int)_data.Type));
            xmlElement.Add(new XAttribute(ns + "UsedInCheck", _data.UsedInCheck));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlForces = xmlElement.Elements(ns + _data.GetItem(eEP_ForceType.eN).XmlWorker.GetXmlElementName());
            Exceptions.CheckPredicate<int, int>("Invalid XML file", xmlForces.Count(), (int)eEP_ForceType.eForceTypeCount, (xmlCount, itemCount) => xmlCount < itemCount);
            for (int counter = (int)eEP_ForceType.eN; counter < (int)eEP_ForceType.eForceTypeCount; ++counter)
            {
                XElement xmlForce = Exceptions.CheckNull<XElement>(xmlForces.ElementAt(counter), "Invalid definition of board field in XML file");
                _data.GetItem((eEP_ForceType)counter).XmlWorker.LoadFromXmlElement(xmlForce);
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            _data.Type = (eEP_ForceItemType )(int)xmlElement.Attribute(ns + "Type");
            _data.UsedInCheck = (bool)xmlElement.Attribute(ns + "UsedInCheck");
        }
        #endregion
    }

    [Serializable]
    public class XEP_InternalForceItem : ObservableObject, XEP_IInternalForceItem
    {
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;

        public XEP_InternalForceItem( XEP_IQuantityManager manager)
        {
            _manager = Exceptions.CheckNull<XEP_IQuantityManager>(manager);
            _xmlWorker = new XEP_InternalForceItemXml(this);
            _N = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eForce, NPropertyName);
            _Vy = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eForce, VyPropertyName);
            _Vz = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eForce, VzPropertyName);
            _Mx = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eMoment, MxPropertyName);
            _My = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eMoment, MyPropertyName);
            _Mz = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eMoment, MzPropertyName);
            _name = "Force";
        }

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

        #region METHODS
        public XEP_InternalForceItem CopyInstance()
        {
            XEP_InternalForceItem newItem = new XEP_InternalForceItem(_manager);
            newItem._N.Value = _N.Value;
            newItem._Vy.Value = _Vy.Value;
            newItem._Vz.Value = _Vz.Value;
            newItem._Mx.Value = _Mx.Value;
            newItem._My.Value = _My.Value;
            newItem._Mz.Value = _Mz.Value;
            newItem.Name = _name;
            return newItem;
        }
        public XEP_IQuantity GetItem(eEP_ForceType type)
        {
            switch (type)
            {
                case eEP_ForceType.eN:
                default:
                    return N;
                case eEP_ForceType.eVy:
                    return Vy;
                case eEP_ForceType.eVz:
                    return Vz;
                case eEP_ForceType.eMx:
                    return Mx;
                case eEP_ForceType.eMy:
                    return My;
                case eEP_ForceType.eMz:
                    return Mz;
            }
        }
        public XEP_IQuantity GetMax()
        {
            List<XEP_IQuantity> data = new List<XEP_IQuantity>();
            data.Add(_N);
            data.Add(_Vy);
            data.Add(_Vz);
            data.Add(_Mx);
            data.Add(_My);
            data.Add(_Mz);
            return MathUtils.FindMaxValue<XEP_IQuantity>(data, item => item.Value);
        }
        public XEP_IQuantity GetMin()
        {
            List<XEP_IQuantity> data = new List<XEP_IQuantity>();
            data.Add(_N);
            data.Add(_Vy);
            data.Add(_Vz);
            data.Add(_Mx);
            data.Add(_My);
            data.Add(_Mz);
            return MathUtils.FindMinValue<XEP_IQuantity>(data, item => item.Value);
        }
        public string GetString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(XEP_QuantityNames.GetUnitName(Type));
            builder.Append(";");
            builder.Append(" ");
            //
            builder.Append(_manager.GetValue(N));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(_manager.GetValue(Vy));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(_manager.GetValue(Vz));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(_manager.GetValue(Mx));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(_manager.GetValue(My));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(_manager.GetValue(Mz));
            return builder.ToString();
        }
        #endregion

        /// <summary>
        /// The <see cref="MaxValue" /> property's name.
        /// </summary>
        public const string MaxValuePropertyName = "MaxValue";

        /// <summary>
        /// Sets and gets the MaxValue property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double MaxValue
        {
            get
            {
                return Manager.GetValue(GetMax());
            }
            set
            { // should not be called
                Exceptions.CheckNull(null);
                RaisePropertyChanged(MaxValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MinValue" /> property's name.
        /// </summary>
        public const string MinValuePropertyName = "MinValue";

        /// <summary>
        /// Sets and gets the MaxValue property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double MinValue
        {
            get
            {
                return Manager.GetValue(GetMin());
            }
            set
            { // should not be called
                Exceptions.CheckNull(null);
                RaisePropertyChanged(MinValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ShortExplanation" /> property's name.
        /// </summary>
        public const string ShortExplanationPropertyName = "ShortExplanation";

        /// <summary>
        /// Sets and gets the ShortExplanation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ShortExplanation
        {
            get
            {
                return GetString();
            }
            set
            { // should not be called
                Exceptions.CheckNull(null);
                RaisePropertyChanged(ShortExplanationPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="UsedInCheck" /> property's name.
        /// </summary>
        public const string UsedInCheckPropertyName = "UsedInCheck";

        private bool _usedInCheck = false;
        /// <summary>
        /// Sets and gets the UsedInCheck property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool UsedInCheck
        {
            get
            {
                return _usedInCheck;
            }

            set
            {
                if (_usedInCheck == value)
                {
                    return;
                }
                _usedInCheck = value;
                RaisePropertyChanged(UsedInCheckPropertyName);
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
        [Required]
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


        /// <summary>
        /// The <see cref="Type" /> property's name.
        /// </summary>
        public const string TypePropertyName = "Type";

        private eEP_ForceItemType _type = eEP_ForceItemType.eULS;

        /// <summary>
        /// Sets and gets the Type property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        /// 
        [Required]
        public eEP_ForceItemType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type == value)
                {
                    return;
                }
                _type = value;
                RaisePropertyChanged(TypePropertyName);
                RaisePropertyChanged(ShortExplanationPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="N" /> property's name.
        /// </summary>
        public const string NPropertyName = "N";

        private XEP_IQuantity _N = null;

        /// <summary>
        /// Sets and gets the N property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        /// 
        [Required]
        public XEP_IQuantity N
        {
            get
            {
                return _N;
            }
            set
            {
                SetForceItem(ref value, ref _N, NPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName);
            }
        }

        private void SetForceItem(ref XEP_IQuantity valueFromBinding, ref XEP_IQuantity property, params string[] names)
        {
            if (property == valueFromBinding || !SetItemFromBinding(ref valueFromBinding, ref property))
            {
                return;
            }
            property = valueFromBinding;
            foreach(string item in names)
            {
                RaisePropertyChanged(item);
            }
        }
        private bool SetItemFromBinding(ref XEP_IQuantity valueFromBinding, ref XEP_IQuantity propertyItem)
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
        /// <summary>
        /// The <see cref="Vy" /> property's name.
        /// </summary>
        public const string VyPropertyName = "Vy";

        private XEP_IQuantity _Vy = null;

        /// <summary>
        /// Sets and gets the Vy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_IQuantity Vy
        {
            get
            {
                return _Vy;
            }

            set
            {
                SetForceItem(ref value, ref _Vy, VyPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Vz" /> property's name.
        /// </summary>
        public const string VzPropertyName = "Vz";

        private XEP_IQuantity _Vz = null;

        /// <summary>
        /// Sets and gets the Vz property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_IQuantity Vz
        {
            get
            {
                return _Vz;
            }

            set
            {
                SetForceItem(ref value, ref _Vz, VzPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Mx" /> property's name.
        /// </summary>
        public const string MxPropertyName = "Mx";

        private XEP_IQuantity _Mx = null;

        /// <summary>
        /// Sets and gets the Mx property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_IQuantity Mx
        {
            get
            {
                return _Mx;
            }
            set
            {
                SetForceItem(ref value, ref _Mx, MxPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="My" /> property's name.
        /// </summary>
        public const string MyPropertyName = "My";

        private XEP_IQuantity _My = null;

        /// <summary>
        /// Sets and gets the My property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_IQuantity My
        {
            get
            {
                return _My;
            }
            set
            {
                SetForceItem(ref value, ref _My, MyPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Mz" /> property's name.
        /// </summary>
        public const string MzPropertyName = "Mz";

        private XEP_IQuantity _Mz = null;

        /// <summary>
        /// Sets and gets the Mz property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_IQuantity Mz
        {
            get
            {
                return _Mz;
            }
            set
            {
                SetForceItem(ref value, ref _Mz, MzPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName);
            }
        }
    }
}
