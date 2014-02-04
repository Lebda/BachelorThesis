using System;
using System.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;
using System.Windows;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IQuantity : XEP_IDataCacheObjectBase, ICloneable
    {
        // Properties
        XEP_IDataCacheObjectBase Owner { get; set; }
        double Value { get; set; }
        double ManagedValue { get; set; }
        eEP_QuantityType QuantityType { get; set; }
        Visibility VisibleState { get; set; }
        bool IsReadOnly { get; set; }
        string EnumName { get; set; }
        XEP_IQuantityManager Manager { get; set; }
        XEP_IEnum2StringManager Enum2StringManager { get; set; }
        // Methods
        bool IsTrue();
        void SetBool(bool value);
        Tenum GetEnumValue<Tenum>()
        where Tenum : struct;
        void SetEnumValue(Enum newValue);
    }
}
