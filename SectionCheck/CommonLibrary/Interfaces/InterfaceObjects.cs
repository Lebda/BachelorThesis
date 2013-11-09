using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CommonLibrary.Geometry;

namespace CommonLibrary.InterfaceObjects
{
    [Serializable]
    public class StrainStressItem
    {
        public StrainStressItem(Point position, double disNeuAxis, double valueInPos)
        {
            _position = GeometryOperations.Copy(position);
            _disNeuAxis = disNeuAxis;
            _valueInPos = valueInPos;
        }
        Point _position = new Point();
        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }
        double _disNeuAxis = 0.0;
        public double DisNeuAxis
        {
            get { return _disNeuAxis; }
            set { _disNeuAxis = value; }
        }
        double _valueInPos = 0.0;
        public double ValueInPos
        {
            get { return _valueInPos; }
            set { _valueInPos = value; }
        }
    }
}
