using System;
using System.Linq;
using System.Windows.Media;

namespace XEP_SectionDrawer.Infrastructure
{
    public class VisualShapes : IVisualShapes
    {
        #region IVisualShapes Members
        Matrix _additionMatrix = new Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
        public Matrix AdditionMatrix
        {
            get { return _additionMatrix; }
            set { _additionMatrix = value; }
        }
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
//             if (_additionMatrix != null && _additionMatrix.Determinant != 1.0)
//             {
//                 MatrixTransform mergedMatrix = new MatrixTransform();
//                 mergedMatrix.Matrix = new Matrix
//                     (conventer.Matrix.M11 + _additionMatrix.M11, conventer.Matrix.M12 + _additionMatrix.M12, conventer.Matrix.M21 + _additionMatrix.M21,
//                     conventer.Matrix.M22 + _additionMatrix.M22, conventer.Matrix.OffsetX + _additionMatrix.OffsetX, conventer.Matrix.OffsetY + _additionMatrix.OffsetY);
//                 _renderedGeo = new PathGeometry(_baseGeo.Figures, _baseGeo.FillRule, mergedMatrix);
//             }
            _renderedGeo = new PathGeometry(_baseGeo.Figures, _baseGeo.FillRule, conventer);
        }

        void IVisualShapes.UpdateBaseGeometry(MatrixTransform conventer)
        {
            _baseGeo = new PathGeometry(_baseGeo.Figures, _baseGeo.FillRule, conventer);
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
