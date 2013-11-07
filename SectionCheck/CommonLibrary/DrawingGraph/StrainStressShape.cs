using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Interfaces;

namespace CommonLibrary.DrawingGraph
{

    public class StrainStressShape : IStrainStressShape
    {

        #region IStrainStressShape Members

        ILine2D IStrainStressShape.BaseLine
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IStrainStressShape Members


        public System.Windows.Media.PointCollection StrainShape
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IStrainStressShape Members


        public System.Windows.Media.PointCollection GetWholeShape()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IStrainStressShape Members

        public ILine2D NeuAxis
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IStrainStressShape Members


        public List<Tuple<System.Windows.Point, double>> ValuesWithPos
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IStrainStressShape Members


        public void ScaleValues(double scale)
        {
            throw new NotImplementedException();
        }

        public void Transform(System.Windows.Media.Matrix conventer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
