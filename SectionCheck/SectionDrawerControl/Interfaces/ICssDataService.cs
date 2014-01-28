using System;
using System.Linq;
using XEP_SectionDrawer.Infrastructure;

namespace XEP_SectionDrawer.Interfaces
{
    public interface ICssDataService
    {
        CssDataAxis GetCssDataAxisHorizontal();
        CssDataAxis GetCssDataAxisVertical();
        CssDataCompressPart GetCssDataCompressPart();
        CssDataReinforcement GetCssDataReinforcement();
        CssDataShape GetCssDataShape();
        CssDataFibers GetCssDataFibersConcrete();
        CssDataFibers GetCssDataFibersReinforcement();
    }
}
