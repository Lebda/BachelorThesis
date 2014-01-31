using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_SectionCheckCommon.ResTrans;
using XEP_SectionCheckCommon.Infrastucture;
using System.Windows.Media;
using XEP_SectionCheckCommon.DataCache;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public static class XEP_Conventors
    {
        static public PointCollection TransformOne(ObservableCollection<XEP_ISectionShapeItem> shape)
        {
            PointCollection points = new PointCollection();
            foreach (XEP_ISectionShapeItem item in shape)
            {
                points.Add(new System.Windows.Point(item.Y.Value, item.Z.Value));
            }
            return points;
        }

        public static string ConvertDiagramType(eEP_MaterialDiagramType type, bool longName = true)
        {
            string retVal;
            switch (type)
            {
                case eEP_MaterialDiagramType.eUserInput:
                    default:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("USER_DIAGRAM")) : (XEP_Constants.USER_DIAGRAM_ABR);
                    break;
                case eEP_MaterialDiagramType.eLinTenEcm:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECM")) : (XEP_Constants.LIN_TEN_ECM_ABR);
                    break;
                case eEP_MaterialDiagramType.eLinTenEcff:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECFF")) : (XEP_Constants.LIN_TEN_ECFF_ABR);
                    break;
                case eEP_MaterialDiagramType.eLinEcm:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_ECM")) : (XEP_Constants.LIN_ECM_ABR);
                    break;
                case eEP_MaterialDiagramType.eLinEcff:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_ECFF")) : (XEP_Constants.LIN_ECFF_ABR);
                    break;
                case eEP_MaterialDiagramType.eBiliUls:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("BILI_ULS")) : (XEP_Constants.BILI_ULS_ABR);
                    break;
                case eEP_MaterialDiagramType.eParRectUls:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("PAR_RECT_ULS")) : (XEP_Constants.PAR_RECT_ULS_ABR);
                    break;
            }
            return retVal;
        }
        public static eEP_MaterialDiagramType ConvertDiagramType(string type, bool longName = true)
        {
            if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECM")) : (XEP_Constants.LIN_TEN_ECM_ABR)))
            {
                return eEP_MaterialDiagramType.eLinTenEcm;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECFF")) : (XEP_Constants.LIN_TEN_ECFF_ABR)))
            {
                return eEP_MaterialDiagramType.eLinTenEcff;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_ECM")) : (XEP_Constants.LIN_ECM_ABR)))
            {
                return eEP_MaterialDiagramType.eLinEcm;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_ECFF")) : (XEP_Constants.LIN_ECFF_ABR)))
            {
                return eEP_MaterialDiagramType.eLinEcff;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("BILI_ULS")) : (XEP_Constants.BILI_ULS_ABR)))
            {
                return eEP_MaterialDiagramType.eBiliUls;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("PAR_RECT_ULS")) : (XEP_Constants.PAR_RECT_ULS_ABR)))
            {
                return eEP_MaterialDiagramType.eParRectUls;
            }
            else
            {
                return eEP_MaterialDiagramType.eUserInput;
            }
        }
    }
}
