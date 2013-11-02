using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SectionDrawerControl.Utility
{
    public class SectionDrawerConventer
    {
        public SectionDrawerConventer(Matrix conventerShape, Matrix conventerStressStrain, double scale4bars = 1.0, double scale4Stress = 1.0, double scale4Strain = 1.0)
        {
            _conventerShape = conventerShape;
            _conventerStressStrain = conventerStressStrain;
            _scale4Bars = scale4bars;
            _scale4Strain = scale4Strain;
            _scale4Stress = scale4Stress;
        }
        Matrix _conventerStressStrain = new Matrix();
        public Matrix ConventerStressStrain
        {
            get { return _conventerStressStrain; }
            set { _conventerStressStrain = value; }
        }
        Matrix _conventerShape = new Matrix();
        public Matrix ConventerShape
        {
            get { return _conventerShape; }
            set { _conventerShape = value; }
        }
        double _scale4Bars = 1.0;
        public double Scale4Bars
        {
            get { return _scale4Bars; }
            set { _scale4Bars = value; }
        }
        double _scale4Stress = 1.0;
        public double Scale4Stress
        {
            get { return _scale4Stress; }
            set { _scale4Stress = value; }
        }
        double _scale4Strain = 1.0;
        public double Scale4Strain
        {
            get { return _scale4Strain; }
            set { _scale4Strain = value; }
        }
    }
}
