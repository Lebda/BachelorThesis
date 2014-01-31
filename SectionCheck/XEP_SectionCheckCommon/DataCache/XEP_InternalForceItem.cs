using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Infrastructure;
using System.ComponentModel.DataAnnotations;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Utility;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Infrastructure;

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
    public class XEP_InternalForceItem : XEP_ObservableObject, XEP_IInternalForceItem
    {
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        XEP_IResolver<XEP_IInternalForceItem> _resolver = null;

        public XEP_InternalForceItem(XEP_IQuantityManager manager, XEP_IResolver<XEP_IInternalForceItem> resolver)
        {
            _manager = Exceptions.CheckNull<XEP_IQuantityManager>(manager);
            _xmlWorker = new XEP_InternalForceItemXml(this);
            resolver = _resolver;
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eForce, NPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eForce, VyPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eForce, VzPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eMoment, MxPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eMoment, MyPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eMoment, MzPropertyName);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            XEP_InternalForceItem p = obj as XEP_InternalForceItem;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (N.Equals(p.N) && Vy.Equals(p.Vy) && Vz.Equals(p.Vz) && Mx.Equals(p.Mx) && My.Equals(p.My) && Mz.Equals(p.Mz));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        string _name = "Internal force";
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

        #region METHODS
        public XEP_IInternalForceItem CopyInstance()
        {
            XEP_IInternalForceItem newItem = _resolver.Resolve();
            newItem.N.Value = N.Value;
            newItem.Vy.Value = Vy.Value;
            newItem.Vz.Value = Vz.Value;
            newItem.Mx.Value = Mx.Value;
            newItem.My.Value = My.Value;
            newItem.Mz.Value = Mz.Value;
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
            data.Add(N);
            data.Add(Vy);
            data.Add(Vz);
            data.Add(Mx);
            data.Add(My);
            data.Add(Mz);
            return MathUtils.FindMaxValue<XEP_IQuantity>(data, item => item.Value);
        }
        public XEP_IQuantity GetMin()
        {
            List<XEP_IQuantity> data = new List<XEP_IQuantity>();
            data.Add(N);
            data.Add(Vy);
            data.Add(Vz);
            data.Add(Mx);
            data.Add(My);
            data.Add(Mz);
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

        public static readonly string MaxValuePropertyName = "MaxValue";
        public double MaxValue
        {
            get { return Manager.GetValue(GetMax()); }
            set { RaisePropertyChanged(MaxValuePropertyName); }
        }
        public static readonly string MinValuePropertyName = "MinValue";
        public double MinValue
        {
            get { return Manager.GetValue(GetMin()); }
            set { RaisePropertyChanged(MinValuePropertyName); }
        }
        public static readonly string ShortExplanationPropertyName = "ShortExplanation";
        public string ShortExplanation
        {
            get { return GetString(); }
            set { RaisePropertyChanged(ShortExplanationPropertyName); }
        }
        public static readonly string UsedInCheckPropertyName = "UsedInCheck";
        private bool _usedInCheck = false;
        public bool UsedInCheck
        {
            get { return _usedInCheck; }
            set { SetMember<bool>(ref value, ref _usedInCheck, (_usedInCheck == value), UsedInCheckPropertyName); }
        }

        public static readonly string TypePropertyName = "Type";
        private eEP_ForceItemType _type = eEP_ForceItemType.eULS;
        public eEP_ForceItemType Type
        {
            get { return _type; }
            set { SetMember<eEP_ForceItemType>(ref value, ref _type, (_type == value), TypePropertyName); }
        }
        public static readonly string NPropertyName = "N";
        public XEP_IQuantity N
        {
            get { return GetOneQuantity(NPropertyName); }
            set { SetItem(ref value, NPropertyName, NPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName); }
        }
        public static readonly string VyPropertyName = "Vy";
        public XEP_IQuantity Vy
        {
            get { return GetOneQuantity(VyPropertyName); }
            set { SetItem(ref value, VyPropertyName, VyPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName); }
        }
        public static readonly string VzPropertyName = "Vz";
        public XEP_IQuantity Vz
        {
            get { return GetOneQuantity(VzPropertyName); }
            set { SetItem(ref value, VzPropertyName, VzPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName); }
        }
        public static readonly string MxPropertyName = "Mx";
        public XEP_IQuantity Mx
        {
            get { return GetOneQuantity(MxPropertyName); }
            set { SetItem(ref value, MxPropertyName, MxPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName); }
        }
        public static readonly string MyPropertyName = "My";
        public XEP_IQuantity My
        {
            get { return GetOneQuantity(MyPropertyName); }
            set { SetItem(ref value, MyPropertyName, MyPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName); }
        }
        public static readonly string MzPropertyName = "Mz";
        public XEP_IQuantity Mz
        {
            get { return GetOneQuantity(MzPropertyName); }
            set { SetItem(ref value, MzPropertyName, MzPropertyName, ShortExplanationPropertyName, MaxValuePropertyName, MinValuePropertyName); }
        }
    }
}
