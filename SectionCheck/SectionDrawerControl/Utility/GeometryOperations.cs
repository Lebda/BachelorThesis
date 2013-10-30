using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SectionDrawerControl.Utility
{
    public static class GeometryOperations
    {
        static public void TransformOne(Matrix conventer, PointCollection shape)
        {
            for (int counter = 0; counter < shape.Count; ++counter)
            {
                shape[counter] = conventer.Transform(shape[counter]);
            }
        }
        public static double Add4Sign(double val1, double val2)
        {
            return (val1 + ((val1 > 0.0) ? (val2) : (-val2)));
        }
        public static double Take4Sign(double val1, double val2)
        {
            return Add4Sign(val1, -val2);
        }
    }
}
