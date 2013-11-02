using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Utility;
namespace SectionDrawerControl.Infrastructure
{
    public class VisualShapes : IVisualShapes
    {
        #region IVisualShapes Members
        PathGeometry _baseGeo = new PathGeometry();
        public PathGeometry BaseGeo
        {
            get { return _baseGeo; }
            set { _baseGeo = value; }
        }
        PathGeometry _renderedGeo = new PathGeometry();
        public System.Windows.Media.PathGeometry RenderedGeo
        {
            get { return _renderedGeo; }
            set { _renderedGeo = value; }
        }

        void IVisualShapes.UpdateRenderedGeometry(MatrixTransform conventer)
        {
            _renderedGeo = new PathGeometry(_baseGeo.Figures, _baseGeo.FillRule, conventer);
        }
        #endregion
    }
    //
    public class VisualFactory
    {
        static VisualFactory _visualFactory = new VisualFactory();
        public static VisualFactory Instance()
        {
            if (_visualFactory == null)
            {
                _visualFactory = new VisualFactory();
            }
            return  _visualFactory;
        }
        protected VisualFactory ()
	    {
	    }
        public IVisualShapes CreateShapes()
        {
            return new VisualShapes();
        }
    }

}
