using System;
using System.Collections.Generic;
using System.Linq;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public class XEP_Enum2StringManager : XEP_IEnum2StringManager
    { // is registered as singleton !!
        Dictionary<string, string[]> _stringArrays4Enums = new Dictionary<string, string[]>();

        public XEP_Enum2StringManager()
        {
           // Array
            _stringArrays4Enums.Add(typeof(XEP_eMaterialDiagramType).Name, Enum.GetNames(typeof(XEP_eMaterialDiagramType)));
        }

        #region XEP_IEnum2StringManager
        public string[] GetNames(string enumName)
        {
            if (_stringArrays4Enums.ContainsKey(enumName))
            {
                return _stringArrays4Enums[enumName];
            }
            return null;
        }
        public Int32 GetValue(string enumName, string enumStringValue)
        {
            if (enumName == null || enumName.Count() == 0 || enumStringValue == null || enumStringValue.Count() == 0)
            {
                return -1;
            }
            string[] names = GetNames(enumName);
            if (names == null)
            {
                return -1;
            }
            return Array.IndexOf(names, enumStringValue);
        }
        #endregion


    }
}
