using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.DataCache;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Implementations
{
    public class XEP_DataCache : XEP_IDataCache
    {
        readonly Dictionary<Guid, XEP_IOneMemberData> _memberData = new Dictionary<Guid, XEP_IOneMemberData>();

        #region XEP_IDataCacheService Members
        public void Clear()
        {
            _memberData.Clear();
        }
        public Dictionary<Guid, XEP_IOneMemberData> GetMemberData()
        {
            return _memberData;
        }
        public XEP_IOneMemberData GetOneMemberData(Guid guid)
        {
            return Common.GetDataDictionary<Guid, XEP_IOneMemberData>(guid, _memberData);
        }
        public eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData)
        {
            Exceptions.CheckNull(memberData);
            if (_memberData.ContainsKey(memberData.Id))
            {
                _memberData.Remove(memberData.Id);
            }
            _memberData.Add(memberData.Id, memberData);
            return eDataCacheServiceOperation.eSuccess;
        }
        public eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData)
        {
            Exceptions.CheckNull(memberData);
            if (_memberData.ContainsKey(memberData.Id))
            {
                _memberData.Remove(memberData.Id);
                return eDataCacheServiceOperation.eSuccess;
            }
            return eDataCacheServiceOperation.eNotFound;
        }
        #endregion
    }
}
