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
        public XEP_QuantityXml(XEP_IQuantity data)
            : base(data)
        {
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_Quantity";
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_IQuantity customer = GetXmlCustomer<XEP_IQuantity>();
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, customer.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Quantity.QuantityTypePropertyName, (int)customer.QuantityType));
            if (customer.QuantityType == eEP_QuantityType.eString)
            {
                xmlElement.Add(new XAttribute(ns + XEP_Quantity.ValueNamePropertyName, customer.ValueName));
            }
            else
            {
                xmlElement.Add(new XAttribute(ns + XEP_Quantity.ValuePropertyName, customer.Value));
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        protected override void LoadAtributes( XElement xmlElement )
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_IQuantity customer = GetXmlCustomer<XEP_IQuantity>();
            customer.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            customer.QuantityType = (eEP_QuantityType)(int)xmlElement.Attribute(ns + XEP_Quantity.QuantityTypePropertyName);
            if (customer.QuantityType == eEP_QuantityType.eString)
            {
                customer.ValueName = (string)xmlElement.Attribute(ns + XEP_Quantity.ValueNamePropertyName);
            }
            else
            {
                customer.Value = (double)xmlElement.Attribute(ns + XEP_Quantity.ValuePropertyName);
            }
        }
        #endregion
    }

    public class XEP_Quantity : XEP_ObservableObject, XEP_IQuantity
    {
        public XEP_Quantity(XEP_IQuantityManager manager, XEP_IEnum2StringManager enum2StringManager)
        {
            XmlWorker = new XEP_QuantityXml(this);
            Manager = manager;
            _enum2StringManager = enum2StringManager;
            Intergrity(null);
        }

        #region ICloneable Members
        public object Clone()
        { // do not copy owner has to be set from outside !
            XEP_IQuantity data = XEP_QuantityFactory.Instance().Create(_value, _quantityType, _name, null,_enumName, _valueName);
            data.VisibleState = _visibleState;
            data.IsReadOnly = _isReadOnly;
            return data;
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
        public XEP_IXmlWorker XmlWorker {get;set;}
        public XEP_IQuantityManager Manager {get;set;}
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
        public string ManagedValueWithUnit
        {
            get { return ManagedValue.ToString() + " " + Manager.GetNameWithUnit(this); }
            set { }
        }
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
            set 
            {
                Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not enum type !", _quantityType, (param => param != eEP_QuantityType.eEnum));
                SetMember<string>(ref value, ref _enumName, (_enumName == value), EnumNamePropertyName); 
            }
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
        string _valueName = String.Empty;
        public static readonly string ValueNamePropertyName = "ValueName";
        public string ValueName
        {
            get { return _valueName; }
            set 
            {
                Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not string type !", _quantityType, (param => param != eEP_QuantityType.eString));
                SetMember<string>(ref value, ref _valueName, (_valueName == value), ValueNamePropertyName); 
            }
        }
        public static readonly string ValueNameManagedPropertyName = "ValueNameManaged";
        public string ValueNameManaged
        {
            get { return _valueName; }
            set
            {
                Exceptions.CheckPredicate<eEP_QuantityType>("XEP_IQuantity is not string type !", _quantityType, (param => param != eEP_QuantityType.eString));
                RaisePropertyChanged(ValueNameManagedPropertyName); // I need it
                if (_valueName == value)
                {
                    return;
                }
                _valueName = value;
                ManagedValue = _valueName.GetHashCode();
            }
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
            return (MathUtils.CompareDouble(_value, p.Value) && (_quantityType == p.QuantityType));
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
