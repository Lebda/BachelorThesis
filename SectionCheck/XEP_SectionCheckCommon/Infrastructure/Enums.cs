using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public enum eEP_ForceItemType
    {
        eULS,
        eSLS,
    }

    public enum eEP_ForceType
    {
        eN = 1, // has to be first,
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
}
