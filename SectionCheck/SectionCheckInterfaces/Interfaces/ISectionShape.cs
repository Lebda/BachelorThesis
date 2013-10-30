using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SectionCheckInterfaces.Interfaces
{
    public interface ISectionShape
    {
        void Prepare();
        PointCollection CssShapeOuter { get; set; }
        PointCollection CssShapeInner { get; set; }
        PointCollection ReinforcementShape { get; set; }
        void TansformAll(Matrix conventer);
        //
        PointCollection TestShape { get; set; }
    }
}
