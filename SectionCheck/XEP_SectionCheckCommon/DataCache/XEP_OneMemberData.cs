using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Utility;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    [Serializable]
    public class XEP_OneMemberData : XEP_IOneMemberData
    {
        Dictionary<Guid, XEP_IOneSectionData> _sectionData = new Dictionary<Guid, XEP_IOneSectionData>();
        readonly Guid _guid = Guid.NewGuid();

        #region XEP_IOneMemberData Members
        public Guid Id
        {
            get { return _guid; }
        }
        public XEP_IOneSectionData GetOneSectionData(Guid guid)
        {
            return Common.GetDataDictionary<Guid, XEP_IOneSectionData>(guid, _sectionData);
        }
        public Dictionary<Guid, XEP_IOneSectionData> GetSectionsData()
        {
            return _sectionData;
        }
        public eDataCacheServiceOperation SaveOneSectionData(XEP_IOneSectionData sectionData)
        {
            Exceptions.CheckNull(sectionData);
            if (_sectionData.ContainsKey(sectionData.Id))
            {
                _sectionData.Remove(sectionData.Id);
            }
            _sectionData.Add(sectionData.Id, sectionData);
            return eDataCacheServiceOperation.eSuccess;
        }
        public eDataCacheServiceOperation RemoveOneSectionData(XEP_IOneSectionData sectionData)
        {
            Exceptions.CheckNull(sectionData);
            if (_sectionData.ContainsKey(sectionData.Id))
            {
                _sectionData.Remove(sectionData.Id);
                return eDataCacheServiceOperation.eSuccess;
            }
            return eDataCacheServiceOperation.eNotFound;
        }
        #endregion
    }
}
