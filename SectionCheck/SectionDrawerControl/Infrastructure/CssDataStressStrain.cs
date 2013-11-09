﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ResourceLibrary;
using System.Windows.Media;
using CommonLibrary.Infrastructure;
using SectionDrawerControl.Utility;
using CommonLibrary.Utility;
using System.Windows.Shapes;
using CommonLibrary.Geometry;
using CommonLibrary.Interfaces;
using CommonLibrary.Factories;
using CommonLibrary.InterfaceObjects;
using CommonLibrary.DrawingGraph;

namespace SectionDrawerControl.Infrastructure
{
    [Serializable]
    public class CssDataFibers : CssDataBase
    {
        public static readonly double s_moveStressStrain = 1.25;
        public static readonly double s_maxCssWidthStressStrain = 0.5;
         public CssDataFibers()
            : base(Application.Current.TryFindResource(CustomResources.ConcreteStrainBrush1_SCkey) as Brush, 
                    Application.Current.TryFindResource(CustomResources.ConcreteStrainPen1_SCkey) as Pen)
        {

        }
         public CssDataFibers(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
        }

         IGeometryMaker _geometryMaker = GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete);
         public IGeometryMaker GeometryMaker
         {
             get { return _geometryMaker; }
             set { _geometryMaker = value; }
         }

        #region STRAIN STRESS GEOMETRY
         public PathGeometry GetStrainGeometry(double CssWidth, bool move = true)
         {
             return DoWorkGeometryMaker(move, s_maxCssWidthStressStrain * CssWidth, s_moveStressStrain * CssWidth, true);
         }
         public PathGeometry GetStressGeometry(double CssWidth, bool move = true)
         {
             return DoWorkGeometryMaker(move, s_maxCssWidthStressStrain * CssWidth, 1.75 * s_moveStressStrain * CssWidth, false);
         }
         private PathGeometry DoWorkGeometryMaker(bool move, double maxWidth, double moveSize, bool isStrain)
         {
             Exceptions.CheckNull(_geometryMaker);
             _geometryMaker.IsMove = move;
             _geometryMaker.MaxWidth = maxWidth;
             _geometryMaker.MoveSize = moveSize;
             _geometryMaker.IsStrain = isStrain;
             _geometryMaker.ShapeMaker = Exceptions.CheckNull<IStrainStressShape>(StrainStressShapeFactory.Instance().Create());
             _geometryMaker.ShapeMaker.NeuAxis = _neuAxisProperty;
             return _geometryMaker.CreateGeometry(_fibersProperty);
         }
        //
        #endregion

        public override PathGeometry Create()
        {
            return null;
        }

        #region OBSERVABLE MEMBERS
        public const string FibersPropertyName = "Fibers";
        private List<ICssDataFiber> _fibersProperty = Exceptions.CheckNull(new List<ICssDataFiber>());
        public List<ICssDataFiber> Fibers
        {
            get
            {
                return _fibersProperty;
            }

            set
            {
                if (_fibersProperty == value)
                {
                    return;
                }
                _fibersProperty = value;
                RaisePropertyChanged(FibersPropertyName);
            }
        }

        public const string NeuAxisPropertyName = "NeuAxis";
        private ILine2D _neuAxisProperty = Line2DFactory.Instance().Create();
        /// <summary>
        /// value in [rad]
        /// Sets and gets the NeuAxisAngle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ILine2D NeuAxis
        {
            get
            {
                return _neuAxisProperty;
            }
            set
            {
                if (_neuAxisProperty == value)
                {
                    return;
                }
                _neuAxisProperty = value;
                RaisePropertyChanged(NeuAxisPropertyName);
            }
        }
        #endregion
    }
}
