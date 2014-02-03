﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using XEP_CommonLibrary.Utility;
using System.Collections.ObjectModel;
using XEP_Prism.Infrastructure;

namespace XEP_SectionCheckCommon.Infrastructure
{
    static public class XEP_ViewModelHelp
    {
        public static ObservableCollection<XEP_ISectionShapeItem> CreateRectShape(XEP_IResolver<XEP_ISectionShapeItem> resolver, double bPos, double hPos, bool outerDir)
        {
            Exceptions.CheckNull(resolver);
            ObservableCollection<XEP_ISectionShapeItem> shape = new ObservableCollection<XEP_ISectionShapeItem>();
            if (MathUtils.IsLessOrEqual(bPos, 0.0) || MathUtils.IsLessOrEqual(hPos, 0.0))
            {
                return shape;
            }
            XEP_ISectionShapeItem item = resolver.Resolve();
            item.Y.Value = bPos;
            item.Z.Value = -hPos;
            item.Type = (outerDir) ? (eEP_CssShapePointType.eOuter) : (eEP_CssShapePointType.eInner);
            shape.Add(item);
            item = resolver.Resolve();
            item.Y.Value = (outerDir) ? (bPos) : (-bPos);
            item.Z.Value = (outerDir) ? (hPos) : (-hPos);
            item.Type = (outerDir) ? (eEP_CssShapePointType.eOuter) : (eEP_CssShapePointType.eInner);
            shape.Add(item);
            item = resolver.Resolve();
            item.Y.Value = (outerDir) ? (-bPos) : (-bPos);
            item.Z.Value = (outerDir) ? (hPos) : (hPos);
            item.Type = (outerDir) ? (eEP_CssShapePointType.eOuter) : (eEP_CssShapePointType.eInner);
            shape.Add(item);
            item = resolver.Resolve();
            item.Y.Value = (outerDir) ? (-bPos) : (bPos);
            item.Z.Value = (outerDir) ? (-hPos) : (hPos);
            item.Type = (outerDir) ? (eEP_CssShapePointType.eOuter) : (eEP_CssShapePointType.eInner);
            shape.Add(item);
            return shape;
        }
    }
}
