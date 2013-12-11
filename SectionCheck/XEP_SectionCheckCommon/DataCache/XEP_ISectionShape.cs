﻿using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_ISectionShape : XEP_IDataCacheObjectBase, XEP_IContainerHolder
    {
        ObservableCollection<XEP_ISectionShapeItem> ShapeOuter { get; set; }
        ObservableCollection<XEP_ISectionShapeItem> ShapeInner { get; set; }
    }
}