using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using XEP_SectionCheckCommon.ResTrans;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

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

        public static string ConvertDiagramType(XEP_eMaterialDiagramType type, bool longName = true)
        {
            string retVal;
            switch (type)
            {
                case XEP_eMaterialDiagramType.UserInput:
                    default:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("USER_DIAGRAM")) : (XEP_Constants.USER_DIAGRAM_ABR);
                    break;
                case XEP_eMaterialDiagramType.LinTenEcm:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECM")) : (XEP_Constants.LIN_TEN_ECM_ABR);
                    break;
                case XEP_eMaterialDiagramType.LinTenEcff:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECFF")) : (XEP_Constants.LIN_TEN_ECFF_ABR);
                    break;
                case XEP_eMaterialDiagramType.LinEcm:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_ECM")) : (XEP_Constants.LIN_ECM_ABR);
                    break;
                case XEP_eMaterialDiagramType.LinEcff:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("LIN_ECFF")) : (XEP_Constants.LIN_ECFF_ABR);
                    break;
                case XEP_eMaterialDiagramType.BiliUls:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("BILI_ULS")) : (XEP_Constants.BILI_ULS_ABR);
                    break;
                case XEP_eMaterialDiagramType.ParRectUls:
                    retVal = (longName) ? (Resources.ResourceManager.GetString("PAR_RECT_ULS")) : (XEP_Constants.PAR_RECT_ULS_ABR);
                    break;
            }
            return retVal;
        }
        public static XEP_eMaterialDiagramType ConvertDiagramType(string type, bool longName = true)
        {
            if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECM")) : (XEP_Constants.LIN_TEN_ECM_ABR)))
            {
                return XEP_eMaterialDiagramType.LinTenEcm;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_TEN_ECFF")) : (XEP_Constants.LIN_TEN_ECFF_ABR)))
            {
                return XEP_eMaterialDiagramType.LinTenEcff;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_ECM")) : (XEP_Constants.LIN_ECM_ABR)))
            {
                return XEP_eMaterialDiagramType.LinEcm;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("LIN_ECFF")) : (XEP_Constants.LIN_ECFF_ABR)))
            {
                return XEP_eMaterialDiagramType.LinEcff;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("BILI_ULS")) : (XEP_Constants.BILI_ULS_ABR)))
            {
                return XEP_eMaterialDiagramType.BiliUls;
            }
            else if (type == ((longName) ? (Resources.ResourceManager.GetString("PAR_RECT_ULS")) : (XEP_Constants.PAR_RECT_ULS_ABR)))
            {
                return XEP_eMaterialDiagramType.ParRectUls;
            }
            else
            {
                return XEP_eMaterialDiagramType.UserInput;
            }
        }
    }
}
