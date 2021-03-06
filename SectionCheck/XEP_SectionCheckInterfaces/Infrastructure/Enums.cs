﻿using System;
using System.Linq;

namespace XEP_SectionCheckInterfaces.Infrastructure
{
    [Flags] //Each element should contain name and a value (which should be degree of 2 – 1, 2, 4, 8, 16, etc.). 
    public enum eEP_ForceItemType
    { //Do not define values with 0 and -1 values if you do not mean to use them as select no flag and select all flags. 
        eULS = 1,
        eSLS = 2,
    }

    public enum XEP_eMaterialDiagramType
    { // No convention for enums I need enums as strings !!!!!!!!!!
        UserInput = 0,
        LinTenEcm,
        LinTenEcff,
        LinEcm,
        LinEcff,
        BiliUls,
        ParRectUls,
    }

    public enum eEP_ForceType
    {
        eN = 0, // has to be first,
        eVy, // do not change
        eVz, // do not change
        eMx, // do not change
        eMy, // do not change
        eMz, // do not change
        eForceTypeCount // has to be last
    }

    public enum eEP_QuantityType
    {
        eNoType = 0, // has to be first
        eBool,
        eEnum,
        eString,
        eNoUnit,
        eCssLength,
        eForce,
        eMoment,
        eStress,
        eStrain,
        eQuantityTypeCount // has to be last
    }

    public enum eDataCacheServiceOperation
    {
        eFailed = 0,
        eSuccess,
        eNotFound,
    }

    public enum eEP_CssShapePointType
    {
        eOuter = 0,
        eInner,
    }
}
