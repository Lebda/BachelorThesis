using System;
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
        #region STRAIN STRESS GEOMETRY
         public PathGeometry GetStrainGeometry(double CssWidth, bool move = true)
         {
             IStrainStressShape shape = StrainStressShapeFactory.Instance().Create();
             Exceptions.CheckNull(shape);
             shape.NeuAxis = _neuAxisProperty;
             List<StrainStressItem> items = CreateStressStrainItems(_fibersProperty, true);
             shape.SetPointValues4MaxWidth(items, s_maxCssWidthStressStrain * CssWidth);
             if (move)
             {
                 shape.TranslateInDirNeuAxis(s_moveStressStrain * CssWidth);
             }
             PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
             myPathGeometry.Figures.Add(GeometryOperations.Create(shape.WholeShape));
             myPathGeometry.FillRule = FillRule.Nonzero;
             return myPathGeometry;
         }
        //
         public PathGeometry GetStressGeometry(double CssWidth, bool move = true)
         {
             IStrainStressShape shape = StrainStressShapeFactory.Instance().Create();
             Exceptions.CheckNull(shape);
             shape.NeuAxis = _neuAxisProperty;
             List<StrainStressItem> items = CreateStressStrainItems(_fibersProperty, false);
             shape.SetPointValues4MaxWidth(items, s_maxCssWidthStressStrain * CssWidth);
             if (move)
             {
                 shape.TranslateInDirNeuAxis(1.75 * s_moveStressStrain * CssWidth);
             }
             PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
             myPathGeometry.Figures.Add(GeometryOperations.Create(shape.WholeShape));
             myPathGeometry.FillRule = FillRule.Nonzero;
             return myPathGeometry;
         }
        #endregion

        public override PathGeometry Create()
        {
            return null;
        }

        private List<StrainStressItem> CreateStressStrainItems(List<ICssDataFiber> fibers, bool strain = true)
        {
            List<StrainStressItem> items = new List<StrainStressItem>();
            foreach (CssDataFiberCon fiber in fibers)
            {
                if (strain)
                {
                    items.Add(new StrainStressItem(fiber.Point, fiber.DistanceFromNeuAxis, fiber.GetFiberData<SSInFiber>(SSInFiber.s_name).Strain));
                }
                else
                {
                    items.Add(new StrainStressItem(fiber.Point, fiber.DistanceFromNeuAxis, fiber.GetFiberData<SSInFiber>(SSInFiber.s_name).Stress));
                }
            }
            return items;
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
