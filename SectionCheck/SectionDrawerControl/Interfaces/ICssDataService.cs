using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SectionDrawerControl.Infrastructure;

namespace SectionDrawerControl.Interfaces
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
