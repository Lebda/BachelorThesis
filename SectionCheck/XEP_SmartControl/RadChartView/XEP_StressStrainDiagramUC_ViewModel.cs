using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;

namespace XEP_SmartControls
{
    class XEP_StressStrainDiagramUC_ViewModel : XEP_ObservableObject
    {
        private string _seriesTypeUC = "Scatter point";
        public static readonly string SeriesTypeUCPropertyName = "SeriesTypeUC";
        public string SeriesTypeUC
        {
            get { return _seriesTypeUC; }
            set { SetMember<string>(ref value, ref _seriesTypeUC, (_seriesTypeUC == value), SeriesTypeUCPropertyName); }
        }

        XEP_IMaterialData _materialDataUC = null; // singleton
        public static readonly string MaterialDataUCPropertyName = "MaterialDataUC";
        public XEP_IMaterialData MaterialDataUC
        {
            get { return _materialDataUC; }
            set { SetMember<XEP_IMaterialData>(ref value, ref _materialDataUC, (_materialDataUC == value), MaterialDataUCPropertyName); }
        }
    }
}
