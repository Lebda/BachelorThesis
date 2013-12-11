using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XEP_SectionCheckCommon.Infrastructure
{
    [Flags] //Each element should contain name and a value (which should be degree of 2 – 1, 2, 4, 8, 16, etc.). 
    public enum eEP_ForceItemType
    { //Do not define values with 0 and -1 values if you do not mean to use them as select no flag and select all flags. 
        eULS = 1,
        eSLS = 2,
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
        eForce,
        eMoment,
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
