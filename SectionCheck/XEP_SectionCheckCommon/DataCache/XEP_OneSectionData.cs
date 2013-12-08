using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Infrastructure;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    [Serializable]
    public class XEP_OneSectionData : XEP_IOneSectionData
    {
        readonly Guid _guid = Guid.NewGuid();
        public Guid Id
        {
            get { return _guid; }
        }

        #region XEP_IOneSectionData
        private string _sectionName = String.Empty;
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        //
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;
        public ObservableCollection<XEP_IInternalForceItem> InternalForces
        {
            get { return _internalForces; }
            set { _internalForces = value; }
        }
        #endregion

    }
}
