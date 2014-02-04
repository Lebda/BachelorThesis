using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{

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

    public class XEP_Quantity : XEP_ObservableObject, XEP_IQuantity
    {
        public XEP_Quantity(XEP_IQuantityManager manager, XEP_IEnum2StringManager enum2StringManager)
        {
            _xmlWorker = new XEP_QuantityXml(this);
            _manager = manager;
            _enum2StringManager = enum2StringManager;
        }

        #region ICloneable Members
        public object Clone()
        { // do not copy owner has to be set from outside !
            XEP_IQuantity data = XEP_QuantityFactory.Instance().Create(_value, _quantityType, _name, _enumName);
            data.VisibleState = _visibleState;
            data.IsReadOnly = _isReadOnly;
            return data;
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        string _name = "Quantity";
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
        public override ObservableCollection<XEP_IQuantity> Data
        {
            get { return null; }
            set { return; }
        }
        [field: NonSerialized]
        XEP_IXmlWorker _xmlWorker = null;
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        [field: NonSerialized]
        XEP_IQuantityManager _manager = null;
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        #endregion

        static public object GetAsUnderlyingType(Enum enval)
        {
            Type entype = enval.GetType();

            Type undertype = Enum.GetUnderlyingType(entype);

            return Convert.ChangeType(enval, undertype);
        }

        #region XEP_IQuantity Members
        public void SetEnumValue(Enum newValue)
        {
            Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not enum type !", _quantityType, (param => param != eEP_QuantityType.eEnum));
            _value = (int)GetAsUnderlyingType(newValue);
        }
        public Tenum GetEnumValue<Tenum>()
        where Tenum : struct
        {
            Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not enum type !", _quantityType, (param => param != eEP_QuantityType.eEnum));
            return (Tenum)Enum.ToObject(typeof(Tenum), (long)_value);
        }
        public bool IsTrue()
        {
            Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not bool type !", _quantityType, (param => param != eEP_QuantityType.eBool));
            return MathUtils.GetBoolFromDouble(_value);
        }
        public void SetBool(bool value)
        {
            Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not bool type !", _quantityType, (param => param != eEP_QuantityType.eBool));
            _value = MathUtils.GetDoubleFromBool(value);
        }
        // PROPERTIES
        XEP_IEnum2StringManager _enum2StringManager = null;
        public static readonly string Enum2StringManagerPropertyName = "Enum2StringManager";
        public XEP_IEnum2StringManager Enum2StringManager
        {
            get { return _enum2StringManager; }
            set { SetMember<XEP_IEnum2StringManager>(ref value, ref _enum2StringManager, (_enum2StringManager == value), Enum2StringManagerPropertyName); }
        }
        string _enumName = null;
        public static readonly string EnumNamePropertyName = "EnumName";
        public string EnumName
        {
            get { return _enumName; }
            set { SetMember<string>(ref value, ref _enumName, (_enumName == value), EnumNamePropertyName); }
        }
        Visibility _visibleState = Visibility.Visible;
        public static readonly string VisibleStatePropertyName = "VisibleState";
        public System.Windows.Visibility VisibleState
        {
            get { return _visibleState; }
            set { SetMember<Visibility>(ref value, ref _visibleState, (_visibleState == value), VisibleStatePropertyName); }
        }
        bool _isReadOnly = false;
        public static readonly string IsReadOnlyPropertyName = "IsReadOnly";
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetMember<bool>(ref value, ref _isReadOnly, (_isReadOnly == value), IsReadOnlyPropertyName); }

        }
        XEP_IDataCacheObjectBase _owner = null;
        public static readonly string OwnerPropertyName = "Owner";
        public XEP_IDataCacheObjectBase Owner
        {
            get { return _owner; }
            set { SetMember<XEP_IDataCacheObjectBase>(ref value, ref _owner, (_owner == value), OwnerPropertyName); }
        }
        public static readonly string ManagedValuePropertyName = "ManagedValue";
        public double ManagedValue
        {
            get { return Manager.GetValue(this); }
            set
            {
                RaisePropertyChanged(ManagedValuePropertyName); // I need it
                if (ManagedValue == value)
                {
                    return;
                }
                if (Owner != null)
                {
                    if (Owner.CallPropertySet4NewManagedValue(_name, value))
                    {
                        RaisePropertyChanged(ValuePropertyName);
                    }
                }
                else
                {
                    _value = Manager.GetValueManaged(value, _quantityType);
                    RaisePropertyChanged(ValuePropertyName);
                }
            }
        }
        public static readonly string ValuePropertyName = "Value";
        private double _value = 0.0;
        public double Value
        {
            get { return _value; }
            set { SetMember<double>(ref value, ref _value, (_value == value), ValuePropertyName); }
        }
        public static readonly string QuantityTypePropertyName = "QuantityType";
        private eEP_QuantityType _quantityType = eEP_QuantityType.eNoType;
        public eEP_QuantityType QuantityType
        {
            get { return _quantityType; }
            set { SetMember<eEP_QuantityType>(ref value, ref _quantityType, (_quantityType == value), QuantityTypePropertyName); }
        }
        #endregion

        #region METHODS
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            XEP_Quantity p = obj as XEP_Quantity;
            if ((System.Object)p == null)
            {
                return false;
            }
            return (MathUtils.CompareDouble(Value, p.Value) && (QuantityType == p.QuantityType));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
