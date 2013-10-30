using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows.Media;
using CommonLibrary.Utility;
using ShapeType = System.Collections.Generic.List<System.Windows.Media.PointCollection>;

namespace SectionDrawerControl.Infrastructure
{
    public abstract class CssDataBase : ObservableObject
    {
        protected ShapeType _shapeObjetcs = null;
        public ShapeType ShapeObjetcs
        {
            get { return _shapeObjetcs; }
            protected set { _shapeObjetcs = value; }
        }
        //
        protected CssDataBase(int shapesCount)
        {
            _shapeObjetcs = new ShapeType();
            for (int counter = 0; counter < shapesCount; ++counter)
            {
                _shapeObjetcs.Add(new PointCollection());
            }
        }
        //
        protected ShapeType GetAllShapesDeepCopy()
        {
            ShapeType deepCopy = new ShapeType();
            foreach (PointCollection iter in _shapeObjetcs)
            {
                deepCopy.Add(new PointCollection(iter));
            }
            return deepCopy;
        }
    }
}
