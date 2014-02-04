using System;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IEnum2StringManager
    {
        string[] GetNames(string enumName);
        Int32 GetValue(string enumName, string enumStringValue);
    }
}
