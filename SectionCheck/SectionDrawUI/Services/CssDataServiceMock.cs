using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SectionDrawerControl.Interfaces;
using SectionDrawerControl.Infrastructure;

namespace SectionDrawUI.Services
{
    public class CssDataServiceMock : ICssDataService
    {
        public CssDataReinforcement GetCssDataReinforcement()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CssDataFibers GetCssDataFibersReinforcement()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CssDataShape GetCssDataShape()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CssDataCompressPart GetCssDataCompressPart()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CssDataAxis GetCssDataAxisHorizontal()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CssDataAxis GetCssDataAxisVertical()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CssDataFibers GetCssDataFibersConcrete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
